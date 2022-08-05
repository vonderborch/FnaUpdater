using CommandLine;

namespace FnaUpdater.Core.Options
{
    /// <summary>
    ///     An options.
    /// </summary>
    public class Options
    {
        /// <summary>
        ///     Gets or sets a value indicating whether as submodule.
        /// </summary>
        /// <value>
        ///     True if as submodule, false if not.
        /// </value>
        [Option('m', "as_submodule", Required = false, HelpText = "Perform the operation treating FNA as a submodule.")]
        public bool AsSubmodule { get; set; }

        /// <summary>
        ///     Gets or sets the pathname of the working directory.
        /// </summary>
        /// <value>
        ///     The pathname of the working directory.
        /// </value>
        [Value(0, MetaName = "working_directory", Required = true)]
        public string WorkingDirectory { get; set; }

        /// <summary>
        ///     Gets or sets the pathname of the install directory.
        /// </summary>
        /// <value>
        ///     The pathname of the install directory.
        /// </value>
        [Value(1, Default = "FNA", MetaName = "install_path", Required = false)]
        public string InstallDirectory { get; set; }
    }
}
