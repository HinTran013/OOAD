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
                    { "ideaSubject", newIdea.ideaSubject },
                    { "ideaDescription", newIdea.ideaDescription },
                    { "ideaDate", newIdea.ideaDate },
                    { "staffID", newIdea.staffUsername }
            };

                collection.InsertOneAsync(newDoc);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public long CountAllIdeas()
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("ideas");
                return collection.CountDocumentsAsync(new BsonDocument()).Result;
            }
            catch
            {
                return -1;
            }
        }
        public List<ideaDTO> GetAllIdeas()
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("ideas");
                var ideas = new List<ideaDTO>();
                var list = collection.Find(new BsonDocument()).ToListAsync().Result;
                foreach (BsonDocument item in list)
                {
                    ideas.Add(new ideaDTO
                    (
                        (string)item["ideaSubject"],
                        (string)item["ideaDescription"],
                        (string)item["ideaDate"],
                        (string)item["staffID"],
                        (ObjectId)item["_id"]
                    ));
                }
                return ideas;
            }
            catch
            {
                return null;
            }
        }
    }
}
