using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            if(CreateAccountProcess.CreateAccountTemplate(new staffDTO(
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
                ),confirmPasswordBox.Password))
            {
                //if create account successfully then clearing data
                clearInputData();
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            clearInputData();
        }

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

        [Obsolete]
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += HandleKeyPress;
        }

        [Obsolete]
        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                ConfirmForm(sender, e);
            }
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

        private void clearInputData()
        {
            nameTextBox.Text = "";
            phoneTextBox.Text = "";
            emailTextBox.Text = "";
            departmentComboBox.SelectedIndex = -1;
            genderComboBox.SelectedIndex = -1;
            addressTextBox.Text = "";
            userNameTextBox.Text = "";
            passwordBox.Password = "";
            confirmPasswordBox.Password = "";
            salaryTextBox.Text = "";
            descriptionTextBox.Text = "";
        }

        #endregion

        
    }
}
