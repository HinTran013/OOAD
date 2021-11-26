using DAL;
using DTO;
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
    }
}
