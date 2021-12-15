using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BUS
{
    public class staffBUS
    {
        static staffDAL staff = new staffDAL();

        public static bool AddNewStaff(staffDTO newStaff)
        {
            return staff.InserNewStaffRecord(newStaff);
        }
        public static bool Login(string username, string password)
        {
            return staff.Login(username, password);
        }
        public static staffDTO GetStaffByUsername(string username)
        {
            return staff.GetStaffByUsername(username);
        }

        [Obsolete]
        public static bool checkIfAccountExists(string username)
        {
            return staff.CheckIfAccountExists(username);
        }

        public static bool UpdateStaffAccountByUsername(staffDTO newStaff)
        {
            return staff.UpdateStaffAccountByUsername(newStaff);
        }
    }
}
