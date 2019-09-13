using System.Collections.Generic;
using AndroidX.AppCompat.App;
using LightSwitch.Agent.Implementation;
using LightSwitch.Core;
using Plugin.CurrentActivity;

namespace LightSwitch.Agent
{
    public class ThemeAgent : ThemeAgentBase
    {
        private readonly Dictionary<VisualOverride, int> _styleMappings =
            new Dictionary<VisualOverride, int>
            {
                [VisualOverride.None] = AppCompatDelegate.ModeNightFollowSystem,
                [VisualOverride.Dark] = AppCompatDelegate.ModeNightYes,
                [VisualOverride.Light] = AppCompatDelegate.ModeNightNo
            };

        protected override void SetVisualStyleImpl(VisualOverride style)
        {
            var currentActivity = CrossCurrentActivity.Current.Activity as AppCompatActivity;

            currentActivity?.RunOnUiThread(() => SetVisualStyleTo(style));
        }

        private void SetVisualStyleTo(VisualOverride style)
        {
            AppCompatDelegate.DefaultNightMode = _styleMappings[style];

            if (CrossCurrentActivity.Current.Activity is AppCompatActivity activity)
                activity.Delegate.ApplyDayNight();
        }
    }
}
