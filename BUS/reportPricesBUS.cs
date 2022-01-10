using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL;
using DTO;
using MongoDB.Bson;

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

        public static reportPricesDTO GetReportFromID(ObjectId objectId)
        {
            return reportPrices.GetReportFromID(objectId);
        }

        public static bool UpdateStateById(ObjectId id, bool newValue)
        {
            return reportPrices.UpdateStateById(id, newValue);
        }
     } 
}
