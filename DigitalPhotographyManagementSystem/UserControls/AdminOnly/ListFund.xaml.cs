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
    /// Interaction logic for ListFund.xaml
    /// </summary>
    public class FundPrint
    {
        public int No { get; set; }
        public string FundID { get; set; }
        public ObjectId fullFundID { get; set; }
        public string InvoiceID { get; set; }
        public string Date { get; set; }
        public string StaffID { get; set; }
        public string Cost { get; set; }
    }
    public partial class ListFund : UserControl
    {
        private ObservableCollection<FundPrint> fundPrints;
        public ListFund()
        {
            InitializeComponent();
            fundPrints = new ObservableCollection<FundPrint>();
            DateTimeTxt.Text = "Date time: " + DateTime.Now.ToString("dd/MM/yyyy");
            PopulateData();
            listFunds.ItemsSource = fundPrints;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listFunds.ItemsSource);
            view.Filter = FundFilter;
        }

        private void PopulateData()
        {
            List<fundBillDTO> funds = fundBillBUS.GetAllFundBills();
            int i = 1;
            foreach (fundBillDTO item in funds)
            {
                var newfundPrint = new FundPrint()
                {
                    No = i++,
                    FundID = item.objectId.ToString().Substring(item.objectId.ToString().Length - 5),
                    fullFundID = (ObjectId)item.objectId,
                    InvoiceID = item.objectIdFromInvoice.ToString().Substring(item.objectIdFromInvoice.ToString().Length - 5),
                    Date = item.date,
                    StaffID = item.staffUsername,
                    Cost = String.Format("{0:#,0}", item.totalMoney) + " VNĐ"
                };
                fundPrints.Add(newfundPrint);
            }
        }

        private bool FundFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchTxtBox.Text))
            {
                return true;
            }
            else
            {
                if (cbbSearchBy.SelectedIndex == 0)
                    return (item as FundPrint).FundID.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 1)
                    return (item as FundPrint).InvoiceID.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 2)
                    return (item as FundPrint).Date.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 3)
                    return (item as FundPrint).StaffID.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                else
                    return true;
            }
        }
        private void SearchTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listFunds.ItemsSource).Refresh();
        }

        private void listFunds_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listFunds.SelectedItem == null)
                return;
            FundPrint fund = listFunds.SelectedItems[0] as FundPrint;
            if (fund != null)
            {
                FundBill_View fundBill_View = new FundBill_View(fund);
                fundBill_View.ShowDialog();
            }
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (fundPrints != null)
                fundPrints.Clear();
            PopulateData();
            
        }
    }
}
