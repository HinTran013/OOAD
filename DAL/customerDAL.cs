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
    public class customerDAL
    {
        private IMongoDatabase db;

        public customerDAL()
        {
            var client = new MongoClient("mongodb+srv://HienTranOOAD:123123123@cluster0.guxtk.mongodb.net/PhotographyManagement?retryWrites=true&w=majority");
            this.db = client.GetDatabase("PhotographyManagement");
        }

        public bool InserNewCustomerRecord(customerDTO newCustomer)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("customers");
                var newDoc = new BsonDocument
            {
                { "name" , newCustomer.name },
                {"address", newCustomer.address },
                {"phoneNumber", newCustomer.phoneNumber },
                {"email", newCustomer.email }
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
