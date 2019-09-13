using System;

namespace LightSwitch.Agent.Implementation
{
    public class AgentOptions
    {
        public bool Verbose { get; set; }
        public Func<object> TargetElementGetter { get; set; }
    }
}