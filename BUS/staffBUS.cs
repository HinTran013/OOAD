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
    }
}
