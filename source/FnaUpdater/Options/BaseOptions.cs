using System;
using CommandLine;
using CommandLine.Text;

namespace FnaUpdater.Options
{
    /// <summary>
    /// An options.
    /// </summary>
	public class BaseOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether to print for user input after execution.
        /// </summary>
        /// <value>
        /// True if ask for user input, false if not.
        /// </value>
        [Option('a', "ask_user_input", Default = false, Required = false, HelpText = "Whether to print that we're waiting for user input on completion.")]
        public bool AskUserInput { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether as submodule.
        /// </summary>
        /// <value>
        /// True if as submodule, false if not.
        /// </value>
        [Option('m', "as_submodule", Default = false, Required = false, HelpText = "Perform the operation treating FNA as a submodule.")]
        public bool AsSubmodule { get; set; }

        /// <summary>
        /// Gets or sets the pathname of the working directory.
        /// </summary>
        /// <value>
        /// The pathname of the working directory.
        /// </value>
        [Value(0, MetaName = "working_directory", Required = false)]
        public string WorkingDirectory { get; set; }

        /// <summary>
        /// Gets or sets the pathname of the install directory.
        /// </summary>
        /// <value>
        /// The pathname of the install directory.
        /// </value>
        [Value(1, Default = "FNA", MetaName = "install_path", Required = false)]
        public string InstallDirectoryName { get; set; }

        public Core.Options AsCoreOptions()
        {
            var workingDirectory = string.IsNullOrWhiteSpace(WorkingDirectory)
                ? Directory.GetCurrentDirectory()
                : WorkingDirectory;

            return new Core.Options()
            {
                AskUserInput = AskUserInput,
                AsSubmodule = AsSubmodule,
                WorkingDirectory = workingDirectory,
                InstallDirectoryName = InstallDirectoryName,
            };
        }
    }
}

