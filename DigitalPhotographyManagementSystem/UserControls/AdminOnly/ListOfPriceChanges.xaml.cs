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
using DTO;
using BUS;
using DigitalPhotographyManagementSystem.View;
using System.Text.RegularExpressions;
using DigitalPhotographyManagementSystem.UserControls.AdminOnly;

namespace DigitalPhotographyManagementSystem.UserControls.AdminOnly
{
    /// <summary>
    /// Interaction logic for ListOfPriceChanges.xaml
    /// </summary>
    /// 
    public class requestModel
    {
        public int No { get; set; }
        public ObjectId fullID { get; set; }
        public string subject { get; set; }
        public string date { get; set; }
        public string state { get; set; }
    }

    public partial class ListOfPriceChanges : UserControl
    {
        ObservableCollection<requestModel> showRequests;

        public ListOfPriceChanges()
        {
            InitializeComponent();
            DateTimeTxt.Text = "Date time: " + DateTime.Now.ToString("dd/MM/yyyy");
            showRequests = new ObservableCollection<requestModel>();
            showInitData();
        }

        private void showInitData()
        {
            List<reportPricesDTO> requests = new List<reportPricesDTO>();
            requests = reportPricesBUS.GetAllPriceRequests();
            int i = 1;

            showRequests = new ObservableCollection<requestModel>();

            foreach (reportPricesDTO item in requests)
            {
                var newRequest = new requestModel()
                {
                    No = i++,
                    date = item.date,
                    fullID = (ObjectId)item.reportID,
                    subject = item.subject,
                    state = item.state ? "SOLVED" : "NOT SOLVED"
                };
                showRequests.Add(newRequest);
            }

            listService.ItemsSource = showRequests;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listService.ItemsSource);
            view.Filter = RequestFilter;
        }

        private bool RequestFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchTxtBox.Text))
            {
                return true;
            }
            else
            {
                if (cbbSearchBy.SelectedIndex == 0)
                    return (item as requestModel).subject.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                else if (cbbSearchBy.SelectedIndex == 1)
                    return (item as requestModel).date.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                else if (cbbSearchBy.SelectedIndex == 2)
                    return (item as requestModel).state.ToString().IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                else
                    return true;
            }

            return false;
        }

        private void SearchTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listService.ItemsSource).Refresh();
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            showInitData();
            var messageBoxResult = MsgBox.Show("Success", "Refresh service successfully!", MessageBoxTyp.Information);
        }

        private void viewBtn_Click(object sender, RoutedEventArgs e)
        {
            var rowItem = (sender as Button).DataContext as requestModel;
            ObjectId itemID = rowItem.fullID;
            invoiceDetailWindow dialog = new invoiceDetailWindow(itemID);
            dialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dialog.ShowDialog();
        }

        private void stateTxt_Loaded(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBlock).Text == "SOLVED")
            {
                (sender as TextBlock).Foreground = Brushes.Green;
            }

            if ((sender as TextBlock).Text == "NOT SOLVED")
            {
                (sender as TextBlock).Foreground = Brushes.Red;
            }
        }
    }
}
