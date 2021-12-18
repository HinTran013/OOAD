using DAL;
using DTO;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class invoiceBUS
    {
        static invoiceDAL invoice = new invoiceDAL();

        public static bool AddNewInvoice(invoiceDTO newInvoice)
        {
            return invoice.InsertNewInvoiceRecord(newInvoice);
        }
        public static long CountAllIncompleteInvoices()
        {
            return invoice.CountAllIncompleteInvoices();
        }
        public static List<invoiceDTO> GetAllUnprintedInvoices()
        {
            return invoice.GetAllUnprintedInvoices();
        }
        public static List<invoiceDTO> GetAllPrintedInvoices()
        {
            return invoice.GetAllPrintedInvoices();
        }
        public static long GetNumServicesFromID(ObjectId objectId)
        {
            return invoice.GetNumServicesFromID(objectId);
        }
        public static invoiceDTO GetInvoiceFromID(ObjectId objectId)
        {
            return invoice.GetInvoiceFromID(objectId);
        }
        public static bool UpdateStateInvoiceFromID(ObjectId objectId, string newState)
        {
            return invoice.UpdateStateInvoiceFromID(objectId, newState);
        }
        public static List<invoiceDTO> GetAllCreatedStateInvoices()
        {
            return invoice.GetAllCreatedInvoices();
        }
        public static List<invoiceDTO> GetAllInvoices()
        {
            return invoice.GetAllInvoices();
        }
    }
}
