using CommandLine;
using FnaUpdater.Core;
using FnaUpdater.Options;

namespace FnaUpdater;

class Program
{
    /// <summary>
    /// Execute the program
    /// </summary>
    /// <param name="args">Command line arguments</param>
    static void Main(string[] args)
    {
        /*
        //args = new string[] { "install", "/Users/christianwebber/Dropbox/Projects/temp", "FNA" };
        args = new string[] { "install", @"C:\Users\ricky\Dropbox\Projects\temp\test", "FNA" };
        var path = Path.Combine(args[1], args[2]);
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
            Directory.CreateDirectory(args[1]);
        }
        */

        // parse command line arguments and execute the appropriate command
        var parseResults = Parser.Default.ParseArguments<InstallOptions, UpdateOptions>(args);

        parseResults.MapResult(
            (InstallOptions opts) => new Installer().Execute(opts.AsCoreOptions(), Log),
            (UpdateOptions opts) => new Updater().Execute(opts.AsCoreOptions(), Log),
            _ => MakeError()
        );
    }

    public static bool Log(string message)
    {
        Console.WriteLine(message);
        return true;
    }

    /// <summary>
    /// Makes the error.
    /// </summary>
    /// <returns></returns>
    public static string MakeError()
    {
        return "\0";
    }
}

