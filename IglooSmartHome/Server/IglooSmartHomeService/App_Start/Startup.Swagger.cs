using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Swagger;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System.Web.Http;
using System.Web.Http.Description;

namespace IglooSmartHome.App_Start
{
    public partial class Startup
    {
        public static void ConfigureSwagger(HttpConfiguration config)
        {
            config.Services.Replace(typeof(IApiExplorer), new MobileAppApiExplorer(config));
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "myService");
                c.OperationFilter<MobileAppHeaderFilter>();
                c.SchemaFilter<MobileAppSchemaFilter>();
                c.AppServiceAuthentication("https://igloosmarthome.azurewebsites.net/", "google");
                c.OperationFilter<AddAuthorizationHeaderParameterOperationFilter>();
            }).EnableSwaggerUi(c =>
            {
                c.EnableOAuth2Support("na", "na", "na");
            });
        }
    }

    public class AddAuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters != null)
            {
                operation.parameters.Add(new Parameter
                {
                    name = "X-ZUMO-AUTH",
                    @in = "header",
                    description = "access token",
                    required = false,
                    type = "string"
                });
            }
        }
    }
}