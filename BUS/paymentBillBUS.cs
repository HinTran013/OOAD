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
    public class paymentBillBUS
    {
        static paymentBillDAL paymentBill = new paymentBillDAL();

        public static bool AddNewPaymentBill(paymentBillDTO newPaymentBill)
        {
            return paymentBill.InsertNewPaymentBillRecord(newPaymentBill);
        }
        public static paymentBillDTO GetPaymentBillFromID(ObjectId objectId)
        {
            return paymentBill.GetPaymentBillFromID(objectId);
        }
        public static List<paymentBillDTO> GetAllPaymentBills()
        {
            return paymentBill.GetAllPaymentBills();
        }
        public static long GetNumServicesFromID(ObjectId objectId)
        {
            return paymentBill.GetNumServicesFromID(objectId);
        }
    }
}
