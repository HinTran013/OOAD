using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL;
using DTO;

namespace BUS
{
    public class reportPricesDetailBUS
    {
        static reportPricesDetailDAL detail = new reportPricesDetailDAL();

        public static bool AddNewReportPrice(reportPricesDetailDTO newprice)
        {
            return detail.InsertNewPriceRecord(newprice);
        }
    }
}
