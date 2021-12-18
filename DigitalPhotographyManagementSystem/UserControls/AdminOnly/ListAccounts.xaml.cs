using BUS;
using DigitalPhotographyManagementSystem.View;
using DTO;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DigitalPhotographyManagementSystem.UserControls.AdminOnly
{
    /// <summary>
    /// Interaction logic for ListAccounts.xaml
    /// </summary>
    /// 
    public class account
    {
        public int No { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string department { get; set; }
        public string gender { get; set; }
    }

    public partial class ListAccounts : UserControl
    {
        private ObservableCollection<account> showAccounts;


        public ListAccounts()
        {
            InitializeComponent();
            List<staffDTO> staffs = new List<staffDTO>();
            staffs = staffBUS.GetAllStaffs();
            int i = 1;

            showAccounts = new ObservableCollection<account>();

            foreach (staffDTO item in staffs)
            {
                var newShowAccount = new account()
                {
                    No = i++,
                    name = item.name,
                    username = item.username,
                    department = item.type,
                    gender = item.gender ? "Male" : "Female"
                };
                showAccounts.Add(newShowAccount);
            }

            listAccount.ItemsSource = showAccounts;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listAccount.ItemsSource);
        }

        private void updateAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var rowItem = (sender as Button).DataContext as account;
                string username = rowItem.username;
                UpdateAccountContainerWindow dialog = new UpdateAccountContainerWindow(username);
                dialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                dialog.ShowDialog();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
