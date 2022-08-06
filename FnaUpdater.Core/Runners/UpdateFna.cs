using System;
using System.IO;
using System.Threading.Tasks;

namespace FnaUpdater.Core.Runners
{
    /// <summary>
    ///     An update fna.
    /// </summary>
    /// <seealso cref="FnaRunner" />
    public class UpdateFna : FnaRunner
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="options">                      Options for controlling the operation. </param>
        /// <param name="precompiledZipDownloadMethod"> The precompiled zip download method. </param>
        public UpdateFna(Options.Options options, Func<string, string, Task> precompiledZipDownloadMethod)
            : base(options, precompiledZipDownloadMethod) { }

        /// <summary>
        ///     Gets the run.
        /// </summary>
        /// <returns>
        ///     A string.
        /// </returns>
        /// <seealso cref="FnaRunner.Run" />
        public override string Run()
        {
            return this.AsSubmodule ? RunAsSubmodule() : RunAsClone();
        }

        /// <summary>
        ///     Executes the 'as submodule' operation.
        /// </summary>
        /// <returns>
        ///     A string.
        /// </returns>
        public string RunAsSubmodule()
        {
            var baseDir = this.CurrentDirectory;

            // Step 1 - Download the Repo
            WriteToConsole("Updating FNA...");
            RunCommand("git submodule deinit -f FNA", baseDir);
            RunCommand($"git rm {this.InstallDirectory} -f", baseDir);
            var path = Path.Combine(baseDir, ".git", "modules", "FNA");
            DeleteRecursive(Path.Combine(baseDir, ".git", "modules", "FNA"));
            RunCommand($"git submodule add {Constants.FnaRepo}.git {this.InstallDirectory}", baseDir);

            WriteToConsole("Updating FNA submodules...");
            RunCommand("git submodule update --init --recursive", baseDir);

            // Step 2 - Download Pre-compiled libraries
            WriteToConsole("Downloading pre-compiled libraries...");
            DownloadCompiledLibs();
            WriteToConsole("FNA is installed and up-to-date!");

            return string.Empty;
        }

        /// <summary>
        ///     Deletes the recursive described by path.
        /// </summary>
        /// <param name="path"> Full pathname of the file. </param>
        private void DeleteRecursive(string path)
        {
            if (Directory.Exists(path))
            {
                // delete files
                foreach (var entry in Directory.GetFiles(path))
                {
                    if (File.Exists(entry))
                    {
                        File.SetAttributes(entry, FileAttributes.Normal);
                        File.Delete(entry);
                    }
                }

                // delete directories
                foreach (var entry in Directory.GetDirectories(path))
                {
                    if (Directory.Exists(entry))
                    {
                        DeleteRecursive(entry);
                    }
                }
            }

            Directory.Delete(path);
        }

        /// <summary>
        ///     Executes the 'as clone' operation.
        /// </summary>
        /// <returns>
        ///     A string.
        /// </returns>
        public string RunAsClone()
        {
            var baseDir = this.CurrentDirectory;
            var baseDirWithInstallPath = Path.Combine(baseDir, this.InstallDirectory);

            // Step 1 - Download the Repo
            WriteToConsole("Updating FNA...");
            RunCommand("git pull origin master", baseDirWithInstallPath);
            WriteToConsole("Updating FNA submodules...");
            RunCommand("git submodule update --init --recursive", baseDir);
            RunCommand("git submodule update --init --recursive", baseDirWithInstallPath);

            // Step 2 - Download Pre-compiled libraries
            WriteToConsole("Downloading pre-compiled libraries...");
            DownloadCompiledLibs();
            WriteToConsole("FNA is installed and up-to-date!");

            return string.Empty;
        }
    }
}
