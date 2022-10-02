using MminoWindows.Basic;
using MminoWindows.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MminoWindows.WindowModel
{
    internal class UpdaterWindowModel : WindowViewModel
    {
        public UpdaterWindowModel()
        {
            InitializeWindow(new UpdaterWindow());
        }
    }
}
