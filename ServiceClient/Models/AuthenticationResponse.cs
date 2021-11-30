using Newtonsoft.Json;

namespace ServiceClient.Models
{
    public class AuthenticationResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public string ExpiresInSeconds { get; set; }
    }
}
