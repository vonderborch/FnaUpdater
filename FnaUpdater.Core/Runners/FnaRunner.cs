using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace FnaUpdater.Core.Runners
{
    /// <summary>
    ///     A fna runner.
    /// </summary>
    public abstract class FnaRunner
    {
        private readonly Func<string, string, Task> _zipDownload;

        /// <summary>
        ///     Specialized constructor for use only by derived class.
        /// </summary>
        /// <param name="options">                      Options for controlling the operation. </param>
        /// <param name="precompiledZipDownloadMethod"> The precompiled zip download method. </param>
        protected FnaRunner(Options.Options options, Func<string, string, Task> precompiledZipDownloadMethod)
        {
            this.AsSubmodule = options.AsSubmodule;
            this.WorkingDirectory = options.WorkingDirectory;
            this.InstallDirectory = options.InstallDirectory;
            this._zipDownload = precompiledZipDownloadMethod;
        }

        /// <summary>
        ///     Gets the pathname of the current directory.
        /// </summary>
        /// <value>
        ///     The pathname of the current directory.
        /// </value>
        protected string CurrentDirectory => Path.Combine(Directory.GetCurrentDirectory(), this.WorkingDirectory, Path.GetDirectoryName(this.InstallDirectory) ?? "");

        /// <summary>
        ///     Gets the current precompiled fna libraries local file.
        /// </summary>
        /// <value>
        ///     The current precompiled fna libraries local file.
        /// </value>
        protected string CurrentPrecompiledFnaLibrariesLocalFile => Path.Combine(this.CurrentDirectory, Constants.PrecompiledFnaLibrariesLocalFile);

        /// <summary>
        ///     Gets the pathname of the current precompiled fna libraries extracted directory.
        /// </summary>
        /// <value>
        ///     The pathname of the current precompiled fna libraries extracted directory.
        /// </value>
        protected string CurrentPrecompiledFnaLibrariesExtractedDirectory => Path.Combine(this.CurrentDirectory, Constants.PrecompiledFnaLibrariesExtractedDirectory);

        /// <summary>
        ///     Gets or sets the pathname of the working directory.
        /// </summary>
        /// <value>
        ///     The pathname of the working directory.
        /// </value>
        protected string WorkingDirectory { get; set; }

        /// <summary>
        ///     Gets or sets the pathname of the install directory.
        /// </summary>
        /// <value>
        ///     The pathname of the install directory.
        /// </value>
        protected string InstallDirectory { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether as submodule.
        /// </summary>
        /// <value>
        ///     True if as submodule, false if not.
        /// </value>
        protected bool AsSubmodule { get; set; }

        /// <summary>
        ///     Writes to console.
        /// </summary>
        /// <param name="message">  The message. </param>
        public void WriteToConsole(string message)
        {
            Console.WriteLine($"{Environment.NewLine}{message}{Environment.NewLine}");
        }

        /// <summary>
        ///     Gets the run.
        /// </summary>
        /// <returns>
        ///     A string.
        /// </returns>
        public abstract string Run();

        /// <summary>
        ///     Downloads the compiled libs.
        /// </summary>
        /// <returns>
        ///     A Task.
        /// </returns>
        protected void DownloadCompiledLibs()
        {
            this._zipDownload(this.CurrentPrecompiledFnaLibrariesExtractedDirectory, this.CurrentPrecompiledFnaLibrariesLocalFile).Wait();
        }

        /// <summary>
        ///     Executes the 'command' operation.
        /// </summary>
        /// <param name="command">      The command. </param>
        /// <param name="runDirectory"> Pathname of the run directory. </param>
        protected void RunCommand(string command, string runDirectory)
        {
            var cdDir = $"/C cd \"{runDirectory}\"";
            var cmd = Process.Start(Constants.CommandPrompt, $"{cdDir} & {command}");
            cmd.WaitForExit();
        }
    }
}
