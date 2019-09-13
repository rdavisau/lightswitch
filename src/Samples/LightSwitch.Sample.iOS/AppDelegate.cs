using Foundation;
using LightSwitch.Agent;
using LightSwitch.Core;
using UIKit;

namespace LightSwitch.Sample.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            LightSwitchAgent.Init();

            return true;
        }
    }
}
