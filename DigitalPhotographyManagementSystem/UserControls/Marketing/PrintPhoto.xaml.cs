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
        public string customerName { get; set; }
        public string Date { get; set; }
    }

    public partial class PrintPhoto : UserControl
    {
        public PrintPhoto()
        {
            InitializeComponent();

            List<InvoicePrint> invoices = new List<InvoicePrint>();

            listInvoice.Items.Add(new InvoicePrint()
            {
                No = 1,
                invoiceID = "123",
                customerName = "Thanh Hien",
                Date = "2/11/2021"
            });

            listInvoice.Items.Add(new InvoicePrint()
            {
                No = 2,
                invoiceID = "124",
                customerName = "Bao Loc",
                Date = "2/11/2021"
            });

            listInvoice.Items.Add(new InvoicePrint()
            {
                No = 3,
                invoiceID = "125",
                customerName = "Hon Tuyen",
                Date = "2/11/2021"
            });
        }

        private void DoneBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
