using Microsoft.AspNetCore.Mvc;
using HuyenThoaiNSO.Models;

namespace HuyenThoaiNSO.Controllers
{
    public class GiftCodeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Gift Code";
            return View();
        }
    }
} 