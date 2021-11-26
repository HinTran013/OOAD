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
                var newDoc = new BsonDocument
            {
                { "customerName" , newInvoice.customerName },
                {"customerRequirement", newInvoice.customerRequirement },
                {"total", newInvoice.total },
                {"staffID", newInvoice.staffID },
                {"customerID", newInvoice.customerID }
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
