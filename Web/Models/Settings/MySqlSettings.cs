using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Settings
{
    public class MySqlSettings
    {
        [Required(ErrorMessage = WebSettings.ErrorMessageRequiredValue)]
        public string Server { get; set; } = null!;

        [Required(ErrorMessage = WebSettings.ErrorMessageRequiredValue)]
        public uint Port { get; set; }

        [Required(ErrorMessage = WebSettings.ErrorMessageRequiredValue)]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = WebSettings.ErrorMessageRequiredValue)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = WebSettings.ErrorMessageRequiredValue)]
        public string Database { get; set; } = null!;
    }
}
