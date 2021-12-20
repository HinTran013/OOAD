using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class issueReportDAL
    {
        private IMongoDatabase db;

        public issueReportDAL()
        {
            var client = new MongoClient("mongodb+srv://HienTranOOAD:123123123@cluster0.guxtk.mongodb.net/PhotographyManagement?retryWrites=true&w=majority");
            this.db = client.GetDatabase("PhotographyManagement");
        }

        public bool InserNewIssueReportRecord(issueReportDTO newReport)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("issueReports");
                var newDoc = new BsonDocument
            {
                {"title" , newReport.title },
                {"date", newReport.date },
                {"issueType", newReport.issueType },
                {"description", newReport.description },
                {"staffUsername", newReport.staffUsername },
                {"isSolved", newReport.isSolved }
            };

                collection.InsertOneAsync(newDoc);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public long CountAllUnsolvedIssues()
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("issueReports");
                return collection.CountDocumentsAsync(x => (bool)x["isSolved"] == false).Result;
            }
            catch
            {
                return -1;
            }
        }
        public long CountAllSolvedIssues()
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("issueReports");
                return collection.CountDocumentsAsync(x => (bool)x["isSolved"] == true).Result;
            }
            catch
            {
                return -1;
            }
        }
        public List<issueReportDTO> GetAllIssueReports()
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("issueReports");
                var issueReports = new List<issueReportDTO>();
                var list = collection.Find(new BsonDocument()).ToListAsync().Result;
                foreach (BsonDocument item in list)
                {
                    issueReports.Add(new issueReportDTO
                    (
                        (string)item["title"],
                        (string)item["date"],
                        (string)item["issueType"],
                        (string)item["description"],
                        (string)item["staffUsername"],
                         (bool)item["isSolved"],
                        (ObjectId)item["_id"]
                    ));
                }
                return issueReports;
            }
            catch
            {
                return null;
            }
        }
        public List<issueReportDTO> GetAllUnsolvedIssueReports()
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("issueReports");
                var issueReports = new List<issueReportDTO>();
                var list = collection.Find(new BsonDocument()).ToListAsync().Result;
                foreach (BsonDocument item in list)
                {
                    issueReports.Add(new issueReportDTO
                    (
                        (string)item["title"],
                        (string)item["date"],
                        (string)item["issueType"],
                        (string)item["description"],
                        (string)item["staffUsername"],
                         (bool)item["isSolved"],
                        (ObjectId)item["_id"]
                    ));
                }
                return issueReports;
            }
            catch
            {
                return null;
            }
        }
        public bool UpdateStateByID(ObjectId id, bool newState)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("issueReports");

                var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
                var update = Builders<BsonDocument>.Update.Set("isSolved", newState);
                var result = collection.UpdateOne(filter, update);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
