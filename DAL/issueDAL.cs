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
    public class issueDAL
    {
        private IMongoDatabase db;

        public issueDAL()
        {
            var client = new MongoClient("mongodb+srv://HienTranOOAD:123123123@cluster0.guxtk.mongodb.net/PhotographyManagement?retryWrites=true&w=majority");
            this.db = client.GetDatabase("PhotographyManagement");
        }

        public bool InsertNewIssueRecord(issueDTO newIssue)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("issues");
                var newDoc = new BsonDocument
            {
                { "issueName" , newIssue.issueName },
                {"issueDate", newIssue.issueDate },
                {"issueType", newIssue.issueType },
                {"issueDescription", newIssue.issueDescription },
                {"staffID", newIssue.staffID }
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
