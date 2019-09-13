using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LightSwitch.Agent.Implementation;
using LightSwitch.Core;
using UIKit;

namespace LightSwitch.Agent
{
    public class ThemeAgent : ThemeAgentBase
    {
        private readonly Dictionary<VisualOverride, UIUserInterfaceStyle> _styleMappings =
            new Dictionary<VisualOverride, UIUserInterfaceStyle>
            {
                [VisualOverride.None] = UIUserInterfaceStyle.Unspecified,
                [VisualOverride.Dark] = UIUserInterfaceStyle.Dark,
                [VisualOverride.Light] = UIUserInterfaceStyle.Light,
            };

        protected override void SetVisualStyleImpl(VisualOverride style)
            => UIApplication
                    .SharedApplication
                    .BeginInvokeOnMainThread(() => SetVisualStyleTo(style));

        private void SetVisualStyleTo(VisualOverride style)
        {
            var interfaceStyle = _styleMappings[style];
            var target = Options.TargetElementGetter == null
                ? UIApplication.SharedApplication.KeyWindow
                : Options.TargetElementGetter();

            switch (target)
            {
                case null:
                    Console.WriteLine("No element found to override interface style, ensure KeyWindow is set or Options.TargetElementGetter returns a value.");
                    break;

                case object o when HasOverrideUserInterfaceStyleProperty(target, out var prop):
                    prop.SetValue(o, interfaceStyle);
                    break;

                default:
                    throw new Exception($"Don't know how to override visual style on object {target}");
            }
        }

        private bool HasOverrideUserInterfaceStyleProperty(object target, out PropertyInfo prop)
            => target
                .GetType()
                .GetProperties()
                .ToDictionary(p => p.Name)
                .TryGetValue("OverrideUserInterfaceStyle", out prop);
    }
}
