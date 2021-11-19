using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using DTO;

namespace DAL
{
    public class ideaDAL
    {
        private IMongoDatabase db;

        public ideaDAL()
        {
            var client = new MongoClient("mongodb+srv://HienTranOOAD:123123123@cluster0.guxtk.mongodb.net/PhotographyManagement?retryWrites=true&w=majority");
            this.db = client.GetDatabase("PhotographyManagement");
        }

        public bool InserNewIdeaRecord(ideaDTO newIdea)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("ideas");
                var newDoc = new BsonDocument
            {
                    { "ideaID", newIdea.ID },
                    { "ideaSubject", newIdea.ideaSubject },
                    { "ideaDescription", newIdea.ideaDescription },
                    { "ideaDate", newIdea.ideaDate },
                    { "staffID", newIdea.staffID }
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
