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
    public class invoiceDetailDAL
    {
        private IMongoDatabase db;

        public invoiceDetailDAL()
        {
            var client = new MongoClient("mongodb+srv://HienTranOOAD:123123123@cluster0.guxtk.mongodb.net/PhotographyManagement?retryWrites=true&w=majority");
            this.db = client.GetDatabase("PhotographyManagement");
        }

        public bool InsertNewInvoiceDetailRecord(invoiceDetailDTO newInvoiceDetail)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("invoiceDetails");
                var newDoc = new BsonDocument
            {
                { "invoiceID" , newInvoiceDetail.invoiceID },
                {"service", newInvoiceDetail.service },
                {"unitQuantity", newInvoiceDetail.unitQuantity },
                {"unitPrice", newInvoiceDetail.unitPrice },
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
