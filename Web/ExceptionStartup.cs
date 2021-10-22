using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Database.DataAccess;
using Database.Models.BO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.Constants;

namespace Web
{
    /// <summary>
    /// Fallback Startup used when primary Startup caused exception during initialization
    /// </summary>
    public class ExceptionStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Minimal configuration
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (Program.StartupException == null) { throw new InvalidOperationException(nameof(Program.StartupException)); }

            var startupLogFile = Constants.Config.StartupExceptionLogFilePath;

            var errorMessage = $"Configuration invalid. Please check {startupLogFile}.";

            var sb = new StringBuilder();
            sb.AppendLine($"{Names.AppName} failed to start at UTC {DateTime.UtcNow} ({DateTime.Now})");
            sb.AppendLine(Program.StartupException.ToString());
            sb.AppendLine("=======================");
            var errorDetails = sb.ToString();

            if (env.IsDevelopment())
            {
                errorMessage = errorDetails;
            }

            try
            {
                File.AppendAllText(startupLogFile, errorDetails);
            }
            catch (Exception ex)
            {
                errorMessage = $"Configuration invalid and failed to write {startupLogFile}. Please check permissions." +
                    $"{Environment.NewLine}{Environment.NewLine}{ex}" +
                    $"{Environment.NewLine}{Environment.NewLine}{errorDetails}";
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.Map(Names.ExceptionViewUrl, async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText(startupLogFile)).ConfigureAwait(false);
                });
                endpoints.MapFallback(async context =>
                {
                    await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
                });
            });
        }
    }
}
