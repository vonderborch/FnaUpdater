using System;
using System.Threading.Tasks;

using CommandLine;

using FnaUpdater.Core.Options;
using FnaUpdater.Core.Runners;

namespace FnaUpdater.Core
{
    public class Updater
    {
        private readonly Func<string, string, Task> _zipDownload;

        public Updater(Func<string, string, Task> precompiledZipDownloadMethod)
        {
            this._zipDownload = precompiledZipDownloadMethod;
        }

        public void ParseAndRun(string[] args)
        {
            var parseResults = Parser.Default.ParseArguments<InstallOptions, UpdateOptions>(args);
            var texts = parseResults.MapResult((InstallOptions opts) => InstallFNA(opts), (UpdateOptions opts) => UpdateFNA(opts), _ => MakeError());
        }

        /// <summary>
        ///     Installs the fna described by opts.
        /// </summary>
        /// <param name="opts"> Options for controlling the operation. </param>
        /// <returns>
        ///     A string.
        /// </returns>
        private string InstallFNA(InstallOptions opts)
        {
            //handle options
            return new InstallFna(opts, this._zipDownload).Run();
        }

        /// <summary>
        ///     Updates the fna described by opts.
        /// </summary>
        /// <param name="opts"> Options for controlling the operation. </param>
        /// <returns>
        ///     A string.
        /// </returns>
        private string UpdateFNA(UpdateOptions opts)
        {
            //handle options
            return new UpdateFna(opts, this._zipDownload).Run();
        }

        /// <summary>
        ///     Makes the error.
        /// </summary>
        /// <returns>
        ///     A string.
        /// </returns>
        private string MakeError()
        {
            return "\0";
        }
    }
}
