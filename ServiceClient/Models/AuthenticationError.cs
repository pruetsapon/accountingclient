using Newtonsoft.Json;

namespace ServiceClient.Models
{
    public class AuthenticationError
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
