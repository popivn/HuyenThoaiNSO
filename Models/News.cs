using System;
using System.ComponentModel.DataAnnotations;

namespace HuyenThoaiNSO.Models
{
    public class News
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string ImageUrl { get; set; }
        public required string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string TimeAgo { get; set; }
        public required string Category { get; set; }
        public required string CategoryColor { get; set; }
    }
} 