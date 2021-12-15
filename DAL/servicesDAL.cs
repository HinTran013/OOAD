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
                        (Int32)item["servicePrice"],
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
    }
}
