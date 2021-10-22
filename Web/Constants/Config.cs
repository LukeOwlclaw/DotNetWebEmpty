using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Constants
{
    public static class Config
    {
        /// <summary>
        /// Relative path to folder where configuration data and logs are stored.
        /// </summary>
        public const string DataAndLogsFolder = "..\\httpdocs_data\\";

        /// <summary>
        /// Path to fallback log file used when exception occurred during initialization.
        /// </summary>
        public const string StartupExceptionLogFilePath = DataAndLogsFolder + "startupException.log";

        /// <summary>
        /// Configuration file for web application.
        /// </summary>
        public const string AppSettingsFile = "appsettings.json";

        /// <summary>
        /// Path to configuration file for web application.
        /// </summary>
        public const string AppSettingsFilePath = DataAndLogsFolder + AppSettingsFile;

        /// <summary>
        /// Template for configuration file for web application.
        /// </summary>
        public const string AppSettingsTemplateFile = "appsettings-template.json";

        /// <summary>
        /// Path to configuration file for Tiny MCE Editor.
        /// </summary>
        public const string TinyMceEditorConfigFilePath = DataAndLogsFolder + "editorconfig.json";

        /// <summary>
        /// Path to original configuration file for Tiny MCE Editor after deploy.
        /// </summary>
        public const string TinyMceEditorConfigFileSourcePath = "editorconfig.json";
    }
}
