using DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class servicesDAL
    {
        private IMongoDatabase db;

        public servicesDAL()
        {
            var client = new MongoClient("mongodb+srv://HienTranOOAD:123123123@cluster0.guxtk.mongodb.net/PhotographyManagement?retryWrites=true&w=majority");
            this.db = client.GetDatabase("PhotographyManagement");
        }

        public bool InserNewService(string newName, double newPrice, string newDescription)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("services");
                var newDoc = new BsonDocument
            {
                    { "serviceName", newName },
                    { "servicePrice", newPrice },
                    { "serviceDescription", newDescription }
            };

                collection.InsertOneAsync(newDoc);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteOneServiceByID(ObjectId id)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("services");
                var filter = new BsonDocument
                {
                    { "_id", id }
                };
                collection.DeleteOne(filter);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<servicesDTO> GetAllServices()
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("services");
                var servicesDoc = collection.Find(new BsonDocument()).ToListAsync().Result;
                List<servicesDTO> services = new List<servicesDTO>();
                foreach(BsonDocument item in servicesDoc)
                {
                    services.Add( new servicesDTO(
                        (ObjectId)item["_id"],
                        (string)item["serviceName"],
                        (double)item["servicePrice"],
                        (string)item["serviceDescription"]));
                    
                }
                
                return services;
            }
            catch
            {
                return null;
            }
        }

        public double GetPriceOfServiceType(string type)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("services");
                var servicesDoc = collection.Find(x => (string)x["serviceName"] == type).SingleAsync().Result;
                
                return servicesDoc["servicePrice"].ToDouble();
            }
            catch
            {
                return 0;
            }
        }

        public bool ReplaceOneService(servicesDTO newService)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("services");
                var filter = new BsonDocument
            {
                { "_id", newService.serviceID }
            };
                var update = new BsonDocument {
                { "serviceName", newService.name},
                { "servicePrice", newService.price},
                { "serviceDescription", newService.description}
            };
                collection.ReplaceOne(filter, update);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateServicePriceByName(string name, double newPrice)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("services");

                var filter = Builders<BsonDocument>.Filter.Eq("serviceName", name);
                var update = Builders<BsonDocument>.Update.Set("servicePrice", newPrice);
                collection.UpdateOne(filter, update);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
