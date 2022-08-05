# FNA Updater
Crappy code to make installing and updating FNA easier. This command line program will download/update the FNA code and the pre-compiled DLL's.

# Download
The latest release is available on the releases page: https://github.com/vonderborch/FnaUpdater/releases/latest

# Execution
The executable has two main paths, one for installing FNA and the other for updating FNA

## Installing FNA
- As Cloned Repo:
    - `.\FnaUpdater.exe install <BASE_SOLUTION_DIRECTORY> [SUB_DIRECTORY]`
        - BASE_SOLUTION_DIRECTORY: (REQUIRED) the base directory we'll be cloning FNA into
        - SUB_DIRECTORY: (OPTIONAL) the subdirectory in the BASE_SOLUTION_DIRECTORY to place the cloned repo. Defaults to FNA
    - Example: `.\FnaUpdater.exe install "C:\MyProjects\MyFNAGame" FNA`
- As Submodule in Repo:
    - `.\FnaUpdater.exe install -m <BASE_SOLUTION_DIRECTORY> [SUB_DIRECTORY]`
        - BASE_SOLUTION_DIRECTORY: (REQUIRED) the base directory we'll be downloading FNA into
        - SUB_DIRECTORY: (OPTIONAL) the subdirectory in the BASE_SOLUTION_DIRECTORY to place the submodule. Defaults to FNA
    - Example: `.\FnaUpdater.exe install -m "C:\MyProjects\MyMonogameAndFnaGame" FNA\FNA`

## Updating FNA
- As Cloned Repo:
    - `.\FnaUpdater.exe update <BASE_SOLUTION_DIRECTORY> [SUB_DIRECTORY]`
        - BASE_SOLUTION_DIRECTORY: (REQUIRED) the base directory the cloned repo is in
        - SUB_DIRECTORY: (OPTIONAL) the subdirectory in the BASE_SOLUTION_DIRECTORY that the cloned repo is in. Defaults to FNA
    - Example: `.\FnaUpdater.exe update "C:\MyProjects\MyFNAGame" FNA`
- As Submodule in Repo:
    - `.\FnaUpdater.exe update -m <BASE_SOLUTION_DIRECTORY> [SUB_DIRECTORY]`
        - BASE_SOLUTION_DIRECTORY: (REQUIRED) the base directory we'll be downloading FNA into
        - SUB_DIRECTORY: (OPTIONAL) the subdirectory in the BASE_SOLUTION_DIRECTORY to place the submodule. Defaults to FNA
    - Example: `.\FnaUpdater.exe update -m "C:\MyProjects\MyMonogameAndFnaGame" FNA\FNA`


## Notes
Links (for example: the pre-compiled lib DLLs) will _not_  be automatically created in your solution and will still need to be manually done by following the normal procedure: https://github.com/FNA-XNA/FNA/wiki/1:-Download-and-Update-FNA

# Known Issues
- The code is terrible :)

Have an issue? https://github.com/vonderborch/FnaUpdater/issues

# Future Plans
See list of issues under the Milestones: https://github.com/vonderborch/FnaUpdater/milestones