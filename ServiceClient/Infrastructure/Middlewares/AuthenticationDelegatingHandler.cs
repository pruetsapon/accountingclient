using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using ServiceClient.Infrastructure.Repositories;

namespace ServiceClient.Infrastructure.Middlewares
{
    public class AuthenticationDelegatingHandler : DelegatingHandler
    {
        private readonly IRedisRepository _repository;
        public AuthenticationDelegatingHandler(IRedisRepository repository)
        {
            _repository = repository;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _repository.GetTokenAsync();
            request.Headers.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
            {
                token = await _repository.RefreshTokenAsync();
                request.Headers.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);
                response = await base.SendAsync(request, cancellationToken);
            }
            return response;
        }
    }
}
