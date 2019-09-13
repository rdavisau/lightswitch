using System.Collections.Generic;
using Gtk;

namespace LightSwitch.Ide.VSMac
{
    public static class Extensions
    {
        public static T SetContent<T>(this T box, params Widget[] widgets)
            where T : Box
        {
            foreach (var widget in widgets)
                box.PackStart(widget);

            return box;
        }
    }
}

