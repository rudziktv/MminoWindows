using MminoWindows.WindowModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MminoWindows.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy UpdaterWindow.xaml
    /// </summary>
    public partial class UpdaterWindow : Window
    {
        public UpdaterWindow()
        {
            InitializeComponent();
            var a = new UpdaterWindowModel(this);
            this.DataContext = a;
            a.CheckUpdates();
        }
    }
}
