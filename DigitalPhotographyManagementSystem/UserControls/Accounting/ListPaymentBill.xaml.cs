using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DTO;
using BUS;
using MongoDB.Bson;

namespace DigitalPhotographyManagementSystem.UserControls.Accounting
{
    /// <summary>
    /// Interaction logic for ListPaymentBill.xaml
    /// </summary>
    /// 

    public class PrintedBill
    {
        public int No { get; set; }
        public string invoiceID { get; set; }
        public ObjectId? invoiceIDFull { get; set; }
        public string customerName { get; set; }
        public string dateTime { get; set; }
        public string staffUsername { get; set; }
        public long numService { get; set; }
    }

    public partial class ListPaymentBill : UserControl
    {
        DateTime timeNow;
        staffDTO accountStaff;

        List<invoiceDTO> invoices;
        ObservableCollection<PrintedBill> printed;

        int num;

        public ListPaymentBill(staffDTO staff)
        {
            InitializeComponent();

            timeNow = DateTime.Now;
            DateTimeTxt.Text = "Date: " + timeNow.ToString("dd/MM/yyyy");

            accountStaff = staff;

            num = 1;

            invoices = new List<invoiceDTO>();
            invoices = invoiceBUS.GetAllPrintedInvoices();

            printed = new ObservableCollection<PrintedBill>();
            
            GetInvoices();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listInvoice.ItemsSource);
            view.Filter = InvoiceFilter;
        }

        private void GetInvoices()
        {
            foreach (invoiceDTO item in invoices)
            {
                printed.Add(new PrintedBill
                {
                    No = num++,
                    invoiceID = item.objectId.ToString().Substring(item.objectId.ToString().Length - 5),
                    invoiceIDFull = item.objectId,
                    customerName = item.customerName,
                    dateTime = item.date,
                    staffUsername = item.staffUsername,
                    numService = invoiceBUS.GetNumServicesFromID((ObjectId)item.objectId)
                });
            }

            listInvoice.ItemsSource = printed;
        }

        private void UpdateList()
        {
            invoices = invoiceBUS.GetAllPrintedInvoices();
            printed.Clear();
            listInvoice.ItemsSource = null;

            num = 1;

            foreach (invoiceDTO item in invoices)
            {
                printed.Add(new PrintedBill
                {
                    No = num++,
                    invoiceID = item.objectId.ToString().Substring(item.objectId.ToString().Length - 5),
                    invoiceIDFull = item.objectId,
                    customerName = item.customerName,
                    dateTime = item.date,
                    staffUsername = item.staffUsername,
                    numService = invoiceBUS.GetNumServicesFromID((ObjectId)item.objectId)
                });
            }

            listInvoice.ItemsSource = printed;
        }

        private bool InvoiceFilter (object item)
        {
            if(string.IsNullOrEmpty(SearchTxt.Text))
            {
                return true;
            }
            else
            {
                if (SearchCbb.SelectedIndex == 0)
                    return (item as PrintedBill).invoiceID.IndexOf(SearchTxt.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                else if (SearchCbb.SelectedIndex == 1)
                    return (item as PrintedBill).customerName.IndexOf(SearchTxt.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                else if (SearchCbb.SelectedIndex == 2)
                    return (item as PrintedBill).staffUsername.IndexOf(SearchTxt.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                else
                    return true;
            }
        }

        private void ExportBtn_Click(object sender, RoutedEventArgs e)
        {
            //Test

            CalculateBill test = new CalculateBill(null, accountStaff);
            test.ShowDialog();
        }

        private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listInvoice.ItemsSource).Refresh();
        }

        private void ViewBtn_Click(object sender, RoutedEventArgs e)
        {
            var calculatebillView = (sender as FrameworkElement).DataContext as PrintedBill;

            CalculateBill bill = new CalculateBill(calculatebillView.invoiceIDFull, accountStaff);

            if(bill.ShowDialog() == true)
            {
                UpdateList();
            }

            CollectionViewSource.GetDefaultView(listInvoice.ItemsSource).Refresh();
        }
    }
}
