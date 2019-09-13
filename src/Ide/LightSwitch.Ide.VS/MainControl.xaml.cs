using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LightSwitch.Core;
using LightSwitch.Ide.Core;

namespace LightSwitch.Ide.VS
{
    public partial class MainControl : UserControl
    {
        private const int MinimumInterval = 1000;
        private int Interval = MinimumInterval;

        readonly TextBox IntervalText = new TextBox
        {
            Text = $"{MinimumInterval}",
            Padding = new Thickness(8),
            Margin = new Thickness(4)
        };

        readonly Label MinIntervalLabel = new Label { Content = "" };

        public Dictionary<Button, VisualOverride> ModeButtons =
            new Dictionary<Button, VisualOverride>
            {
                [Button("No Override")] = VisualOverride.None,
                [Button("Light")] = VisualOverride.Light,
                [Button("Dark")] = VisualOverride.Dark,
                [Button("Interval")] = VisualOverride.Toggle,
            };

        SolidColorBrush DefaultButtonColor, ActiveButtonColor;
     
        public MainControl()
        {
            InitializeComponent();
            SetupViews();
        }

        private void SetupViews()
        {
            foreach (var button in ModeButtons.Keys)
            {
                MainPanel.Children.Add(button);
                button.Click += ButtonOnClick;
            }

            MainPanel.Children.Add(IntervalText);

            IntervalText.TextChanged += IntervalTextOnTextChanged;
            Env.Instance.VisualOverrideChanged += VisualOverrideChanged;

            DefaultButtonColor = (SolidColorBrush)ModeButtons.First().Key.Background;
            ActiveButtonColor = new SolidColorBrush(Colors.Green);
        }

        private async void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            var kind = ModeButtons[(Button)sender];
            var interval = Int32.Parse(IntervalText.Text);

            await Env.Instance.SetVisualOverride(kind, interval);
        }

        private void IntervalTextOnTextChanged(object sender, TextChangedEventArgs e)
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
                MinIntervalLabel.Content = $"Min {MinimumInterval}ms";
                Interval = 1000;
            }
            else { MinIntervalLabel.Content = ""; }

            Env.Instance.CurrentInterval = Interval;
        }

        private void VisualOverrideChanged(object sender, VisualOverrideChangedEventArgs e)
        {
            foreach (var (button, @override) in ModeButtons)
                StyleForSelected(button, @override == e.NewVisualOverride);
        }

        public void StyleForSelected(Button button, bool selected)
        {
            button.Background = selected ? ActiveButtonColor : DefaultButtonColor;
        }

        static Button Button(string text)
        {
            var button = new Button
            {
                Content = text,
                Padding = new Thickness(4),
                Margin = new Thickness(2)
            };

            return button;
        }
    }
}