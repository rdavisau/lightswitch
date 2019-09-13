using System;
using LightSwitch.Agent;
using LightSwitch.Agent.Implementation;
using LightSwitch.Core;

namespace LightSwitch
{
    /// <summary>
    /// LightSwitch
    /// </summary>
    public static class LightSwitchAgent
    {
        static readonly Lazy<IThemeAgent> implementation = new Lazy<IThemeAgent>(SetupAgent, System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Gets if the plugin is supported on the current platform.
        /// </summary>
        public static bool IsSupported => implementation.Value != null;

        /// <summary>
        /// Current plugin implementation to use
        /// </summary>
        public static IThemeAgent Current
        {
            get
            {
                IThemeAgent ret = implementation.Value;
                if (ret == null)
                    throw NotImplementedInReferenceAssembly();

                return ret;
            }
        }

        public static void Init(AgentOptions options = null)
        {
            var agent = implementation.Value;

            if (options != null)
                agent.Options = options;
        }

        static IThemeAgent SetupAgent()
        {
#if NETSTANDARD1_0 || NETSTANDARD2_0
            return null;
#else
#pragma warning disable IDE0022 // Use expression body for method
            var agent = new ThemeAgent();
            var receiver = new CommandReceiver
            {
                OnCommand = agent.ExecuteCommand
            };

            return agent;
#pragma warning restore IDE0022 // Use expression body for methods
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly() =>
            new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");

    }
}
