using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HuyenThoaiNSO.Models;
using HuyenThoaiNSO.Services;

namespace HuyenThoaiNSO.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly INewsService _newsService;

    public HomeController(ILogger<HomeController> logger, INewsService newsService)
    {
        _logger = logger;
        _newsService = newsService;
    }

    public IActionResult Index()
    {
        var latestNews = _newsService.GetLatestNews();
        return View(latestNews);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
