using Microsoft.EntityFrameworkCore;
using HuyenThoaiNSO.Models;

namespace HuyenThoaiNSO.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        // Thêm các DbSet properties cho các entity của bạn ở đây
        // Ví dụ: public DbSet<User> Users { get; set; }
    }
} 