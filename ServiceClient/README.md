# How to use In Startup.cs

## AuthenticationService
    - services.AddHttpClient<IAuthenticationService, AuthenticationService>();
    - services.AddHttpClient<IMasterService, MasterService>()
                .AddHttpMessageHandler<AuthenticationDelegatingHandler>();

## RedisRepository
    -services.AddSingleton<ConnectionMultiplexer>(sp =>
    {
        var settings = sp.GetRequiredService<IOptions<AuthenticationSettings>>().Value;
        var configuration = ConfigurationOptions.Parse(settings.RedisConnectionString, true);
        configuration.ResolveDns = true;
        return ConnectionMultiplexer.Connect(configuration);
    });
    - services.AddTransient<IRedisRepository, RedisRepository>();