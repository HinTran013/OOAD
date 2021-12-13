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
    public class photoDAL
    {
        private IMongoDatabase db;

        public photoDAL()
        {
            var client = new MongoClient("mongodb+srv://HienTranOOAD:123123123@cluster0.guxtk.mongodb.net/PhotographyManagement?retryWrites=true&w=majority");
            this.db = client.GetDatabase("PhotographyManagement");
        }

        public bool InsertNewIssueRecord(photoDTO newPhoto)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("photos");
                var newDoc = new BsonDocument
            {
                    { "invoiceID", newPhoto.invoiceID },
                    { "photoContent", newPhoto.photoContent }
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
