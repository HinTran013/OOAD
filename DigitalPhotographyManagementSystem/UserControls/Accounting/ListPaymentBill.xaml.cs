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

            foreach(invoiceDTO item in invoices)
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

            listInvoice.Items.Add(printed);
        }




        private void ExportBtn_Click(object sender, RoutedEventArgs e)
        {
            //Test

            CalculateBill test = new CalculateBill();
            test.ShowDialog();
        }
    }
}
