using System;
using System.Text.Json.Serialization;

namespace BDInfrastructure.Models
{
    public class SqlServiceRequestHistoryItem
    {
        [JsonPropertyName("ChangeDate")]
        public DateTime ChangeDate { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }
    }

    public class SqlServiceRequestResult
    {
        public int RequestId { get; set; }
        public int UnitId { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public string HistoryJson { get; set; }
    }
}