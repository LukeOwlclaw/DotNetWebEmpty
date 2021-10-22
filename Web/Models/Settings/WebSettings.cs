using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Settings
{
    public class WebSettings
    {
        public const string ErrorMessageDefaultValue = "\"{0}\" should be set via default value if not set in '" + Constants.Config.AppSettingsFilePath + "'";
        public const string ErrorMessageRequiredValue = "Please define \"{0}\" in '" + Constants.Config.AppSettingsFilePath + "'";

        /// <summary>
        /// Connection details for accessing database for web application
        /// </summary>
        [Required(ErrorMessage = ErrorMessageRequiredValue)]
        public string DatabaseFile { get; set; } = null!;
    }
}
