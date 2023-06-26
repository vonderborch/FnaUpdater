using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Tar;

namespace FnaUpdater.Core
{
	public abstract class Runner
	{
        private string _gitInstalledOutput = string.Empty;

		public string Execute(Options options, Func<string, bool> log)
		{
            if (options.AsSubmodule)
            {
                throw new NotImplementedException();
                ExecuteAsSubmodule(options, log);
            }
            else
            {
                ExecuteClone(options, log);
            }

            UpdatePrecompiledLibraries(options, log);

            log("Complete!");
            return "0";
        }

        protected abstract void ExecuteClone(Options options, Func<string, bool> log);

        protected abstract void ExecuteAsSubmodule(Options options, Func<string, bool> log);

        private void UpdatePrecompiledLibraries(Options options, Func<string, bool> log)
        {
            log("Downloading/Updating Precompiled Libraries...");
            // Cleanup previous libs
            if (Directory.Exists(options.FnalibsInstallPath))
            {
                log("  Cleaning previous pre-compiled libs...");
                Directory.Delete(options.FnalibsInstallPath, true);
            }
            if (File.Exists(options.FnalibsArchivePath))
            {
                log("  Cleaning previous pre-compiled libs archive...");
                File.Delete(options.FnalibsArchivePath);
            }

            // Download the Pre-Compiled Libs
            log("  Downloading pre-compiled libs...");
            using (var client = new HttpClient())
            {
                using (var s = client.GetStreamAsync(Constants.PrecompiledFnaLibraries))
                {
                    using (var fs = new FileStream(options.FnalibsArchivePath, FileMode.Create))
                    {
                        s.Result.CopyTo(fs);
                    }
                }
            }

            // Extract the FNALibs to their directory
            log("  Extracting pre-compiled libs...");
            using (var inStream = File.OpenRead(options.FnalibsArchivePath))
            {
                using (var zipStream = new BZip2InputStream(inStream))
                {
                    var tarArchive = TarArchive.CreateInputTarArchive(zipStream);
                    tarArchive.ExtractContents(options.FnalibsInstallPath);
                    tarArchive.Close();
                }
            }

            // Cleanup the FNALibs Archive
            log("  Cleaning pre-compiled libs archive...");
            File.Delete(options.FnalibsArchivePath);

            // Done!
            log($"  Pre-compiled libs downloaded: {options.FnalibsInstallPath}");

            if (options.AskUserInput)
            {
                Console.WriteLine("Press any key to continue...");
            }
        }

        protected bool InGitRepo(string directory)
        {
            if (Directory.GetDirectories(directory).Any(x => Path.GetFileName(x) == ".git"))
            {
                return true;
            }
            var parentDir = Path.GetDirectoryName(directory);

            return parentDir != null
                ? InGitRepo(parentDir)
                : false;
        }
    }
}

