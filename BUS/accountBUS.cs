using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    class accountBUS
    {
        static accountDAL account = new accountDAL();

        public static bool AddNewIssue(accountDTO newAccount)
        {
            return account.InsertNewAccountRecord(newAccount);
        }
    }
}
