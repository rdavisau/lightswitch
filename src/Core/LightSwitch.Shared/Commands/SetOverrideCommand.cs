using System;
using System.Linq;

namespace LightSwitch.Core.Commands
{
    public class SetOverrideCommand : ILightSwitchCommand
    {
        public VisualOverride Override { get; set; }
        public TimeSpan ToggleInterval { get; set; }

        public static SetOverrideCommand FromBuffer(byte[] buffer)
            => new SetOverrideCommand
            {
                Override = (VisualOverride) buffer[1],
                ToggleInterval = TimeSpan.FromMilliseconds(BitConverter.ToInt32(buffer, 2))
            };

        public byte[] ToBuffer()
            => new byte[] { (byte)CommandKind.SetOverrideMode, (byte)Override }
                .Concat(BitConverter.GetBytes((int)ToggleInterval.TotalMilliseconds))
                .ToArray();

        public void Deconstruct(out VisualOverride @override, out TimeSpan interval)
        {
            @override = Override;
            interval = ToggleInterval;
        }

        public static SetOverrideCommand Create(VisualOverride @override, int intervalMilliseconds)
            => new SetOverrideCommand
            {
                Override = @override,
                ToggleInterval = TimeSpan.FromMilliseconds(intervalMilliseconds)
            };
    }
}