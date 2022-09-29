using MminoWindows.Triggers;
using MminoWindows.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MminoWindows.Basic
{
    internal class MainViewModel : WindowViewModel
    {
        private BaseViewModel _selectedViewModel;

        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public UpdateViewCommand UpdateViewCommand { get; set; }

        public MainViewModel()
        {
            
            UpdateViewCommand = new(this);
            SelectedViewModel = new BeginViewModel();
            InitializeWindow(App.Current.MainWindow);
        }

    }
}
