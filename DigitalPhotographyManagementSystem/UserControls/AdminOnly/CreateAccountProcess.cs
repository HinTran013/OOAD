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
            if (String.IsNullOrEmpty(newStaff.username) || 
                String.IsNullOrEmpty(newStaff.password) || 
                newStaff.salary == 0 ||
                String.IsNullOrEmpty(newStaff.name)
                )
            {
                var messageBoxResult = MsgBox.Show("Warning", "Please fill out employee's name, username, password and salary!", MessageBoxTyp.Warning);
                return false;
            }

            if (String.IsNullOrEmpty(newStaff.type))
            {
                var messageBoxResult = MsgBox.Show("Warning", "Please choose employee's department!", MessageBoxTyp.Warning);
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

        private static bool IsValidEmail(string email)
        {
            if (email.Trim().EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private static bool checkConfirmPassword(string password, string confirmPassword)
        {
            if(password == confirmPassword)
            {
                return true;
            }

            return false;
        }

        [Obsolete]
        public static bool CreateAccountTemplate(staffDTO newStaff, string confirmPassword)
        {
            if (!CheckIfRequiredIsFilled(newStaff)) {
                return false;
            }
            else if (CheckAccountExists(newStaff.username))
            {
                var messageBoxResult = MsgBox.Show("Warning", "Username has already existed!", MessageBoxTyp.Warning);
                return false;
            }
            else if (!IsValidEmail(newStaff.email))
            {
                var messageBoxResult = MsgBox.Show("Warning", "Email format is invalid!", MessageBoxTyp.Warning);
                return false;
            }
            else if (!checkConfirmPassword(newStaff.password, confirmPassword))
            {
                var messageBoxResult = MsgBox.Show("Warning", "Password and confirm password are not equal!", MessageBoxTyp.Warning);
                return false;
            }
            else
            {
                CreateAccount(newStaff);
                var messageBoxResult = MsgBox.Show("Successfully!", "Create account successfully!", MessageBoxTyp.Information);
                return true;
            }
        }
    }
}
