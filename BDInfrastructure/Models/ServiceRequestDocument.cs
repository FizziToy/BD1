using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace BDInfrastructure.Models
{
    public class ServiceRequestDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int RequestId { get; set; }
        public int UnitId { get; set; }
        public int TenantId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<string> History { get; set; }
    }
}