using System;
using System.Collections.Generic;
using System.Linq;
using Gtk;
using LightSwitch.Core;
using LightSwitch.Ide.Core;
using MonoDevelop.Components;
using MonoDevelop.Ide.Gui;

namespace LightSwitch.Ide.VSMac
{
    public class MainPad : PadContent
    {
        public override Control Control { get; }
            = new MainPadControl();

        protected override void Initialize(IPadWindow window)
            => ((MainPadControl)Control).ShowAll();

        public override void Dispose()
            => Control.Dispose();
	}

	public class MainPadControl : VBox
    {
        private const int MinimumInterval = 1000;
        int Interval = MinimumInterval;

        readonly Entry IntervalText = new Entry { Text = $"{MinimumInterval}"};
        readonly Label MinIntervalLabel = new Label { Text = "" };

        public Dictionary<Button, VisualOverride> ModeButtons = 
            new Dictionary<Button, VisualOverride>
            {
                [Button("No Override")] = VisualOverride.None,
                [Button("Light")] = VisualOverride.Light,
                [Button("Dark")] = VisualOverride.Dark,
                [Button("Interval")] = VisualOverride.Toggle,
            };

        public MainPadControl()
        {
            this.SetContent(ModeButtons.Keys.ToArray());
            PackStart(IntervalText);
            PackStart(MinIntervalLabel);

            foreach (var b in ModeButtons.Keys)
                b.Clicked += ModeButtonClicked;

            IntervalText.Changed += IntervalText_Changed;
            Env.Instance.VisualOverrideChanged += VisualOverrideChanged;
        }

        private async void ModeButtonClicked(object sender, EventArgs e)
        {
            var kind = ModeButtons[(Button)sender];
            var interval = Int32.Parse(IntervalText.Text);

            await Env.Instance.SetVisualOverride(kind, interval);
        }

        private void IntervalText_Changed(object sender, EventArgs e)
        {
            if (!int.TryParse(IntervalText.Text, out Interval))
            {
                IntervalText.Text = Interval == 0
                    ? ""
                    : $"{Interval}";

                return;
            }

            if (Interval < 1000)
            {
                MinIntervalLabel.Text = $"Min {MinimumInterval}ms";
                Interval = 1000;
            }
            else { MinIntervalLabel.Text = ""; }

            Env.Instance.CurrentInterval = Interval;
        }
        
        public void VisualOverrideChanged(object sender, VisualOverrideChangedEventArgs e)
        {
            foreach (var (button, @override) in ModeButtons)
                StyleForSelected(button, @override == e.NewVisualOverride);
        }

        public void StyleForSelected(Button button, bool selected)
        {
            button.ModifyBg(StateType.Normal, selected ? Colors.SelectedBackgroundColor : Colors.UnselectedBackgroundColor);
            button.ModifyFg(StateType.Normal, selected ? Colors.SelectedTextColor : Colors.UnselectedTextColor);
        }

        static Button Button(string text)
        {
            var button = new Button(text);

            button.ModifyBg(StateType.Active, Colors.HighlightColor);
            button.ModifyBg(StateType.Prelight, Colors.HighlightColor);
            button.ModifyBg(StateType.Normal, Colors.UnselectedBackgroundColor);

            button.ModifyText(StateType.Active, Colors.UnselectedTextColor);
            button.ModifyText(StateType.Prelight, Colors.UnselectedTextColor);
            button.ModifyText(StateType.Normal, Colors.UnselectedTextColor);

            return button;
        }
    }
}

