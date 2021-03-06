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
                    {"state", newInvoice.state },
                    {"date", newInvoice.date },
                    { "totalMoney", newInvoice.totalMoney },
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
        public long CountAllIncompleteInvoices()
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("invoices");
                return collection.CountDocumentsAsync(x => (string)x["state"] != "CALC").Result;
            }
            catch
            {
                return -1;
            }
        }
        //only serves the purpose of loading item in printphoto.xaml, so the dtos are not fully loaded
        public List<invoiceDTO> GetAllUnprintedInvoices()
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("invoices");
                var invoicesDoc = collection.Find(x => ((string)x["state"]) == "SHOT").ToListAsync().Result;
                List<invoiceDTO> invoices = new List<invoiceDTO>();
                foreach (BsonDocument item in invoicesDoc)
                {
                    invoices.Add(new invoiceDTO
                    ( 
                        (string)item["customerName"],
                        null,
                        null,
                        null,
                        null,
                        (string)item["staffUsername"],
                        null,
                        (string)item["date"],
                        null,
                        (ObjectId)item["_id"]
                    ));
                }
                return invoices;
            }
            catch
            {
                return null;
            }
        }

        public List<invoiceDTO> GetAllPrintedInvoices()
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("invoices");
                var invoicesDoc = collection.Find(x => ((string)x["state"]) == "PRINT").ToListAsync().Result;
                List<invoiceDTO> invoices = new List<invoiceDTO>();
                foreach (BsonDocument item in invoicesDoc)
                {
                    invoices.Add(new invoiceDTO
                    (
                        (string)item["customerName"],
                        null,
                        null,
                        null,
                        null,
                        (string)item["staffUsername"],
                        null,
                        (string)item["date"],
                        null,
                        (ObjectId)item["_id"]
                    ));
                }
                return invoices;
            }
            catch
            {
                return null;
            }
        }

        public List<invoiceDTO> GetAllCreatedInvoices()
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("invoices");
                var invoicesDoc = collection.Find(x => ((string)x["state"]) == "CREATED").ToListAsync().Result;
                List<invoiceDTO> invoices = new List<invoiceDTO>();
                foreach (BsonDocument item in invoicesDoc)
                {
                    invoices.Add(new invoiceDTO
                    (
                        (string)item["customerName"],
                        null,
                        null,
                        null,
                        null,
                        (string)item["staffUsername"],
                        null,
                        (string)item["date"],
                        null,
                        (ObjectId)item["_id"]
                    ));
                }
                return invoices;
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
                var collection = db.GetCollection<BsonDocument>("invoices");
                var inv = collection.Find(x => ((ObjectId)x["_id"]) == objectId).SingleAsync().Result;
                return inv["invoiceDetail"].AsBsonArray.Count();
            }
            catch
            {
                return -1;
            }
        }
        public invoiceDTO GetInvoiceFromID(ObjectId objectId)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("invoices");
                var inv = collection.Find(x => ((ObjectId)x["_id"]) == objectId).SingleAsync().Result;
                var invoicedetails = new List<invoiceDetailDTO>();
                foreach (BsonDocument detail in inv["invoiceDetail"].AsBsonArray)
                {
                    invoicedetails.Add(new invoiceDetailDTO
                    (
                        (string)detail["service"],
                        (int)detail["unitQuantity"],
                        (double)detail["unitPrice"]
                    ));
                }
                invoiceDTO invoice = new invoiceDTO
                (
                    (string)inv["customerName"],
                    (string)inv["customerAddress"],
                    (string)inv["customerPhoneNo"],
                    (string)inv["customerEmail"],
                    (string)inv["customerRequestDetail"],
                    (string)inv["staffUsername"],
                    (string)inv["state"],
                    (string)inv["date"],
                    invoicedetails,
                    (ObjectId)inv["_id"],
                    (double)inv["totalMoney"]
                );

                return invoice;
            }
            catch
            {
                return null;
            }
        }
        public bool UpdateStateInvoiceFromID(ObjectId objectId, string newState)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("invoices");

                var filter = Builders<BsonDocument>.Filter.Eq("_id", objectId);
                var update = Builders<BsonDocument>.Update.Set("state", newState);
                var result = collection.UpdateOne(filter, update);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<invoiceDTO> GetAllInvoices()
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("invoices");
                var invoices = new List<invoiceDTO>();
                var list = collection.Find(new BsonDocument()).ToListAsync().Result;
                foreach (BsonDocument item in list)
                {
                    invoices.Add(new invoiceDTO
                    (
                        (string)item["customerName"],
                        (string)item["customerAddress"],
                        (string)item["customerPhoneNo"],
                        (string)item["customerEmail"],
                        (string)item["customerRequestDetail"],
                        (string)item["staffUsername"],
                        (string)item["state"],
                        (string)item["date"],
                        null,
                        (ObjectId)item["_id"],
                        (double)item["totalMoney"]
                    ));
                }
                return invoices;
            }
            catch
            {
                return null;
            }
        }
    }
}
