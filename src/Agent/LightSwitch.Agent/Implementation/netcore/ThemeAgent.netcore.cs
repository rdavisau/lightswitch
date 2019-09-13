using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LightSwitch.Agent
{
    public class ThemeAgent : ThemeAgentBase
    {
        protected override void SetVisualStyleImpl(VisualOverride style)
        {
            Debug.WriteLine(style);
        }
    }
}
