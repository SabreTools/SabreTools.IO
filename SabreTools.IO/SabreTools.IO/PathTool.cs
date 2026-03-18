using System;

namespace SabreTools.IO
{
    /// <summary>
    /// Methods around path operations
    /// </summary>
    public static class PathTool
    {
        /// <summary>
        /// Get the home directory for the current user
        /// </summary>
        public static string GetHomeDirectory()
        {
#if NET20 || NET35
            return Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
#else
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
#endif
        }

        /// <summary>
        /// Get the runtime directory
        /// </summary>
        public static string GetRuntimeDirectory()
        {
#if NET20 || NET35 || NET40 || NET452
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
#else
            return AppContext.BaseDirectory;
#endif
        }
    }
}
