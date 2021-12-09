using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DTO;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DAL
{
    public class reportPricesDetailDAL
    {
        private IMongoDatabase db;

        public reportPricesDetailDAL()
        {
            var client = new MongoClient("mongodb+srv://HienTranOOAD:123123123@cluster0.guxtk.mongodb.net/PhotographyManagement?retryWrites=true&w=majority");
            this.db = client.GetDatabase("PhotographyManagement");
        }

        public bool InsertNewPriceRecord(reportPricesDetailDTO detail)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("reportPricesDetail");
                var newDoc = new BsonDocument
            {
                    { "reportPricesID", detail.reportPricesID},
                    { "photoType", detail.photoType },
                    { "oldPrice", detail.oldPrice },
                    { "newPrice", detail.newPrice }
            };

                collection.InsertOneAsync(newDoc);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
