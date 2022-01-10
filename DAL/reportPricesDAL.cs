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
                BsonArray serviceList = new BsonArray();
                for(int i = 0; i < form.newServices.Count; i++)
                {
                    serviceList.Add(new BsonDocument
                    {
                        { "serviceType", form.newServices[i].name },
                        { "oldPrice", form.newServices[i].price },
                        { "newPrice", form.newPrices[i] }
                    });
                }

                var newDoc = new BsonDocument
                {   
                    { "date", form.date },
                    { "subject", form.subject },
                    { "serviceList", serviceList },
                    { "accountantID", form.accountantID },
                    { "state", form.state }
                };

                collection.InsertOneAsync(newDoc);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<reportPricesDTO> GetAllPriceRequests()
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("reportPrices");
                var reportsDoc = collection.Find(_ => true).SortByDescending(x => x["_id"]).ToListAsync().Result;
                List<reportPricesDTO> reports = new List<reportPricesDTO>();
                foreach (BsonDocument item in reportsDoc)
                {
                    reports.Add(new reportPricesDTO(
                        (string)item["date"],
                        (string)item["subject"],
                        null,
                        null,
                        (string)item["accountantID"],
                        (bool)item["state"],
                        (ObjectId)item["_id"]
                        )
                        );
                }

                return reports;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public reportPricesDTO GetReportFromID(ObjectId objectId)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("reportPrices");
                var report = collection.Find(x => ((ObjectId)x["_id"]) == objectId).SingleAsync().Result;
                var reportDetails = new List<reportPricesDetailDTO>();
                foreach (BsonDocument detail in report["serviceList"].AsBsonArray)
                {
                    reportDetails.Add(new reportPricesDetailDTO
                    (
                        (string)detail["serviceType"],
                        (double)detail["oldPrice"],
                        (double)detail["newPrice"]
                    ));
                }

                reportPricesDTO gottenReport = new reportPricesDTO(
                    (string)report["date"],
                    (string)report["subject"],
                    null,
                    null,
                    (string)report["accountantID"],
                    (bool)report["state"],
                    null,
                    reportDetails
                    );

                return gottenReport;
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateStateById(ObjectId id, bool newValue)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("reportPrices");

                var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
                var update = Builders<BsonDocument>.Update.Set("state", newValue);
                collection.UpdateOne(filter, update);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
