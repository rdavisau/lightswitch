using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace LightSwitch.Ide.VS
{
    [Guid("63be7816-817a-4f41-8948-433031c1bfa3")]
    public class MainPad : ToolWindowPane
    {
        public MainPad() : base(null)
        {
            Caption = "LightSwitch 💡";

            this.Content = new MainControl();
        }
    }
}
