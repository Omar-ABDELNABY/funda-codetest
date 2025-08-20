using Adapters.ListingApiClient.Configuration;
using Adapters.ListingApiClient.Extensions;
using Domain.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true)
    .Build();

var settings = configuration.GetSection("ListingApiClientAdapterSettings").Get<ListingApiClientAdapterSettings>()!;

builder.Services.AddLogging(b => b.AddConsole());

builder.Services.AddListingApiClientAdapter(settings);

builder.Services.AddScoped<IGetBrokersWithMostListingsUseCase, GetBrokersWithMostListingsUseCase>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
