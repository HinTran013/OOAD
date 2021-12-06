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
    }
}
