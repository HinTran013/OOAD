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
                { "title" , newReport.title },
                {"date", newReport.date },
                {"staffID", newReport.staffID },
                {"issueType", newReport.issueType },
                {"issueID", newReport.issueID },
                {"description", newReport.description },
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
