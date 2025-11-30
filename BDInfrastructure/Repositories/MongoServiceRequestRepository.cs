using BDInfrastructure.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BDInfrastructure.Repositories
{
    public class MongoServiceRequestRepository
    {
        private readonly IMongoCollection<ServiceRequestDocument> _collection;

        public MongoServiceRequestRepository(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDb:ConnectionString"]);
            var db = client.GetDatabase(config["MongoDb:Database"]);
            _collection = db.GetCollection<ServiceRequestDocument>(config["MongoDb:Collection"]);
        }

        public IEnumerable<ServiceRequestDocument> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public void Insert(ServiceRequestDocument doc)
        {
            _collection.InsertOne(doc);
        }
    }
}