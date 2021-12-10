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
    public class invoiceDAL
    {
        private IMongoDatabase db;

        public invoiceDAL()
        {
            var client = new MongoClient("mongodb+srv://HienTranOOAD:123123123@cluster0.guxtk.mongodb.net/PhotographyManagement?retryWrites=true&w=majority");
            this.db = client.GetDatabase("PhotographyManagement");
        }

        public bool InsertNewInvoiceRecord(invoiceDTO newInvoice)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("invoices");
                BsonArray invoiceDetails = new BsonArray();
                foreach (invoiceDetailDTO invoice in newInvoice.invoiceDetails)
                {
                    invoiceDetails.Add(new BsonDocument 
                    { 
                        { "service", invoice.service},
                        { "unitQuantity", invoice.unitQuantity },
                        { "unitPrice", invoice.unitPrice }
                    });
                }
                var newDoc = new BsonDocument
                {
                    {"customerName" , newInvoice.customerName },
                    {"customerAddress", newInvoice.customerAddress },
                    {"customerPhoneNo", newInvoice.customerPhoneNo },
                    {"customerEmail", newInvoice.customerEmail },
                    {"customerRequestDetail", newInvoice.customerRequestDetail },
                    {"staffUsername", newInvoice.staffUsername },
                    {"isCompleted", newInvoice.isCompleted },
                    {"invoiceDetail", invoiceDetails}
                };
                collection.InsertOneAsync(newDoc);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public long CountAllInvoices()
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("invoices");
                return collection.CountDocumentsAsync(new BsonDocument()).Result;
            }
            catch
            {
                return -1;
            }
        }
    }
}
