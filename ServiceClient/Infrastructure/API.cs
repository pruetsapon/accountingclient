namespace ServiceClient.Infrastructure
{
    public static class API
    {
        public static class Authentication
        {
            public static string GetTokenAsync(string baseUri)
            {
                return $"{baseUri}token";
            }
        }
    }
}
