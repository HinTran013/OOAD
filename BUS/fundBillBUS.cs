using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BUS
{
    public class fundBillBUS
    {
        static fundBillDAL fundBill = new fundBillDAL();

        public static bool AddFundBill(fundBillDTO newBill)
        {
            return fundBill.InserNewFundBillRecord(newBill);
        }
        public static List<fundBillDTO> GetAllFundBills()
        {
            return fundBill.GetAllFundBills();
        }
    }
}
