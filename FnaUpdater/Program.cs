using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using FnaUpdater.Core;

using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Tar;

namespace FnaUpdater
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var updater = new Updater(DownloadCompiledLibs);
            updater.ParseAndRun(args);
            Console.ReadKey();
        }

        protected static async Task DownloadCompiledLibs(string extractedDirectory, string localFile)
        {
            if (Directory.Exists(extractedDirectory))
            {
                Directory.Delete(extractedDirectory, true);
            }

            if (File.Exists(localFile))
            {
                File.Delete(localFile);
            }

            var httpClient = new HttpClient();
            using (var stream = await httpClient.GetStreamAsync(Constants.PrecompiledFnaLibraries))
            {
                using (var fileStream = new FileStream(localFile, FileMode.CreateNew))
                {
                    await stream.CopyToAsync(fileStream);
                }
            }

            using (var inStream = File.OpenRead(localFile))
            {
                using (var zipStream = new BZip2InputStream(inStream))
                {
                    var tarArchive = TarArchive.CreateInputTarArchive(zipStream);
                    tarArchive.ExtractContents(extractedDirectory);
                    tarArchive.Close();
                }
            }
        }
    }
}
