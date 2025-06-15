using Microsoft.AspNetCore.Mvc;
using HuyenThoaiNSO.Data;
using HuyenThoaiNSO.Models;
using System.Security.Cryptography;
using System.Text;

namespace HuyenThoaiNSO.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                return Json(new { success = false, message = "Vui lòng nhập đầy đủ thông tin" });
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == Username);
            if (user == null)
            {
                return Json(new { success = false, message = "Tài khoản không tồn tại" });
            }

            if (!VerifyPassword(Password, user.Password))
            {
                return Json(new { success = false, message = "Mật khẩu không đúng" });
            }

            // Lưu thông tin đăng nhập vào session
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetInt32("UserId", user.Id);

            return Json(new { success = true });
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(string Username, string Password)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                return Json(new { success = false, message = "Vui lòng nhập đầy đủ thông tin" });
            }

            if (_context.Users.Any(u => u.Username == Username))
            {
                return Json(new { success = false, message = "Tên đăng nhập đã tồn tại" });
            }

            // Tạo user mới
            var user = new User
            {
                Username = Username,
                Password = HashPassword(Password),
                Balance = 0,
                created_at = DateTime.Now
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Json(new { success = true });
        }

        public IActionResult Logout()
        {
            // Xóa session
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            return HashPassword(inputPassword) == storedPassword;
        }
    }
} 