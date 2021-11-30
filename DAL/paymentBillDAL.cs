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
    public class paymentBillDAL
    {
        private IMongoDatabase db;

        public paymentBillDAL()
        {
            var client = new MongoClient("mongodb+srv://HienTranOOAD:123123123@cluster0.guxtk.mongodb.net/PhotographyManagement?retryWrites=true&w=majority");
            this.db = client.GetDatabase("PhotographyManagement");
        }

        public bool InsertNewPaymentBillRecord(paymentBillDTO newPaymentBill)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("paymentBills");
                var newDoc = new BsonDocument
            {
                { "date" , newPaymentBill.date },
                {"totalMoney", newPaymentBill.totalMoney },
                {"accountantID", newPaymentBill.accountantID },
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
