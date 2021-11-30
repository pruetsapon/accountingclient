namespace ServiceClient
{
    public class AuthenticationSettings
    {
        public string RedisConnectionString { get; set; }
        public string OauthURL { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
    }
}
