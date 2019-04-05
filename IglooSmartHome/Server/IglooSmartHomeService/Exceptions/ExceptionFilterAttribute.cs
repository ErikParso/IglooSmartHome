using IglooSmartHomeService.Exceptions;
using System.Net.Http;

namespace IglooSmartHomeService.ExceptionFilters
{
    public class ExceptionFilterAttribute : System.Web.Http.Filters.ExceptionFilterAttribute
    {
        public override void OnException(System.Web.Http.Filters.HttpActionExecutedContext context)
        {
            var filteredException = context.Exception as FilteredException;
            if (filteredException != null)
            {
                context.Response = new HttpResponseMessage(filteredException.StatusCode)
                {
                    ReasonPhrase = filteredException.Message,
                    Content = new StringContent(filteredException.Message),
                };
                context.Response.Headers.Add("filtered-exception", context.Exception.GetType().Name);
            }
        }
    }
}