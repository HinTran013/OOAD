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
                    {"date", newPaymentBill.date },
                    {"invoiceDetail", billDetails},
                    {"totalMoney", newPaymentBill.totalMoney }
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
