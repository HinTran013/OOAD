using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BUS
{
    public class customerBUS
    {
        static customerDAL customer = new customerDAL();

        public static bool AddNewCustomer(customerDTO newCustomer)
        {
            return customer.InserNewCustomerRecord(newCustomer);
        }
    }
}
