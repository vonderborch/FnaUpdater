using CrossCommands;

namespace FnaUpdater.Core
{
    public class Updater : Runner
    {
        public Updater()
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
            log("Updating FNA and submodules...");
            Cross.Commands.RunCommand(options.FnaRootInstallPath, "git submodule update --recursive --remote");
            Cross.Commands.RunCommand(options.FnaSourceInstallPath, "git submodule update --recursive --remote");
            log("  Submodules installed!");
        }

        protected override void ExecuteClone(Options options, Func<string, bool> log)
        {
            // Clone repo
            log("Cloning FNA repo...");
            Cross.Commands.RunCommand(options.FnaSourceInstallPath, "git pull origin master");
            log($"  FNA Repo updated!");

            // Update submodules
            log("Pulling submodules...");
            Cross.Commands.RunCommand(options.FnaRootInstallPath, "git submodule update --recursive --remote");
            Cross.Commands.RunCommand(options.FnaSourceInstallPath, "git submodule update --recursive --remote");
            log("  Submodules updated!");
        }
    }
}

