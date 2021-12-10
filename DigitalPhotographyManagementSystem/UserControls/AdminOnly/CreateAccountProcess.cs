using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BUS;
using DigitalPhotographyManagementSystem.View;
using DTO;

namespace DigitalPhotographyManagementSystem.UserControls.AdminOnly
{
    public static class CreateAccountProcess
    {
        [Obsolete]
        private static bool CheckAccountExists(string userName)
        {
            return staffBUS.checkIfAccountExists(userName);
        }

        private static bool CheckIfRequiredIsFilled(staffDTO newStaff)
        {
            if (String.IsNullOrEmpty(newStaff.username))
            {
                var messageBoxResult = MsgBox.Show("Warning", "Please fill out username!", MessageBoxTyp.Warning);
                return false;
            }
            else if (String.IsNullOrEmpty(newStaff.password))
            {
                var messageBoxResult = MsgBox.Show("Warning", "Please fill out password!", MessageBoxTyp.Warning);
                return false;
            }
            else if (String.IsNullOrEmpty(newStaff.type))
            {
                var messageBoxResult = MsgBox.Show("Warning", "Please choose type of department!", MessageBoxTyp.Warning);
                return false;
            }
            else if (newStaff.salary == 0)
            {
                var messageBoxResult = MsgBox.Show("Warning", "Please fill out salary!", MessageBoxTyp.Warning);
                return false;
            }
            else if (String.IsNullOrEmpty(newStaff.name))
            {
                var messageBoxResult = MsgBox.Show("Warning", "Please fill out name!", MessageBoxTyp.Warning);
                return false;
            }

            if (String.IsNullOrEmpty(newStaff.phoneNumber))
            {
                newStaff.phoneNumber = "";
            }

            if (String.IsNullOrEmpty(newStaff.email))
            {
                newStaff.email = "";
            }

            if (String.IsNullOrEmpty(newStaff.address))
            {
                newStaff.address = "";
            }

            if (String.IsNullOrEmpty(newStaff.description))
            {
                newStaff.description = "";
            }

            return true;
        }

        private static void CreateAccount(staffDTO newStaff)
        {
            staffBUS.AddNewStaff(newStaff);
        }

        public static void test()
        {
            var messageBoxResult = MsgBox.Show("Warning", "Username has already existed!", MessageBoxTyp.Warning);
        }

        [Obsolete]
        public static void CreateAccountTemplate(staffDTO newStaff)
        {

            if (CheckAccountExists(newStaff.username))
            {
                var messageBoxResult = MsgBox.Show("Warning", "Username has already existed!", MessageBoxTyp.Warning);
            }
            else
            {
                if (CheckIfRequiredIsFilled(newStaff))
                {
                    CreateAccount(newStaff);
                }
            }
        }
    }
}
