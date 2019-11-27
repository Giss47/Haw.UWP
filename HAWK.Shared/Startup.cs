using HAWK.Shared.Services.AppConfigService;
using HAWK.Shared.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace HAWK.Shared
{
    /// <summary>
    /// Dependency Injection Operations Using Microsoft library Extentions.Host
    /// </summary>
    public class Startup
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static void Init()
        {
            ServiceProvider = new HostBuilder()
                .ConfigureServices(ConfigureServices)
                .Build().Services;
        }

        private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            services.AddTransient<MainViewModel>();
            services.AddTransient<IAppDataDirectoryService, AppDataDirectoryService>();
            services.AddTransient<ILocalFileService, LocalFileService>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<OrganizationCredViewModel>();
        }
    }
}