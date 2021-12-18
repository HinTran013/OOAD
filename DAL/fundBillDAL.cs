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
                    { "_idFromInvoice", newBill.objectIdFromInvoice},
                    { "date", newBill.date },
                    { "totalMoney", newBill.totalMoney },
                    { "staffUsername", newBill.staffUsername }
                };

                collection.InsertOneAsync(newDoc);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<fundBillDTO> GetAllFundBills()
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("fundBills");
                var fundBills = new List<fundBillDTO>();
                var list = collection.Find(new BsonDocument()).ToListAsync().Result;
                foreach (BsonDocument item in list)
                {
                    fundBills.Add(new fundBillDTO
                    (
                        (string)item["date"],
                        (double)item["totalMoney"],
                        (string)item["staffUsername"],
                        (ObjectId)item["_idFromInvoice"],
                        (ObjectId)item["_id"]
                    ));
                }
                return fundBills;
            }
            catch
            {
                return null;
            }
        }
    }
}
