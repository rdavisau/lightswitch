using System;
using LightSwitch.Ide.Core;
using MonoDevelop.Components.Commands;

namespace Dark.Ide.VSMac
{
    public class BaseCommandHandler : CommandHandler
    {
        public Env Env = Env.Instance;

        protected override void Run()
        {
            base.Run();

            Console.WriteLine(GetType().Name);
        }

        protected override void Update(CommandInfo info)
        {
            base.Update(info);

            info.Enabled = true;
        }
    }
    
    public class DisableOverrideHandler : BaseCommandHandler
    {
        protected override async void Run()
        {
            base.Run();
            await Env.DisableOverride();
        }
    }

    public class ToggleOverrideHandler : BaseCommandHandler
    {
        protected override async void Run()
        {
            base.Run();
            await Env.ToggleOverride();
        }
    }
    
    public class ToggleIntervalHandler : BaseCommandHandler
    {
        protected override async void Run()
        {
            base.Run();
            await Env.ToggleInterval();
        }
    }
}
