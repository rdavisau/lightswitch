using System;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Foundation;
using Pages;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace LightSwitch.Sample.Forms
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Xamarin.Forms.Forms.Init();
            LightSwitchAgent.Init();

            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            Window.RootViewController = new MyPage().CreateViewController();
            Window.MakeKeyAndVisible();

            ListIPAddresses();

            return true;
        }

        private void ListIPAddresses()
        {
            try
            {
                var inet =
                    NetworkInterface
                        .GetAllNetworkInterfaces()
                        .SelectMany(x =>
                            x.GetIPProperties()
                             .UnicastAddresses.Where(y => y.Address.AddressFamily == AddressFamily.InterNetwork))
                        .Select(y => y.Address);

                Debug.WriteLine(String.Join(Environment.NewLine, inet));

            }
            catch (Exception ex) { Debug.WriteLine(ex); }
        }
    }
}