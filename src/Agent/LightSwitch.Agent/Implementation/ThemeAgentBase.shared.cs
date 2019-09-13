using System;
using System.Threading;
using System.Threading.Tasks;
using LightSwitch.Core;
using LightSwitch.Core.Commands;

namespace LightSwitch.Agent.Implementation
{
    public abstract class ThemeAgentBase : IThemeAgent
    {
        private Action _stopToggle;

        public AgentOptions Options { get; set; } = new AgentOptions();
        public TimeSpan ToggleInterval { get; set; } = TimeSpan.FromSeconds(1);
        protected abstract void SetVisualStyleImpl(VisualOverride style);

        public void ExecuteCommand(ILightSwitchCommand command)
        {
            switch (command)
            {
                case SetOverrideCommand setOverride:

                    var (mode, interval) = setOverride;

                    ToggleInterval = interval;
                    SetOverrideMode(mode);

                    break;
            }
        }

        public void SetOverrideMode(VisualOverride mode)
        {
            switch (mode)
            {
                case VisualOverride.Toggle:
                    StartToggle();
                    return;
                    
                default:
                    StopToggle();
                    SetVisualStyle(mode);
                    return;
            }
        }

        public void SetVisualStyle(VisualOverride style)
        {
            if (Options.Verbose)
                Console.WriteLine($"Setting visual style to: {style}");

            SetVisualStyleImpl(style);
        }

        public void StartToggle()
        {
            StopToggle();

            if (Options.Verbose)
                Console.WriteLine($"Starting toggle.");

            var canceler = new CancellationTokenSource();
            var delay = ToggleInterval;
            var nextMode = VisualOverride.Dark;

            Task.Factory.StartNew(async
            () =>
            {
                while (!canceler.IsCancellationRequested)
                {
                    nextMode = nextMode == VisualOverride.Dark
                        ? VisualOverride.Light
                        : VisualOverride.Dark;

                    SetVisualStyle(nextMode);

                    await Task.Delay(delay, canceler.Token);
                }

            }, TaskCreationOptions.LongRunning);

            _stopToggle = canceler.Cancel;
        }

        public void StopToggle()
        {
            if (Options.Verbose)
                Console.WriteLine($"Stopping toggle.");

            _stopToggle?.Invoke();
        }
    }
}