using Microsoft.AspNetCore.Mvc;
using HuyenThoaiNSO.Models;
using HuyenThoaiNSO.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HuyenThoaiNSO.Controllers
{
    public class RankingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RankingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string type = "players")
        {
            ViewData["Title"] = GetTitleForType(type);
            ViewData["ActiveTab"] = type;
            return View();
        }

        [Route("api/[controller]")]
        [ApiController]
        public class RankingApiController : ControllerBase
        {
            private readonly ApplicationDbContext _context;

            public RankingApiController(ApplicationDbContext context)
            {
                _context = context;
            }

            [HttpGet("{type}")]
            public async Task<IActionResult> GetRanking(string type, [FromQuery] string period = "all", [FromQuery] int page = 1)
            {
                var pageSize = 10;
                var query = _context.Users.AsQueryable();

                // Lọc theo thời gian
                switch (period.ToLower())
                {
                    case "day":
                        query = query.Where(u => u.created_at >= DateTime.Today);
                        break;
                    case "week":
                        query = query.Where(u => u.created_at >= DateTime.Today.AddDays(-7));
                        break;
                    case "month":
                        query = query.Where(u => u.created_at >= DateTime.Today.AddMonths(-1));
                        break;
                }

                // Sắp xếp và phân trang
                var totalItems = await query.CountAsync();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
                var items = await query
                    .OrderByDescending(u => u.Balance)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                // Tạo model cho bảng xếp hạng
                var model = new RankingTableModel
                {
                    Id = type,
                    Title = GetTitleForType(type),
                    Icon = GetIconForType(type),
                    SelectedPeriod = period,
                    Columns = GetColumnsForType(type),
                    Items = items.Select((u, index) => new RankingItem
                    {
                        Rank = (page - 1) * pageSize + index + 1
                    }.Also(item =>
                    {
                        item.SetValue("username", u.Username);
                        item.SetValue("level", "1"); // Thay bằng level thực tế
                        item.SetValue("balance", u.Balance.ToString("N0"));
                    })).ToList(),
                    CurrentPage = page,
                    TotalPages = totalPages
                };

                return Ok(model);
            }
        }

        private static string GetTitleForType(string type)
        {
            return type switch
            {
                "players" => "Top Cao Thủ",
                "guilds" => "Top Gia Tộc",
                "classes" => "Top Class",
                "wealth" => "Top Tài Phú",
                _ => "Bảng xếp hạng"
            };
        }

        private static string GetIconForType(string type)
        {
            return type switch
            {
                "players" => "bi-person-fill",
                "guilds" => "bi-people-fill",
                "classes" => "bi-hdd-network-fill",
                "wealth" => "bi-bar-chart-fill",
                _ => "bi-trophy-fill"
            };
        }

        private static List<RankingColumn> GetColumnsForType(string type)
        {
            return type switch
            {
                "players" => new List<RankingColumn>
                {
                    new RankingColumn { Key = "username", Title = "Tên người chơi", Type = "text" },
                    new RankingColumn { Key = "level", Title = "Cấp độ", Type = "text" },
                    new RankingColumn { Key = "balance", Title = "Tài sản", Type = "text" }
                },
                "guilds" => new List<RankingColumn>
                {
                    new RankingColumn { Key = "name", Title = "Tên gia tộc", Type = "text" },
                    new RankingColumn { Key = "members", Title = "Số thành viên", Type = "text" },
                    new RankingColumn { Key = "level", Title = "Cấp độ", Type = "text" }
                },
                "classes" => new List<RankingColumn>
                {
                    new RankingColumn { Key = "name", Title = "Tên class", Type = "text" },
                    new RankingColumn { Key = "players", Title = "Số người chơi", Type = "text" },
                    new RankingColumn { Key = "winRate", Title = "Tỷ lệ thắng", Type = "text" }
                },
                "wealth" => new List<RankingColumn>
                {
                    new RankingColumn { Key = "username", Title = "Tên người chơi", Type = "text" },
                    new RankingColumn { Key = "balance", Title = "Tài sản", Type = "text" },
                    new RankingColumn { Key = "rank", Title = "Xếp hạng", Type = "text" }
                },
                _ => new List<RankingColumn>
                {
                    new RankingColumn { Key = "username", Title = "Tên người chơi", Type = "text" },
                    new RankingColumn { Key = "level", Title = "Cấp độ", Type = "text" },
                    new RankingColumn { Key = "balance", Title = "Tài sản", Type = "text" }
                }
            };
        }
    }

    public static class Extensions
    {
        public static T Also<T>(this T self, Action<T> action)
        {
            action(self);
            return self;
        }
    }
} 