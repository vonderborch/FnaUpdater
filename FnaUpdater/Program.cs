using FnaUpdater.Core;

namespace MyApp // Note: actual namespace depends on the project name.
{
    /// <summary>
    ///     A program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        ///     Main entry-point for this application.
        /// </summary>
        /// <param name="args"> An array of command-line argument strings. </param>
        private static void Main(string[] args)
        {
            new Updater().ParseAndRun(args);
        }
    }
}
