using System.Diagnostics;

using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Tar;

namespace FnaUpdater.Runners
{
    /// <summary>
    ///     A fna runner.
    /// </summary>
    public abstract class FnaRunner
    {
        /// <summary>
        ///     Specialized constructor for use only by derived class.
        /// </summary>
        /// <param name="options">  Options for controlling the operation. </param>
        protected FnaRunner(Options.Options options)
        {
            this.AsSubmodule = options.AsSubmodule;
            this.WorkingDirectory = options.WorkingDirectory;
            this.InstallDirectory = options.InstallDirectory;
        }

        /// <summary>
        ///     Gets the pathname of the current directory.
        /// </summary>
        /// <value>
        ///     The pathname of the current directory.
        /// </value>
        protected string CurrentDirectory => Path.Combine(Directory.GetCurrentDirectory(), this.WorkingDirectory);

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
        protected async Task DownloadCompiledLibs()
        {
            if (Directory.Exists(this.CurrentPrecompiledFnaLibrariesExtractedDirectory))
            {
                Directory.Delete(this.CurrentPrecompiledFnaLibrariesExtractedDirectory, true);
            }

            if (File.Exists(this.CurrentPrecompiledFnaLibrariesLocalFile))
            {
                File.Delete(this.CurrentPrecompiledFnaLibrariesLocalFile);
            }

            var httpClient = new HttpClient();
            using (var stream = await httpClient.GetStreamAsync(Constants.PrecompiledFnaLibraries))
            {
                using (var fileStream = new FileStream(this.CurrentPrecompiledFnaLibrariesLocalFile, FileMode.CreateNew))
                {
                    await stream.CopyToAsync(fileStream);
                }
            }

            using (var inStream = File.OpenRead(this.CurrentPrecompiledFnaLibrariesLocalFile))
            {
                using (var zipStream = new BZip2InputStream(inStream))
                {
                    var tarArchive = TarArchive.CreateInputTarArchive(zipStream);
                    tarArchive.ExtractContents(this.CurrentPrecompiledFnaLibrariesExtractedDirectory);
                    tarArchive.Close();
                }
            }
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
