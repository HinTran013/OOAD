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
    public class accountDAL
    {
        private IMongoDatabase db;

        public accountDAL()
        {
            var client = new MongoClient("mongodb+srv://HienTranOOAD:123123123@cluster0.guxtk.mongodb.net/PhotographyManagement?retryWrites=true&w=majority");
            this.db = client.GetDatabase("PhotographyManagement");
        }

        public bool InsertNewAccountRecord(accountDTO newAccount)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("accounts");
                var newDoc = new BsonDocument
            {
                { "staffID" , newAccount.staffID },
                {"userName", newAccount.userName },
                {"password", newAccount.password },
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
