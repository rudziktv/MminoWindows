using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MminoWindows.Services
{
    internal class DownloadService
    {
        public static Task DownloadFile(HttpContent content, string path, IProgress<KeyValuePair<long, long>>? progress = default)
        {
            MessageBox.Show("Started");
            return Task.Run(async () =>
            {
                try
                {
                    var contentLength = content.Headers.ContentLength;
                    contentLength ??= 0;
                    long length = (long)contentLength;

                    var fileStream = File.Create(path);
                    var a = content.CopyToAsync(fileStream);

                    if (progress != null)
                    {
                        while (!a.IsCompletedSuccessfully)
                        {
                            progress.Report(new(fileStream.Position, length));
                        }
                        progress.Report(new(fileStream.Position, length));
                    }
                    else
                    {
                        await a;
                    }
                    fileStream.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
    }
}
