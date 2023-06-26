using System;
using CommandLine;
using CommandLine.Text;

namespace FnaUpdater.Options
{
    /// <summary>
    /// An update options.
    /// </summary>
    /// <seealso cref="Options" />
    [Verb("update", HelpText = "Update an existing FNA install")]
    public class UpdateOptions : BaseOptions
    {
    }
}

