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
    public class billDetailDAL
    {
        private IMongoDatabase db;

        public billDetailDAL()
        {
            var client = new MongoClient("mongodb+srv://HienTranOOAD:123123123@cluster0.guxtk.mongodb.net/PhotographyManagement?retryWrites=true&w=majority");
            this.db = client.GetDatabase("PhotographyManagement");
        }

        public bool InserNewBillDetailRecord(billDetailDTO newBillDetail)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("billDetails");
                var newDoc = new BsonDocument
            {
                { "billID" , newBillDetail.billID },
                {"photoType", newBillDetail.photoType },
                {"unitPrice", newBillDetail.unitPrice },
                {"unitQuantity", newBillDetail.unitQuantity }
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
