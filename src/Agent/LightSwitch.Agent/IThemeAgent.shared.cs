using System;
using LightSwitch.Agent.Implementation;
using LightSwitch.Core;

namespace LightSwitch.Agent
{
    public interface IThemeAgent
    {
        void SetOverrideMode(VisualOverride mode);

        TimeSpan ToggleInterval { get; set; }
        AgentOptions Options { get; set; }
    }
}
