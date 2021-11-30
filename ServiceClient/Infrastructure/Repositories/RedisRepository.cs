using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceClient.Models;
using ServiceClient.Services;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace ServiceClient.Infrastructure.Repositories
{
    public class RedisRepository : IRedisRepository
    {
        private readonly ILogger<RedisRepository> _logger;
        private readonly ConnectionMultiplexer _redis;
        private readonly IAuthenticationService _authenticationService;
        private readonly IDatabase _database;

        public RedisRepository(ILoggerFactory loggerFactory, ConnectionMultiplexer redis, IAuthenticationService authenticationService)
        {
            _logger = loggerFactory.CreateLogger<RedisRepository>();
            _redis = redis;
            _authenticationService = authenticationService;
            _database = redis.GetDatabase();
        }

        public async Task<AuthenticationResponse> GetTokenAsync()
        {
            var keys = "token";
            var token = await _database.StringGetAsync(keys);
            if (token.IsNullOrEmpty)
            {
                var newToken = await _authenticationService.GetTokenAsync();
                await _database.StringSetAsync(keys, JsonConvert.SerializeObject(newToken));
                return newToken;
            }
            else
            {
                return JsonConvert.DeserializeObject<AuthenticationResponse>(token);
            }
        }

        public async Task<AuthenticationResponse> RefreshTokenAsync()
        {
            var keys = "token";
            var newToken = await _authenticationService.GetTokenAsync();
            await _database.StringSetAsync(keys, JsonConvert.SerializeObject(newToken));
            return newToken;
        }
    }
}
