using Xamarin.Forms;

namespace LightSwitch.Sample.Forms
{
    public static class Themes
    {
        public static ResourceDictionary Light = new ResourceDictionary
        {
            [Keys.BackgroundColor] = Color.White.MultiplyAlpha(.85),
            [Keys.TextColor] = Color.Black.MultiplyAlpha(.85),
        };

        public static ResourceDictionary Dark = new ResourceDictionary
        {
            [Keys.BackgroundColor] = Color.Black.MultiplyAlpha(.85),
            [Keys.TextColor] = Color.White.MultiplyAlpha(.85),
        };
    }
}