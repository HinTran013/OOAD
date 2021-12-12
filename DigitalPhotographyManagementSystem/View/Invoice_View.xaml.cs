using BUS;
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
using System.Windows.Shapes;

namespace DigitalPhotographyManagementSystem.View
{
    /// <summary>
    /// Interaction logic for Invoice_View.xaml
    /// </summary>
    public class InvoiceDetail
    {
        public int No { get; set; }
        public string ServiceName { get; set; }
        public int UnitQuantity { get; set; }
    }
    public partial class Invoice_View : Window
    {
        public Invoice_View(ObjectId objectId)
        {
            InitializeComponent();            
            invoiceDTO invoice = invoiceBUS.GetInvoiceFromID(objectId);
            DateTimeTxt.Text = invoice.date;
            CustomerNameTxt.Text = "Customer: " + invoice.customerName;
            InvoiceIDTxt.Text = invoice.objectId.ToString().Substring(invoice.objectId.ToString().Length - 5);
            AddressTxt.Text = "Address: " + invoice.customerAddress;
            EmailTxt.Text = "Email: " + invoice.customerEmail;
            PhoneNoTxt.Text = "Phone Number: " + invoice.customerPhoneNo;
            ReqDetailTxt.Text = "Request Detail: " + invoice.customerRequestDetail;
            int i = 1;
            foreach (invoiceDetailDTO item in invoice.invoiceDetails)
            {
                listInvoiceService.Items.Add(new InvoiceDetail()
                {
                    No = i++,
                    ServiceName = item.service,
                    UnitQuantity = item.unitQuantity
                });
            }
            staffDTO staff = staffBUS.GetStaffByUsername(invoice.staffUsername.ToLower());
            StaffNameTxt.Text = "Staff: " + staff.name;
        }

        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(MainGrid, "invoice");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
