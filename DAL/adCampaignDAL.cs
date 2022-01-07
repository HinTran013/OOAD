using DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class adCampaignDAL
    {
        private IMongoDatabase db;

        public adCampaignDAL()
        {
            var client = new MongoClient("mongodb+srv://HienTranOOAD:123123123@cluster0.guxtk.mongodb.net/PhotographyManagement?retryWrites=true&w=majority");
            this.db = client.GetDatabase("PhotographyManagement");
        }

        public bool InsertNewAdCampaignRecord(adCampaignDTO newAdCampaign)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("adCampaigns");
                var newDoc = new BsonDocument
            {
                {"campaignName" , newAdCampaign.campaignName },
                {"dateStart", newAdCampaign.dateStart },
                {"dateEnd", newAdCampaign.dateEnd },
                {"type", newAdCampaign.type },
                {"description", newAdCampaign.description },
                {"staffID", newAdCampaign.staffUsername }
            };

                collection.InsertOneAsync(newDoc);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public long CountAllRunningCampaigns()
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("adCampaigns");
                var list = collection.Find(new BsonDocument()).ToListAsync().Result;
                return list.LongCount(x => (DateTime.Parse((string)x["dateEnd"], new CultureInfo("vi-VN", true)) >= DateTime.Now) && (DateTime.Parse((string)x["dateStart"], new CultureInfo("vi-VN", true)) <= DateTime.Now));
            }
            catch
            {
                return 0;
            }
        }
        public List<adCampaignDTO> GetAllAdCampaigns()
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("adCampaigns");
                var adCampaigns = new List<adCampaignDTO>();
                var list = collection.Find(new BsonDocument()).ToListAsync().Result;
                foreach (BsonDocument item in list)
                {
                    adCampaigns.Add(new adCampaignDTO
                    (
                        (string)item["campaignName"],
                        (string)item["dateStart"],
                        (string)item["dateEnd"],
                        (string)item["type"],
                        (string)item["staffID"],
                        (string)item["description"],
                        (ObjectId)item["_id"]
                    ));
                }
                return adCampaigns;
            }
            catch
            {
                return null;
            }
        }
    }
   
}
