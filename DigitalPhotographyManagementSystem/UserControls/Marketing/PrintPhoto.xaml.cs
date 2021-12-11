using BUS;
using DigitalPhotographyManagementSystem.View;
using DTO;
using MongoDB.Bson;
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
        private List<InvoicePrint> invoicePrint;
        public PrintPhoto()
        {
            InitializeComponent();
            DateTimeTxt.Text = "Date time: " + DateTime.Now.ToString("dd/MM/yyyy");
            List<invoiceDTO> invoices = new List<invoiceDTO>();
            invoices = invoiceBUS.GetAllUnprintedInvoices();
            int i = 1;
            invoicePrint = new List<InvoicePrint>();
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
                listInvoice.Items.Add(newInvoicePrint);
            }
        }

        private void DoneBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = listInvoice.Items.IndexOf(item);
            
        }

        private void listInvoice_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var index = listInvoice.SelectedIndex;
            if (index != -1)
            {
                Invoice_View invoice_View = new Invoice_View(invoicePrint[index].fullInvoiceID);
                invoice_View.ShowDialog();
            }
        }
    }
}
