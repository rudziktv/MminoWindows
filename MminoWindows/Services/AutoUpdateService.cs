using MminoWindows.Module;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MminoWindows.Services
{
    internal class AutoUpdateService
    {
        private const string INFO_URL = "https://raw.githubusercontent.com/rudziktv/MminoWindows/master/info.ini";

        public static async Task<bool> CheckUpdate()
        {
            var client = new HttpClient();
            var infoFileIni = await client.GetStringAsync(INFO_URL);
            var version = infoFileIni.Split("\n")[0].Split("=")[1].Trim('"');
            return version == GlobalData.CURRENT_VERSION;
        }

        public static async Task<bool> CheckDownloadUpdate(IProgress<KeyValuePair<long, long>>? progress = default)
        {
            if (!await CheckUpdate())
            {
                var client = new HttpClient();
                var infoFileIni = await client.GetStringAsync(INFO_URL);
                var downloadUrl = infoFileIni.Split("\n")[1].Split("=")[1].Trim('"');
                var response = await client.GetAsync(downloadUrl, HttpCompletionOption.ResponseHeadersRead);
                var checkSum = response.Content.Headers.ContentMD5;

                if (File.Exists("update.zip"))
                {
                    var md5 = MD5.Create();
                    var file = File.OpenRead("update.zip");
                    var checkSumLocal = md5.ComputeHash(file);
                    file.Close();
                    if (CheckSumAreSame(checkSum, checkSumLocal))
                        return false;
                    else
                        await DownloadService.DownloadFile(response.Content, "update.zip", progress);  
                }
                else
                    await DownloadService.DownloadFile(response.Content, "update.zip", progress);
                return true;
            }
            return false;
        }

        

        private async void CheckDownloadInstallUpdate()
        {
            if (await CheckDownloadUpdate())
            {
                
            }
        }

        private void GenerateBat()
        {

        }

        private static bool CheckSumAreSame(byte[] array1, byte[] array2)
        {
            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
