using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Database.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Web.Constants;

namespace Web
{
    public static class Program
    {
        internal static Exception? StartupException { get; set; }

        private const bool DoMigrate = false;
        public static void Main(string[] args)
        {
            try
            {
                CheckConfigurationFiles();

                var host = CreateHostBuilder(args).Build();

                if (DoMigrate && DateTime.Now.Ticks != 0)
                {
                    MigrateDatabase(host);
                }

                host.Run();
            }
            catch (Exception ex)
            {
                StartupException = ex;
                ExceptionCreateHostBuilder().Build().Run();
            }
        }

        private static void CheckConfigurationFiles()
        {
            if (!Directory.Exists(Config.DataAndLogsFolder))
            {
                Directory.CreateDirectory(Config.DataAndLogsFolder);
            }

            if (!File.Exists(Config.AppSettingsFilePath))
            {
                throw new Exception($"Please create settings file {Path.GetFullPath(Config.AppSettingsFilePath)} from {Config.AppSettingsTemplateFile}.");
            }

            if (!File.Exists(Config.TinyMceEditorConfigFilePath))
            {
                if (!File.Exists(Config.TinyMceEditorConfigFileSourcePath))
                {
                    throw new Exception($"Editor configuration file {Config.TinyMceEditorConfigFileSourcePath} does not exist. Please re-deploy.");
                }
                else
                {
                    File.Move(Config.TinyMceEditorConfigFileSourcePath, Config.TinyMceEditorConfigFilePath);
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    var provider = new PhysicalFileProvider(Path.GetFullPath(Config.DataAndLogsFolder));
                    config.AddJsonFile(provider, Config.AppSettingsFile, optional: false, reloadOnChange: false);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }

        public static IHostBuilder ExceptionCreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<ExceptionStartup>();
                });
        }

        private static void MigrateDatabase(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<WebDbContext>();

            var assemblyMigrations = context.Database.GetMigrations();
            var appliedMigrations = context.Database.GetAppliedMigrations();
            var createDatabase = !appliedMigrations.Any();
            var pendingMigrations = context.Database.GetPendingMigrations();

            var unknownMigrations = appliedMigrations.Except(assemblyMigrations);
            if (unknownMigrations.Any())
            {
                SharedHelpers.Debug.Break();

                throw new Exception($"Application version too old. " +
                    $"Unknown applied migration(s):{string.Join(",", unknownMigrations)}. " +
                    $"Last assembly migration:{assemblyMigrations.LastOrDefault() ?? "not found"}.");
            }
            else if (pendingMigrations.Any())
            {
                context.Database.Migrate();
                if (createDatabase)
                {
                    //initializing custom roles
                    var firstSample = new Database.Models.BO.Sample("DB INIT");
                    context.Sample.Add(firstSample);
                }
            }
        }
    }
}
