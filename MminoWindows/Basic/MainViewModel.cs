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
    internal class MainViewModel : WindowViewModel, IProgress<KeyValuePair<long, long>>
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

        private float _progress;

        public float Progress
        {
            get { return _progress; }
            set
            { 
                _progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        private float _downloadedMegabytes;

        public float DownloadedMegabytes
        {
            get { return _downloadedMegabytes; }
            set
            {
                _downloadedMegabytes = value;
                OnPropertyChanged(nameof(DownloadedMegabytes));
            }
        }

        public float FileSize { get; set; }


        public UpdateViewCommand UpdateViewCommand { get; set; }
        public Command GetUpdatesCommand { get; set; }
        private CancellationToken cancellationToken;

        public MainViewModel()
        {
            
            UpdateViewCommand = new(this);
            SelectedViewModel = new BeginViewModel();
            InitializeWindow(App.Current.MainWindow);
            GetUpdatesCommand = new(GetUpdates);
        }

        private async void GetUpdates()
        {
            try
            {
                AutoUpdateService.CheckDownloadUpdate(this);
            }
            catch
            {
                Console.WriteLine("ERROR");
            }
        }

        public void Report(KeyValuePair<long, long> value)
        {
            Progress = ((float)value.Key / (float)value.Value);
            DownloadedMegabytes = (float)ConvertUnits.FileSize.ConvertFromBytes(ConvertUnits.FileSize.SizeUnit.megabytes, (double)value.Key);
            if (FileSize != (float)ConvertUnits.FileSize.ConvertFromBytes(ConvertUnits.FileSize.SizeUnit.megabytes, (double)value.Value))
            {
                FileSize = (float)ConvertUnits.FileSize.ConvertFromBytes(ConvertUnits.FileSize.SizeUnit.megabytes, (double)value.Value);
                OnPropertyChanged(nameof(FileSize));
            }
        }
    }
}
