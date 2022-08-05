﻿using CommandLine;

namespace FnaUpdater.Options
{
    /// <summary>
    ///     An install options.
    /// </summary>
    /// <seealso cref="Options" />
    [Verb("install", HelpText = "Install FNA to the current repository")]
    public class InstallOptions : Options { }
}
