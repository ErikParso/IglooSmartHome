using Autofac;
using Autofac.Integration.WebApi;
using Azure.Server.Utils.Email;
using IglooSmartHome.App_Start;
using IglooSmartHome.Controllers;
using IglooSmartHome.Models;
using IglooSmartHome.Services;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Owin;
using Owin;
using System.Configuration;
using System.Reflection;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]

namespace IglooSmartHome.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = GetInjectionContainer();

            var config = new HttpConfiguration() { DependencyResolver = new AutofacWebApiDependencyResolver(container) };
            config.Formatters.JsonFormatter
                .SerializerSettings
                .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.EnableSystemDiagnosticsTracing();
            ConfigureSwagger(config);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);

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
            builder.RegisterType<IglooSmartHomeContext>();
            builder.RegisterType<ValuesController>();
            builder.RegisterType<CustomRegistrationController>();
            builder.RegisterType<CustomLoginController>();
            builder.RegisterType<AccountsController>();
            builder.RegisterType<SendgridService>().As<IEmailService>();
            return builder.Build();
        }
    }
}