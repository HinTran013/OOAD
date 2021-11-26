using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class invoiceDetailBUS
    {
        static invoiceDetailDAL invoiceDetail = new invoiceDetailDAL();

        public static bool AddNewInvoiceDetail(invoiceDetailDTO newInvoiceDetail)
        {
            return invoiceDetail.InsertNewInvoiceDetailRecord(newInvoiceDetail);
        }
    }
}
