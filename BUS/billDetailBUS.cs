using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class billDetailBUS
    {
        static billDetailDAL billDetail = new billDetailDAL();

        public static bool AddNewBillDetail(billDetailDTO newBillDetail)
        {
            return billDetail.InserNewBillDetailRecord(newBillDetail);
        }
    }
}
