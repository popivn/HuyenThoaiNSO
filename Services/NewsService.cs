using System;
using System.Collections.Generic;
using System.Linq;
using HuyenThoaiNSO.Models;
using HuyenThoaiNSO.Extensions;

namespace HuyenThoaiNSO.Services
{
    public interface INewsService
    {
        IEnumerable<News> GetLatestNews();
    }

    public class NewsService : INewsService
    {
        private readonly List<News> _news;

        public NewsService()
        {
            // Tạo timestamp cố định cho dữ liệu mẫu
            var now = DateTime.Now;
            _news = new List<News>
            {
                new News
                {
                    Id = 1,
                    Title = "Cập nhật phiên bản mới 2.0",
                    Content = "Khám phá những tính năng mới thú vị trong phiên bản 2.0 của Huyền Thoại NSO.",
                    ImageUrl = "/public/img/news/news1.png",
                    Author = "Admin",
                    PublishDate = new DateTime(2025, 3, 20, 10, 30, 0) // 20/03/2024 10:30:00
                },
                new News
                {
                    Id = 2,
                    Title = "Sự kiện mùa hè 2024",
                    Content = "Tham gia ngay sự kiện mùa hè với nhiều phần thưởng hấp dẫn và hoạt động thú vị.",
                    ImageUrl = "/public/img/news/news1.png",
                    Author = "Admin",
                    PublishDate = new DateTime(2024, 3, 19, 15, 45, 0) // 19/03/2024 15:45:00
                },
                new News
                {
                    Id = 3,
                    Title = "Hướng dẫn người chơi mới",
                    Content = "Bí quyết và mẹo chơi game cho người mới bắt đầu trong Huyền Thoại NSO.",
                    ImageUrl = "/public/img/news/news1.png",
                    Author = "Admin",
                    PublishDate = new DateTime(2024, 3, 18, 9, 15, 0) // 18/03/2024 09:15:00
                },
                new News
                {
                    Id = 4,
                    Title = "Hướng dẫn người chơi mới",
                    Content = "Bí quyết và mẹo chơi game cho người mới bắt đầu trong Huyền Thoại NSO.",
                    ImageUrl = "/public/img/news/news1.png",
                    Author = "Admin",
                    PublishDate = new DateTime(2024, 3, 18, 9, 15, 0) // 18/03/2024 09:15:00
                }
            };
        }

        public IEnumerable<News> GetLatestNews()
        {
            return _news.OrderByDescending(n => n.PublishDate)
                       .Select(n => new News
                       {
                           Id = n.Id,
                           Title = n.Title,
                           Content = n.Content,
                           ImageUrl = n.ImageUrl,
                           Author = n.Author,
                           PublishDate = n.PublishDate,
                           TimeAgo = n.PublishDate.GetTimeAgo() // Tính toán TimeAgo dựa trên timestamp thực
                       });
        }
    }
} 