﻿using DAL;
using DTO;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class servicesBUS
    {
        static servicesDAL services = new servicesDAL();

        public static List<servicesDTO> GetAllServices()
        {
            return services.GetAllServices();
        }

        public static double GetPriceOfServiceType(string type)
        {
            return services.GetPriceOfServiceType(type);
        }

        public static bool ReplaceOneService(servicesDTO newService)
        {
            return services.ReplaceOneService(newService);
        }
    }
}
