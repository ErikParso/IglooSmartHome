using Autofac;
using Autofac.Integration.SignalR;
using Autofac.Integration.WebApi;
using Azure.Server.Utils.Email;
using IglooSmartHome.AppStart;
using IglooSmartHome.Controllers;
using IglooSmartHome.DataObjects;
using IglooSmartHome.Models;
using IglooSmartHome.Services;
using IglooSmartHomeService.Controllers;
using IglooSmartHomeService.Services;
using IglooSmartHomeService.SignalR;
using Microsoft.AspNet.SignalR;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Owin;
using Owin;
using System.Configuration;
using System.Reflection;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]

namespace IglooSmartHome.AppStart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = GetInjectionContainer();
            GlobalHost.DependencyResolver = new AutofacDependencyResolver(container);

            var config = new HttpConfiguration();
            ConfigureSwagger(config);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
            app.MapSignalR("/signalr", new HubConfiguration() { EnableDetailedErrors = true });

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);
            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                // This middleware is intended to be used locally for debugging. By default, HostName will
                // only have a value when running in an App Service application.
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }
        }

        private static IContainer GetInjectionContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterHubs(Assembly.GetExecutingAssembly());

            builder.RegisterType<SendgridService>()
                .As<IEmailService<Account>>();
            builder.RegisterType<DeviceConnectionsMappingService>()
                .SingleInstance();
            builder.RegisterType<IglooSmartHomeContext>();

            return builder.Build();
        }
    }
}