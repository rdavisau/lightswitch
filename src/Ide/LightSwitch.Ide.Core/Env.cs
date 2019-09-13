using System;
using System.Threading.Tasks;
using LightSwitch.Core;
using LightSwitch.Core.Commands;

namespace LightSwitch.Ide.Core
{
    public class Env
    {
        public static Env Instance { get; private set; }
        static Env() => Instance = new Env();

        readonly CommandSender Sender = new CommandSender();

        public int CurrentInterval { get; set; }
        public VisualOverride CurrentOverride { get; set; }

        public event EventHandler<VisualOverrideChangedEventArgs> VisualOverrideChanged;

        public async Task SetVisualOverride(VisualOverride @override, int? interval = null)
        {
            var nextInterval = interval ?? CurrentInterval;

            var cmd = SetOverrideCommand.Create(@override, nextInterval);
            await Sender.SendCommand(cmd);

            CurrentOverride = @override;
            CurrentInterval = nextInterval;

            VisualOverrideChanged?.Invoke(
                this,
                VisualOverrideChangedEventArgs.Create(@override));
        }

        public Task DisableOverride()
            => SetVisualOverride(VisualOverride.None);

        public async Task ToggleOverride()
        {
            var nextOverride = CurrentOverride == VisualOverride.Dark
                ? VisualOverride.Light
                : VisualOverride.Dark;

            await SetVisualOverride(nextOverride);
        }

        public async Task ToggleInterval()
        {
            var nextOverride = CurrentOverride == VisualOverride.Toggle
                ? VisualOverride.None
                : VisualOverride.Toggle;

            await SetVisualOverride(nextOverride);
        }
    }
}

