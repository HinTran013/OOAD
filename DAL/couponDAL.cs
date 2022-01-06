using DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
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

        public double CheckCouponWithCode(string str)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("coupons");
                var coupon = collection.Find(x => (string)x["couponCode"] == str).SingleAsync().Result;

                return coupon["couponPercent"].ToDouble();
            }
            catch
            {
                return -1.0;
            }
        }
        public bool CheckCouponDate(string str)
        {
            var newCulture = new CultureInfo("");
            newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            CultureInfo.DefaultThreadCurrentCulture = newCulture;
            CultureInfo.DefaultThreadCurrentUICulture = newCulture;
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;

            DateTime timeNow = DateTime.Now;

            try
            {
                var collection = db.GetCollection<BsonDocument>("coupons");
                var coupon = collection.Find(x => (string)x["couponCode"] == str).SingleAsync().Result;

                string time = coupon["endDate"].ToString() + " 00:00:00 AM";

                if (DateTime.Compare(timeNow, DateTime.Parse(time)) <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
