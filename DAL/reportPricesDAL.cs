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
    public class reportPricesDAL
    {
        private IMongoDatabase db;

        public reportPricesDAL()
        {
            var client = new MongoClient("mongodb+srv://HienTranOOAD:123123123@cluster0.guxtk.mongodb.net/PhotographyManagement?retryWrites=true&w=majority");
            this.db = client.GetDatabase("PhotographyManagement");
        }

        public bool InsertNewReportPricesRecord(reportPricesDTO form)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("reportPrices");
                var newDoc = new BsonDocument
            {
                    { "reportPricesID", form.reportPriceID},
                    { "date", form.date },
                    { "subject", form.subject },
                    { "accountantID", form.accountantID }
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
