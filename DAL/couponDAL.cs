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
    public class couponDAL
    {
        private IMongoDatabase db;

        public couponDAL()
        {
            var client = new MongoClient("mongodb+srv://HienTranOOAD:123123123@cluster0.guxtk.mongodb.net/PhotographyManagement?retryWrites=true&w=majority");
            this.db = client.GetDatabase("PhotographyManagement");
        }

        public bool InsertNewCouponRecord(couponDTO newCoupon)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("coupons");
                var newDoc = new BsonDocument
            {
                {"couponCode" , newCoupon.couponCode },
                {"couponTitle", newCoupon.couponTitle },
                {"couponPercent", newCoupon.couponPercent },
                {"startDate", newCoupon.startDate },
                {"endDate", newCoupon.endDate },
                {"couponDesc", newCoupon.couponDesc }
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
