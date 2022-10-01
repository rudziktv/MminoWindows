using MminoWindows.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MminoWindows.WindowModel
{
    internal class AboutWindowModel : Basic.WindowViewModel
    {
        public AboutWindowModel()
        {
            this.window = new AboutWindow();
        }
    }
}
