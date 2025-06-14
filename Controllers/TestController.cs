using Microsoft.AspNetCore.Mvc;
using HuyenThoaiNSO.Data;
using Microsoft.EntityFrameworkCore;

namespace HuyenThoaiNSO.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Thử kết nối đến database
                bool canConnect = await _context.Database.CanConnectAsync();
                
                if (canConnect)
                {
                    return Content("Kết nối database thành công!");
                }
                else
                {
                    return Content("Không thể kết nối đến database.");
                }
            }
            catch (Exception ex)
            {
                return Content($"Lỗi kết nối database: {ex.Message}");
            }
        }
    }
} 