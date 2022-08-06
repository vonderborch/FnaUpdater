using System;
using System.Threading.Tasks;

namespace FnaUpdater.Core.Runners
{
    /// <summary>
    ///     An install fna.
    /// </summary>
    /// <seealso cref="FnaRunner" />
    public class InstallFna : FnaRunner
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="options">                      Options for controlling the operation. </param>
        /// <param name="precompiledZipDownloadMethod"> The precompiled zip download method. </param>
        public InstallFna(Options.Options options, Func<string, string, Task> precompiledZipDownloadMethod)
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
            WriteToConsole("Adding FNA as a submodule...");
            RunCommand($"git submodule add {Constants.FnaRepo}.git {this.InstallDirectory}", baseDir);
            WriteToConsole("Pulling FNA submodules...");
            RunCommand("git submodule update --init --recursive", baseDir);

            // Step 2 - Download Pre-compiled libraries
            WriteToConsole("Downloading pre-compiled libraries...");
            DownloadCompiledLibs();
            WriteToConsole("FNA is installed and up-to-date!");

            return string.Empty;
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

            // Step 1 - Download the Repo
            WriteToConsole("Cloning FNA repo...");
            RunCommand($"git clone --recursive {Constants.FnaRepo} {this.InstallDirectory}", baseDir);
            WriteToConsole("Pulling FNA submodules...");
            RunCommand("git submodule update --init --recursive", baseDir);

            // Step 2 - Download Pre-compiled libraries
            WriteToConsole("Downloading pre-compiled libraries...");
            DownloadCompiledLibs();
            WriteToConsole("FNA is installed and up-to-date!");

            return string.Empty;
        }
    }
}
