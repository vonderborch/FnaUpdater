using CommandLine;

using FnaUpdater.Core.Options;
using FnaUpdater.Core.Runners;

namespace FnaUpdater.Core
{
    public class Updater
    {
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
        private static string InstallFNA(InstallOptions opts)
        {
            //handle options
            return new InstallFna(opts).Run();
        }

        /// <summary>
        ///     Updates the fna described by opts.
        /// </summary>
        /// <param name="opts"> Options for controlling the operation. </param>
        /// <returns>
        ///     A string.
        /// </returns>
        private static string UpdateFNA(UpdateOptions opts)
        {
            //handle options
            return new UpdateFna(opts).Run();
        }

        /// <summary>
        ///     Makes the error.
        /// </summary>
        /// <returns>
        ///     A string.
        /// </returns>
        private static string MakeError()
        {
            return "\0";
        }
    }
}
