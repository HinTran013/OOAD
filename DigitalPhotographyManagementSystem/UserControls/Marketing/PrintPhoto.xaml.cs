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

namespace DigitalPhotographyManagementSystem.UserControls
{
    /// <summary>
    /// Interaction logic for PrintPhoto.xaml
    /// </summary>
    /// 
    public class InvoicePrint
    {
        public int No { get; set; }
        public string invoiceID { get; set; }
        public ObjectId fullInvoiceID { get; set; }
        public string customerName { get; set; }
        public string Date { get; set; }
        public string StaffID { get; set; }
        public long Services { get; set; }
    }
    public partial class PrintPhoto : UserControl
    {
        private ObservableCollection<InvoicePrint> invoicePrint;
        public PrintPhoto()
        {
            InitializeComponent();
            DateTimeTxt.Text = "Date time: " + DateTime.Now.ToString("dd/MM/yyyy");
            List<invoiceDTO> invoices = new List<invoiceDTO>();
            invoices = invoiceBUS.GetAllUnprintedInvoices();
            int i = 1;
            invoicePrint = new ObservableCollection<InvoicePrint>();
            
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
                    Services = invoiceBUS.GetNumServicesFromID((ObjectId)item.objectId)
                };
                invoicePrint.Add(newInvoicePrint);
            }
            listInvoice.ItemsSource = invoicePrint;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listInvoice.ItemsSource);
            view.Filter = InvoiceFilter;
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
                    return (item as InvoicePrint).StaffID.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                else
                    return true;
            }
        }
        private void DoneBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            var invoice = (sender as FrameworkElement).DataContext as InvoicePrint;
            int index = listInvoice.Items.IndexOf(item);
            if (invoice != null)
            {
                invoiceBUS.UpdateStateInvoiceFromID(invoice.fullInvoiceID, "PRINT");
                invoicePrint.Remove(invoice);
                MsgBox.Show("Updated the new state of invoice!", MessageBoxTyp.Information);
            }
        }

        private void listInvoice_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var invoice = listInvoice.SelectedItems[0] as InvoicePrint;
            if (invoice != null)
            {
                Invoice_View invoice_View = new Invoice_View(invoice.fullInvoiceID);
                invoice_View.ShowDialog();
            }
        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listInvoice.ItemsSource).Refresh();
        }

        private void DateChooser_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
