using Microsoft.AspNetCore.Mvc;

namespace HuyenThoaiNSO.Controllers
{
    public class DownloadController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Tải Game";
            return View();
        }
    }
} 