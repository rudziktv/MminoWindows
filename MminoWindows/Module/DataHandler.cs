using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MminoWindows.Triggers;

namespace MminoWindows.Module
{
    static internal class DataHandler
    {
        public static Basic.MainViewModel MainViewModel { get; set; } = new();
        public static UpdateViewCommand UpdateViewCommand { get; set; } = new(MainViewModel);
    }
}
