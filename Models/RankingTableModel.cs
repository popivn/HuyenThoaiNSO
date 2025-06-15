using System.Collections.Generic;

namespace HuyenThoaiNSO.Models
{
    public class RankingTableModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public List<string> TimePeriods { get; set; } = new List<string> { "Ngày", "Tuần", "Tháng", "Tất cả" };
        public string SelectedPeriod { get; set; } = "Tất cả";
        public List<RankingColumn> Columns { get; set; }
        public List<RankingItem> Items { get; set; }
        public bool ShowPagination { get; set; } = true;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
    }

    public class RankingColumn
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public string Type { get; set; } = "text"; // text, image, badge
    }

    public class RankingItem
    {
        public int Rank { get; set; }
        private Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();

        public void SetValue(string key, string value)
        {
            Values[key] = value;
        }

        public string GetValue(string key)
        {
            return Values.ContainsKey(key) ? Values[key] : string.Empty;
        }
    }
} 