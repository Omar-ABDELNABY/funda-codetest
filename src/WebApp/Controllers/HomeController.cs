using System.Diagnostics;
using Domain.UseCases;
using Microsoft.AspNetCore.Mvc;
using funda_codetest_webapp.Models;

namespace funda_codetest_webapp.Controllers;

public class HomeController : Controller
{
    private readonly IGetBrokersWithMostListingsUseCase _getBrokersWithMostListingsUseCase;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IGetBrokersWithMostListingsUseCase getBrokersWithMostListingsUseCase, ILogger<HomeController> logger)
    {
        _getBrokersWithMostListingsUseCase = getBrokersWithMostListingsUseCase;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }

    [HttpGet("/api/brokers/top")]
    public async Task<IActionResult> TopBrokersApi([FromQuery] bool withGarden = false)
    {
        var brokers = await _getBrokersWithMostListingsUseCase.GetBrokersWithMostListingsInAmsterdam(withGarden);
        var viewModel = brokers
            .Select(MakelaarListingCountViewModel.FromDomain)
            .ToList();
        return Ok(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
