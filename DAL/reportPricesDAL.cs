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

        //public List<reportPricesDTO> GetAllPriceRequests()
        //{
        //    try
        //    {
        //        var collection = db.GetCollection<BsonDocument>("staffs");
        //        var staffsDoc = collection.Find(_ => true).ToListAsync().Result;
        //        List<staffDTO> staffs = new List<staffDTO>();
        //        foreach (BsonDocument item in staffsDoc)
        //        {
        //            staffs.Add(new staffDTO(
        //                (string)item["name"],
        //                null,
        //                (bool)item["gender"],
        //                (string)item["email"],
        //                (string)item["phoneNumber"],
        //                (int)item["salary"],
        //                (string)item["address"],
        //                (string)item["type"],
        //                (string)item["description"],
        //                (string)item["username"],
        //                (string)item["password"]));
        //        }
        //        return staffs;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
