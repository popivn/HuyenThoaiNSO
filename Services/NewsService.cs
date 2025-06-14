using System;
using System.Collections.Generic;
using System.Linq;
using HuyenThoaiNSO.Models;
using HuyenThoaiNSO.Extensions;

namespace HuyenThoaiNSO.Services
{
    public interface INewsService
    {
        List<News> GetLatestNews();
        List<News> GetPopularNews();
        List<News> GetTrendingNews();
        List<News> GetNewsByCategory(string category);
        News GetNewsById(int id);
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
                    CreatedAt = now,
                    TimeAgo = "2 giờ trước",
                    Category = "Tính năng",
                    CategoryColor = "primary"
                },
                new News
                {
                    Id = 2,
                    Title = "Sự kiện mùa hè 2024",
                    Content = "Tham gia ngay sự kiện mùa hè với nhiều phần thưởng hấp dẫn và hoạt động thú vị.",
                    ImageUrl = "/public/img/news/news1.png",
                    Author = "Admin",
                    CreatedAt = now.AddDays(-1),
                    TimeAgo = "1 ngày trước",
                    Category = "Sự kiện",
                    CategoryColor = "success"
                },
                new News
                {
                    Id = 3,
                    Title = "Hướng dẫn người chơi mới",
                    Content = "Bí quyết và mẹo chơi game cho người mới bắt đầu trong Huyền Thoại NSO.",
                    ImageUrl = "/public/img/news/news1.png",
                    Author = "Admin",
                    CreatedAt = now.AddDays(-2),
                    TimeAgo = "2 ngày trước",
                    Category = "Hot",
                    CategoryColor = "danger"
                },
                new News
                {
                    Id = 4,
                    Title = "Hướng dẫn người chơi mới",
                    Content = "Bí quyết và mẹo chơi game cho người mới bắt đầu trong Huyền Thoại NSO.",
                    ImageUrl = "/public/img/news/news1.png",
                    Author = "Admin",
                    CreatedAt = now.AddDays(-3),
                    TimeAgo = "3 ngày trước",
                    Category = "Hot",
                    CategoryColor = "danger"
                }
            };
        }

        public List<News> GetLatestNews()
        {
            return _news.OrderByDescending(n => n.CreatedAt)
                       .Select(n => new News
                       {
                           Id = n.Id,
                           Title = n.Title,
                           Content = n.Content,
                           ImageUrl = n.ImageUrl,
                           Author = n.Author,
                           CreatedAt = n.CreatedAt,
                           TimeAgo = n.CreatedAt.GetTimeAgo(),
                           Category = n.Category,
                           CategoryColor = n.CategoryColor
                       }).ToList();
        }

        public List<News> GetPopularNews()
        {
            return _news.Where(n => n.Category == "Sự kiện").ToList();
        }

        public List<News> GetTrendingNews()
        {
            return _news.Where(n => n.Category == "Hot").ToList();
        }

        public List<News> GetNewsByCategory(string category)
        {
            return _news.Where(n => n.Category == category).ToList();
        }

        public News GetNewsById(int id)
        {
            return _news.FirstOrDefault(n => n.Id == id);
        }
    }
}