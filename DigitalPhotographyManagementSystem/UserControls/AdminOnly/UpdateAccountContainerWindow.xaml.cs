using DigitalPhotographyManagementSystem.UserControls.Account_Information;
using DTO;
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
using System.Windows.Shapes;
using BUS;
using DigitalPhotographyManagementSystem.View;

namespace DigitalPhotographyManagementSystem.UserControls.AdminOnly
{
    /// <summary>
    /// Interaction logic for UpdateAccountContainerWindow.xaml
    /// </summary>
    public partial class UpdateAccountContainerWindow : Window
    {
        public UpdateAccountContainerWindow()
        {
            InitializeComponent();
            AccountInformation updateForm = new AccountInformation();
            myStack.Children.Add(updateForm);
        }

        public UpdateAccountContainerWindow(string username)
        {
            InitializeComponent();
            username = username.ToLower();
            AccountInformation updateForm = new AccountInformation(staffBUS.GetStaffByUsername(username), true);
            myStack.Children.Add(updateForm);
        }

        private void ShutdownBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
