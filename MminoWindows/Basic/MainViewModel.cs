using MminoWindows.Module;
using MminoWindows.Services;
using MminoWindows.Triggers;
using MminoWindows.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

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
            SelectedViewModel = new BeginViewModel();
            UpdateViewCommand = new(this);
            InitializeWindow(App.Current.MainWindow);
        }

        public MainViewModel(Window window)
        {
            SelectedViewModel = new BeginViewModel();
            UpdateViewCommand = new(this);
            InitializeWindow(window);
        }
    }
}
