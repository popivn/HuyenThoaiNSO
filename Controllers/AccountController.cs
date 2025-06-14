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
                TempData["Error"] = "Vui lòng nhập đầy đủ thông tin";
                return RedirectToAction("Login");
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == Username);
            if (user == null)
            {
                TempData["Error"] = "Tài khoản không tồn tại";
                return RedirectToAction("Login");
            }

            if (!VerifyPassword(Password, user.Password))
            {
                TempData["Error"] = "Mật khẩu không đúng";
                return RedirectToAction("Login");
            }

            // Lưu thông tin đăng nhập vào session
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetInt32("UserId", user.Id);

            return RedirectToAction("Index", "Home");
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
                TempData["Error"] = "Vui lòng nhập đầy đủ thông tin";
                return RedirectToAction("Register");
            }

            if (_context.Users.Any(u => u.Username == Username))
            {
                TempData["Error"] = "Tên đăng nhập đã tồn tại";
                return RedirectToAction("Register");
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

            TempData["Success"] = "Đăng ký thành công! Vui lòng đăng nhập.";
            return RedirectToAction("Login");
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