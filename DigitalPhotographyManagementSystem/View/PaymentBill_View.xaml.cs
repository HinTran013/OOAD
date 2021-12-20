using BUS;
using DTO;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for PaymentBill_View.xaml
    /// </summary>
    public class billDetail
    {
        public int No { get; set; }
        public string ServiceName { get; set; }
        public int UnitQuantity { get; set; }
        public int UnitPrice { get; set; }
    }
    public partial class PaymentBill_View : Window
    {
        public PaymentBill_View(ObjectId objectId)
        {
            InitializeComponent();
            paymentBillDTO paymentBill = paymentBillBUS.GetPaymentBillFromID(objectId);
            CustomerNameTxt.Text = "Customer: " + paymentBill.customerName;
            paymentBillIDTxt.Text = paymentBill.objectId.ToString().Substring(paymentBill.objectId.ToString().Length - 5);
            AddressTxt.Text = "Address: " + paymentBill.customerAddress;
            EmailTxt.Text = "Email: " + paymentBill.customerEmail;
            PhoneNoTxt.Text = "Phone Number: " + paymentBill.customerPhoneNo;
            ReqDetailTxt.Text = "Request Detail: " + paymentBill.customerRequestDetail;
            int i = 1;
            foreach (billDetailDTO item in paymentBill.billDetails)
            {
                listPaymentBillService.Items.Add(new billDetail()
                {
                    No = i++,
                    ServiceName = item.service,
                    UnitQuantity = item.unitQuantity,
                    UnitPrice = item.unitPrice
                });
            }
            staffDTO staff = staffBUS.GetStaffByUsername(paymentBill.staffUsername.ToLower());
            StaffNameTxt.Text = staff.name;
            TotalMoneyTxt.Text = "VNĐ " + String.Format("{0:#,0}", paymentBill.totalMoney);
            DueDateTxt.Text = DateTime.Parse(paymentBill.dueDate, new CultureInfo("vi-VN", true)).ToString("dd MMM yy");
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(MainGrid, "paymentBill");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
