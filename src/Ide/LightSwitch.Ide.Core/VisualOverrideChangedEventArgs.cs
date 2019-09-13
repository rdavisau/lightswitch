using System;
using LightSwitch.Core;

namespace LightSwitch.Ide.Core
{
    public class VisualOverrideChangedEventArgs : EventArgs
    {
        public static VisualOverrideChangedEventArgs Create(VisualOverride newVisualOverride)
            => new VisualOverrideChangedEventArgs
            {
                NewVisualOverride = newVisualOverride
            };

        public VisualOverride NewVisualOverride { get; set; }
    }
}

