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

namespace DigitalPhotographyManagementSystem.UserControls.AdminOnly
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : UserControl
    {
        private bool gender = true;
        private string type = "";
        private int salary = 0;

        public CreateAccount()
        {
            InitializeComponent();
        }

        #region Event Listeners
        [Obsolete]
        private void ConfirmForm(object sender, RoutedEventArgs e)
        {
            setUpData();

            CreateAccountProcess.CreateAccountTemplate(new staffDTO(
                nameTextBox.Text,
                "",
                this.gender,
                emailTextBox.Text,
                phoneTextBox.Text,
                salary,
                addressTextBox.Text,
                departmentComboBox.Text,
                descriptionTextBox.Text,
                userNameTextBox.Text,
                passwordBox.Password
                ));
        }
        #endregion

        #region Logic Functions
        private void setUpData()
        {
            //set up gender
            if (genderComboBox.Text == "Female" || genderComboBox.Text == "Other")
                gender = false;
            //set up salary
            if (!String.IsNullOrEmpty(salaryTextBox.Text))
            {
                salary = Int32.Parse(salaryTextBox.Text);
            }
        }
        #endregion

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            nameTextBox.Text = departmentComboBox.Text;
        }
    }
}
