using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace LightSwitch.Ide.VS
{
    [Guid("29459ccb-187a-4f48-bfe4-f25e123cd3a5")]
    public class Main : ToolWindowPane
    {
        public Main() : base(null)
        {
            this.Caption = "LightSwitch 💡";

            this.Content = new MainControl();
        }
    }
}
