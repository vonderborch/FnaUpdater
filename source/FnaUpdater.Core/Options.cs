using System;
using System.Runtime.InteropServices;

namespace FnaUpdater.Core
{
    /// <summary>
    /// An options.
    /// </summary>
	public class Options
    {
        /// <summary>
        /// Gets or sets a value indicating asking for user input.
        /// </summary>
        /// <value>
        /// True if ask for user input, false if not.
        /// </value>
        public bool AskUserInput { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether as submodule.
        /// </summary>
        /// <value>
        /// True if as submodule, false if not.
        /// </value>
        public bool AsSubmodule { get; set; }

        /// <summary>
        /// Gets or sets the pathname of the working directory.
        /// </summary>
        /// <value>
        /// The pathname of the working directory.
        /// </value>
        public string WorkingDirectory { get; set; }

        /// <summary>
        /// Gets or sets the pathname of the install directory.
        /// </summary>
        /// <value>
        /// The pathname of the install directory.
        /// </value>
        public string InstallDirectoryName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FnaRootInstallPath => Path.Combine(WorkingDirectory, InstallDirectoryName);

        /// <summary>
        /// 
        /// </summary>
        public string FnalibsArchivePath => Path.Combine(WorkingDirectory, Constants.PrecompiledFnaLibrariesLocalFile);

        /// <summary>
        /// 
        /// </summary>
        public string FnalibsInstallPath => Path.Combine(FnaRootInstallPath, Constants.PrecompiledFnaLibrariesExtractedDirectory);

        /// <summary>
        /// 
        /// </summary>
        public string FnaSourceInstallPath => Path.Combine(FnaRootInstallPath, Constants.FnaRepoName);
    }
}

