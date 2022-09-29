using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MminoWindows.ControlModel
{
    internal class TopMenuControlModel : Basic.ViewModel
    {
        public TopMenuControlModel()
        {
            UpdateViewCommand = new(Module.DataHandler.MainViewModel);
        }
    }
}
