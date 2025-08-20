using Adapters.ListingApiClient.Configuration;
using Adapters.ListingApiClient.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Adapters.ListingApiClient.Tests.Integration;

public class TestBase
{
    private readonly IServiceProvider _serviceProvider;

    protected TestBase()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        var settings = configuration.GetSection("ListingApiClientAdapterSettings").Get<ListingApiClientAdapterSettings>()!;

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging(b => b.AddConsole());

        serviceCollection.AddListingApiClientAdapter(settings);

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    protected T GetService<T>() where T : notnull => _serviceProvider.GetRequiredService<T>();
}
