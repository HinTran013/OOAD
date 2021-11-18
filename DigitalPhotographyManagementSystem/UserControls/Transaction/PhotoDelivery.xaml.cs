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
    /// Interaction logic for PhotoDelivery.xaml
    /// </summary>
    public class InvoiceDelivery
    {
        public int No { get; set; }
        public string invoiceID { get; set; }
        public string customerName { get; set; }
        public string dueDate { get; set; }
    }

    public partial class PhotoDelivery : UserControl
    {
        public PhotoDelivery()
        {
            InitializeComponent();

            listInvoice.Items.Add(new InvoiceDelivery()
            {
                No = 1,
                invoiceID = "123",
                customerName = "Thanh Hien",
                dueDate = "2/11/2021"
            });

            listInvoice.Items.Add(new InvoiceDelivery()
            {
                No = 2,
                invoiceID = "124",
                customerName = "Bao Loc",
                dueDate = "2/11/2021"
            });

            listInvoice.Items.Add(new InvoiceDelivery()
            {
                No = 3,
                invoiceID = "125",
                customerName = "Hon Tuyen",
                dueDate = "2/11/2021"
            });
        }
    }
}
