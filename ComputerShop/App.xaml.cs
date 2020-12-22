using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Dashboard.Data.EF;
using Dashboard.Data.Entities;
using DesignLogin;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dashboard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            //Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<Controller>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<CompuerShopDbContext>(cs => cs.UseSqlServer(
            //    "Server=MYPC;Database=ComputerShopManager;Trusted_Connection=True;")
            //);
            services.AddDbContext<CompuerShopDbContext>(cs => cs.UseSqlServer(
                "Server=ANOME-PC\\SQLEXPRESS;Database=ComputerShopManager;Trusted_Connection=true;")
            );


            //services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
            //services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
            //services.AddTransient<IUserService, UserService>();
            //services.AddTransient(typeof(LoginWindow));
            //(options => options.UseSqlServer( 
            //    Configuration.GetConnectionString("ShopManagerDatabase")));

            services.AddTransient(typeof(Controller));
        }
    }
}
