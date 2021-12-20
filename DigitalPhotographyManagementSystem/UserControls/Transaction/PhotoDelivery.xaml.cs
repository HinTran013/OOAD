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

using DTO;
using BUS;

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
        //public bool isDue { get; set; }
        public string state { get; set; }
        public string dueDate { get; set; }
    }

    public partial class PhotoDelivery : UserControl
    {
        DateTime timeNow;
        staffDTO accountStaff;

        public PhotoDelivery(staffDTO staff)
        {
            InitializeComponent();

            timeNow = DateTime.Now;
            DateTimeTxt.Text = timeNow.ToString("dd/MM/yyyy");
            accountStaff = staff;
        }
    }
}
