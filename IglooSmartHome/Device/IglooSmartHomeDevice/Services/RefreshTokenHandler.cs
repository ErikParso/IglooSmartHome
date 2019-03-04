using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IglooSmartHomeDevice.Services
{
    public class RefreshTokenHandler : DelegatingHandler
    {
        public AuthenticationService AuthenticationService { get; set; }

        public RefreshTokenHandler() : base(new HttpClientHandler())
        {

        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode != HttpStatusCode.Unauthorized)
            {
                return response;
            }
            var newAccessToken = await AuthenticationService.RefreshTokenAsync();
            if (string.IsNullOrWhiteSpace(newAccessToken))
            {
                return response;
            }
            else
            {
                response.Dispose();
                request.Headers.Remove("X-ZUMO-AUTH");
                request.Headers.Add("X-ZUMO-AUTH", newAccessToken);
                return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
