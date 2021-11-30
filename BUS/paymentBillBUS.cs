using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class paymentBillBUS
    {
        static paymentBillDAL paymentBill = new paymentBillDAL();

        public static bool AddNewPaymentBill(paymentBillDTO newPaymentBill)
        {
            return paymentBill.InsertNewPaymentBillRecord(newPaymentBill);
        }
    }
}
