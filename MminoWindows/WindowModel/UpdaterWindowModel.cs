using MminoWindows.Basic;
using MminoWindows.Module;
using MminoWindows.Services;
using MminoWindows.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static MminoWindows.Module.ConvertUnits;

namespace MminoWindows.WindowModel
{
    internal class UpdaterWindowModel : WindowViewModel, IProgress<KeyValuePair<long, long>>
    {
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

        private long downloadedBytes = 0;
        private long totalBytes = 0;

        private long lastDownloadedBytes = 0;

        private float _downloadSpeed;

        public float DownloadSpeed
        {
            get { return _downloadSpeed; }
            set
            { 
                _downloadSpeed = value;
                OnPropertyChanged(nameof(DownloadSpeed));
            }
        }

        public UpdaterWindowModel(Window window)
        {
            this.window = window;
        }

        public async Task CheckUpdatesWithOpen()
        {
            window.Show();
            System.Timers.Timer timer = new(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            await AutoUpdateService.CheckDownloadInstallUpdate(this);
            //window.Close();
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (totalBytes != 0)
            {
                var speed = downloadedBytes - lastDownloadedBytes;
                DownloadSpeed = (float)Math.Round((float)ConvertUnits.FileSize.ConvertFromBytes(ConvertUnits.FileSize.SizeUnit.megabytes, speed), 2);
                lastDownloadedBytes = downloadedBytes;
            }
        }

        public async Task CheckUpdates()
        {
            System.Timers.Timer timer = new(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            await AutoUpdateService.CheckDownloadInstallUpdate(this);
            App.Current.MainWindow = new MainWindow();
            App.Current.MainWindow.Show();
            window.Close();
        }

        public void Report(KeyValuePair<long, long> value)
        {
            downloadedBytes = value.Key;
            totalBytes = value.Value;

            Progress = ((float)value.Key / (float)value.Value);
            DownloadedMegabytes = (float)Math.Round((float)ConvertUnits.FileSize.ConvertFromBytes(ConvertUnits.FileSize.SizeUnit.megabytes, (double)value.Key), 2);
            if (FileSize != (float)ConvertUnits.FileSize.ConvertFromBytes(ConvertUnits.FileSize.SizeUnit.megabytes, (double)value.Value))
            {
                FileSize = (float)Math.Round((float)ConvertUnits.FileSize.ConvertFromBytes(ConvertUnits.FileSize.SizeUnit.megabytes, (double)value.Value), 2);
                OnPropertyChanged(nameof(FileSize));
            }
        }
    }
}
