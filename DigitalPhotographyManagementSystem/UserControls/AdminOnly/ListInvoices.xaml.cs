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
    /// Interaction logic for ListInvoices.xaml
    /// </summary>
    public partial class ListInvoices : UserControl
    {
        private ObservableCollection<InvoicePrint> invoicePrint;
        public ListInvoices()
        {
            InitializeComponent();
            DateTimeTxt.Text = "Date time: " + DateTime.Now.ToString("dd/MM/yyyy");
            invoicePrint = new ObservableCollection<InvoicePrint>();
            PopulateData();           
            listInvoice.ItemsSource = invoicePrint;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listInvoice.ItemsSource);
            view.Filter = InvoiceFilter;
        }
        private void PopulateData()
        {
            List<invoiceDTO> invoices = new List<invoiceDTO>();
            invoices = invoiceBUS.GetAllInvoices();
            int i = 1;
            foreach (invoiceDTO item in invoices)
            {
                var newInvoicePrint = new InvoicePrint()
                {
                    No = i++,
                    invoiceID = item.objectId.ToString().Substring(item.objectId.ToString().Length - 5),
                    fullInvoiceID = (ObjectId)item.objectId,
                    customerName = item.customerName,
                    Date = item.date,
                    StaffID = item.staffUsername,
                    Services = invoiceBUS.GetNumServicesFromID((ObjectId)item.objectId),
                    State = item.state
                };
                invoicePrint.Add(newInvoicePrint);
            }
        }
        private bool InvoiceFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchTxtBox.Text))
            {
                return true;
            }
            else
            {
                if (cbbSearchBy.SelectedIndex == 0)
                    return (item as InvoicePrint).invoiceID.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 1)
                    return (item as InvoicePrint).customerName.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 2)
                    return (item as InvoicePrint).Date.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 3)
                    return (item as InvoicePrint).StaffID.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                else
                    return true;
            }
        }
        private void SearchTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listInvoice.ItemsSource).Refresh();
        }

        private void listInvoice_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listInvoice.SelectedItem == null)
                return;
            InvoicePrint invoice = listInvoice.SelectedItems[0] as InvoicePrint;
            if (invoice != null)
            {
                Invoice_View invoice_View = new Invoice_View(invoice.fullInvoiceID);
                invoice_View.ShowDialog();
            }
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (invoicePrint != null)
                invoicePrint.Clear();
            PopulateData();
        }
    }
}
