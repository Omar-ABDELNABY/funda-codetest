using Adapters.ListingApiClient.Configuration;
using Adapters.ListingApiClient.Constants;
using Adapters.ListingApiClient.DelegatingHandlers;
using Domain.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace Adapters.ListingApiClient.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddListingApiClientAdapter(this IServiceCollection services, ListingApiClientAdapterSettings settings)
    {
        services.AddSingleton(settings);

        services.AddTransient<ThrottlingHandler>();
        services.AddTransient<ErrorLogHandler>();

        var uri = $"{settings.BaseAddress}{settings.ApiKey}";
        services.AddHttpClient(ClientConstants.ListingApiClientName, client =>
            {
                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddHttpMessageHandler<ThrottlingHandler>()
            .AddHttpMessageHandler<ErrorLogHandler>();

        services.AddScoped<IListingSearchPort, Clients.ListingApiClient>();

        return services;
    }
}
