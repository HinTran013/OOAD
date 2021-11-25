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
    public class fundBillDAL
    {
        private IMongoDatabase db;

        public fundBillDAL()
        {
            var client = new MongoClient("mongodb+srv://HienTranOOAD:123123123@cluster0.guxtk.mongodb.net/PhotographyManagement?retryWrites=true&w=majority");
            this.db = client.GetDatabase("PhotographyManagement");
        }

        public bool InserNewFundBillRecord(fundBillDTO newBill)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("fundBills");
                var newDoc = new BsonDocument
            {
                    { "description", newBill.description },
                    { "date", newBill.date },
                    { "totalMoney", newBill.totalMoney },
                    { "accountantID", newBill.accountantID }
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
