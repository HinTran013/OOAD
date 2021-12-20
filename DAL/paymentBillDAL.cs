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
    public class paymentBillDAL
    {
        private IMongoDatabase db;

        public paymentBillDAL()
        {
            var client = new MongoClient("mongodb+srv://HienTranOOAD:123123123@cluster0.guxtk.mongodb.net/PhotographyManagement?retryWrites=true&w=majority");
            this.db = client.GetDatabase("PhotographyManagement");
        }

        public bool InsertNewPaymentBillRecord(paymentBillDTO newPaymentBill)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("paymentBills");
                BsonArray billDetails = new BsonArray();
                foreach (billDetailDTO bill in newPaymentBill.billDetails)
                {
                    billDetails.Add(new BsonDocument
                    {
                        { "service", bill.service},
                        { "unitQuantity", bill.unitQuantity },
                        { "unitPrice", bill.unitPrice }
                    });
                }
                var newDoc = new BsonDocument
                {
                    {"customerName" , newPaymentBill.customerName },
                    {"customerAddress", newPaymentBill.customerAddress },
                    {"customerPhoneNo", newPaymentBill.customerPhoneNo },
                    {"customerEmail", newPaymentBill.customerEmail },
                    {"customerRequestDetail", newPaymentBill.customerRequestDetail },
                    {"staffUsername", newPaymentBill.staffUsername },
                    {"state", newPaymentBill.state },
                    {"dueDate", newPaymentBill.dueDate },
                    {"billDetail", billDetails},
                    {"couponDiscount", newPaymentBill.couponDiscount },
                    {"totalMoney", newPaymentBill.totalMoney },
                    {"lastModified", DateTime.Now}
                };

                collection.InsertOneAsync(newDoc);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public paymentBillDTO GetPaymentBillFromID(ObjectId objectId)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("paymentBills");
                var inv = collection.Find(x => ((ObjectId)x["_id"]) == objectId).SingleAsync().Result;
                var billdetails = new List<billDetailDTO>();
                foreach (BsonDocument detail in inv["billDetail"].AsBsonArray)
                {
                    billdetails.Add(new billDetailDTO
                    (
                        (string)detail["service"],
                        (int)detail["unitPrice"],
                        (int)detail["unitQuantity"]
                    ));
                }
                paymentBillDTO paymentBill = new paymentBillDTO
                (
                    (string)inv["customerName"],
                    (string)inv["customerAddress"],
                    (string)inv["customerPhoneNo"],
                    (string)inv["customerEmail"],
                    (string)inv["customerRequestDetail"],
                    (string)inv["staffUsername"],
                    (string)inv["state"],
                    (string)inv["dueDate"],
                    billdetails,
                    (double)inv["couponDiscount"],
                    (ObjectId)inv["_id"]
                );

                return paymentBill;
            }
            catch
            {
                return null;
            }
        }
        public List<paymentBillDTO> GetAllPaymentBills()
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("paymentBills");
                var paymentBills = new List<paymentBillDTO>();
                var list = collection.Find(new BsonDocument()).ToListAsync().Result;
                List<billDetailDTO> billDetail = new List<billDetailDTO>();
                foreach (BsonDocument item in list)
                {
                    foreach (var detail in item["billDetail"].AsBsonArray)
                    {
                        billDetail.Add(new billDetailDTO(
                            (string)detail["service"],
                            (int)detail["unitPrice"],
                            (int)detail["unitQuantity"]
                        ));
                    }
                    paymentBills.Add(new paymentBillDTO
                    (
                        (string)item["customerName"],
                        (string)item["customerAddress"],
                        (string)item["customerPhoneNo"],
                        (string)item["customerEmail"],
                        (string)item["customerRequestDetail"],
                        (string)item["staffUsername"],
                        (string)item["state"],
                        (string)item["dueDate"],
                        billDetail,
                        (double)item["couponDiscount"],
                        (ObjectId)item["_id"],
                        (DateTime)item["lastModified"]
                    ));
                }
                return paymentBills;
            }
            catch
            {
                return null;
            }
        }
        public long GetNumServicesFromID(ObjectId objectId)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("paymentBills");
                var inv = collection.Find(x => ((ObjectId)x["_id"]) == objectId).SingleAsync().Result;
                return inv["billDetail"].AsBsonArray.Count();
            }
            catch
            {
                return -1;
            }
        }
        /*public int CalculateRevenueFromMonth(int month)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("paymentBills");
                var billCompleted = collection.Find(x => ((string)x["state"]) == "COMPLETED").ToListAsync().Result;
                var billOverdue = collection.Find(x => ((string)x["state"]) == "OVERDUE").ToListAsync().Result;
                int sum;
                foreach (var bill in billCompleted)
                {
                    foreach (var detail in bill["billDetail"].AsBsonArray)
                    {
                        
                    }
                }
            }
            catch
            {
                return -1;
            }
        }*/
    }
}
