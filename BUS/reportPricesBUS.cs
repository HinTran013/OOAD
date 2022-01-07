using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL;
using DTO;

namespace BUS
{
     public class reportPricesBUS
    {
        static reportPricesDAL reportPrices = new reportPricesDAL();

        public static bool AddReportPrices(reportPricesDTO form)
        {
            return reportPrices.InsertNewReportPricesRecord(form);
        }

        public static List<reportPricesDTO> GetAllPriceRequests()
        {
            return reportPrices.GetAllPriceRequests();
        }
    }
}
