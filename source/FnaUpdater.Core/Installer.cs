using CrossCommands;

namespace FnaUpdater.Core
{
	public class Installer : Runner
	{
		public Installer()
		{
		}

        protected override void ExecuteAsSubmodule(Options options, Func<string, bool> log)
        {
            // make sure we're in a git repo...
            if (!InGitRepo(options.FnalibsInstallPath))
            {
                throw new ArgumentException($"Install path '{options.FnalibsInstallPath}' is not in a git repo!");
            }

            // Clone repo
            log("Adding FNA as a submodule...");
            Cross.Commands.RunCommand(options.FnaRootInstallPath, $"git submodule add {Constants.FnaRepo}.git {options.FnaSourceInstallPath}");
            log($"  FNA Repo added as submodule!");

            // Update submodules
            log("Pulling submodules...");
            Cross.Commands.RunCommand(options.FnaRootInstallPath, "git submodule update --init --recursive");
            log("  Submodules installed!");
        }

        protected override void ExecuteClone(Options options, Func<string, bool> log)
        {
            // Clone repo
            log("Cloning FNA repo...");
            Cross.Commands.RunCommand(options.FnaRootInstallPath, $"git clone --recursive {Constants.FnaRepo} {options.FnaSourceInstallPath}");
            log($"  FNA Repo cloned to: {options.FnaSourceInstallPath}");

            // Update submodules
            log("Pulling submodules...");
            Cross.Commands.RunCommand(options.FnaRootInstallPath, "git submodule update --init --recursive");
            log("  Submodules installed!");
        }
    }
}

