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
                var client = new HttpClient();
                var infoUrl = "https://raw.githubusercontent.com/rudziktv/MminoWindows/master/info.ini";
                var infoVersion = await client.GetStringAsync(infoUrl);
                var a = infoVersion.Split('\n');
                if (a[0].Split('=')[1].Trim('"') != GlobalData.CURRENT_VERSION)
                {
                    MessageBox.Show("Your version isn't updated.");
                    var fileUrl = a[1].Split('=')[1].Trim('"');
                    var version = await client.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead);
                    DownloadService.DownloadFile(version.Content, "download.zip", this);
                }
                else
                {
                    MessageBox.Show("Your app is up-to-date.");
                }

                var url = "https://github.com/rudziktv/MminoWindows/releases/download/0.0.0-preview-ffffff/mmino-windows.zip";
                //var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

                //DownloadingFile(response.Content);
                

                
                /*
                if (!Directory.Exists("/update/"))
                {
                    Directory.CreateDirectory("/update/");
                }
                else
                {
                    var b = Directory.GetFiles("/update/");
                    foreach (var item in b)
                    {
                        File.Delete(item);
                    }
                }
                ZipFile.ExtractToDirectory("update.zip", "/update/");
                */
            }
            catch
            {
                Console.WriteLine("ERROR");
            }
        }

        private Task DownloadingFile(HttpContent content)
        {
            MessageBox.Show("Started");
            return Task.Run(async () =>
            {
                var contentLength = content.Headers.ContentLength;
                contentLength ??= 0;
                long length = (long)contentLength;
                var progress = length;

                var fileStream = File.Create("update.zip");
                var a = content.CopyToAsync(fileStream);

                while (!a.IsCompletedSuccessfully)
                {
                    Report(new(fileStream.Length, length));
                }                
                fileStream.Close();
            });
        }

        public void Report(KeyValuePair<long, long> value)
        {
            Progress = ((float)value.Key / (float)value.Value);
        }
    }
}
