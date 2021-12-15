using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DTO;
using BUS;
using System.Text.RegularExpressions;
using DigitalPhotographyManagementSystem.View;

namespace DigitalPhotographyManagementSystem.UserControls.Account_Information
{
    /// <summary>
    /// Interaction logic for AccountInformation.xaml
    /// </summary>
    public partial class AccountInformation : UserControl
    {
        private staffDTO curStaff;

        public AccountInformation()
        {
            InitializeComponent();
        }

        public AccountInformation(staffDTO staff)
        {
            InitializeComponent();
            this.curStaff = staff;
            GetCreatedData(staff);
        }

        #region Event Listener
        private void phoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //To limit the characters which are the numbers in the textbox
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void salaryTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //To limit the characters which are the numbers in the textbox
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            staffDTO currentStaff = staffBUS.GetStaffByUsername(curStaff.username);
            GetCreatedData(currentStaff);
            var messageBoxResult = MsgBox.Show("Refresh", "Refresh Successfully!", MessageBoxTyp.Information);
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool gender;
                if(genderComboBox.Text == "Male")
                {
                    gender = true;
                }
                else
                {
                    gender = false;
                }

                if(IsValidEmail(emailTextBox.Text) &&
                   checkConfirmPassword(passwordBox.Password, confirmPasswordBox.Password))
                {
                    staffBUS.UpdateStaffAccountByUsername(new staffDTO(
                        nameTextBox.Text,
                        "",
                        gender,
                        emailTextBox.Text,
                        phoneTextBox.Text,
                        123,
                        addressTextBox.Text,
                        departmentComboBox.Text,
                        descriptionTextBox.Text,
                        userNameTextBox.Text,
                        passwordBox.Password));

                    var messageBoxResult = MsgBox.Show("Success", "Update Successfully!", MessageBoxTyp.Information);
                }
            }
            catch (Exception ex)
            {
                var messageBoxResult = MsgBox.Show("Error", "Cannot Update!", MessageBoxTyp.Error);
                throw ex;
            }
        }
        #endregion

        private void GetCreatedData(staffDTO staff)
        {
            nameTextBox.Text = staff.name.ToString();
            phoneTextBox.Text = staff.phoneNumber.ToString();
            emailTextBox.Text = staff.email;

            switch (staff.type)
            {
                case "Technical":
                    Technical.IsSelected = true;
                    break;
                case "Accounting":
                    Accounting.IsSelected = true;
                    break;
                case "Marketing":
                    Marketing.IsSelected = true;
                    break;
                case "Transaction":
                    Transaction.IsSelected = true;
                    break;
                default:
                    departmentComboBox.Items.Add("Admin");
                    departmentComboBox.SelectedItem = "Admin";
                    break;
            }

            if (staff.gender == true)
            {
                genderComboBox.SelectedIndex = 0;
            } 
            else
            {
                genderComboBox.SelectedIndex = 1;
            }

            addressTextBox.Text = staff.address;
            userNameTextBox.Text = staff.username;
            passwordBox.Password = staff.password;
            salaryTextBox.Text = staff.salary.ToString();
            descriptionTextBox.Text = staff.description;
        }
        private static bool IsValidEmail(string email)
        {
            if (email.Trim().EndsWith("."))
            {
                var messageBoxResult = MsgBox.Show("Warning", "Email format is wrong!", MessageBoxTyp.Warning);
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                var messageBoxResult = MsgBox.Show("Warning", "Email format is wrong!", MessageBoxTyp.Warning);
                return false;
            }
        }
        private static bool checkConfirmPassword(string password, string confirmPassword)
        {
            if (password == confirmPassword)
            {
                return true;
            }
            var messageBoxResult = MsgBox.Show("Warning", "Please confirm your password!", MessageBoxTyp.Warning);
            return false;
        }
    }
}
