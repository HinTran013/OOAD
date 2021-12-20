using BUS;
using DigitalPhotographyManagementSystem.UserControls.Accounting;
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
    /// Interaction logic for ManagePaymentBill.xaml
    /// </summary>
    public class PaymentPrint
    {
        public int No { get; set; }
        public string paymentID { get; set; }
        public ObjectId fullpaymentID { get; set; }
        public string customerName { get; set; }
        public string Date { get; set; }
        public string StaffID { get; set; }
        public long Services { get; set; }
        public string TotalMoney { get; set; }
        public string State { get; set; }
        public double CouponPercent { get; set; }
    }
    public partial class ManagePaymentBill : UserControl
    {
        private ObservableCollection<PaymentPrint> paymentPrint;
        public ManagePaymentBill()
        {
            InitializeComponent();
            DateTimeTxt.Text = "Date time: " + DateTime.Now.ToString("dd/MM/yyyy");
            List<paymentBillDTO> paymentBills = new List<paymentBillDTO>();
            paymentBills = paymentBillBUS.GetAllPaymentBills();
            int i = 1;
            paymentPrint = new ObservableCollection<PaymentPrint>();
            foreach (paymentBillDTO item in paymentBills)
            {
                var newPaymentBillPrint = new PaymentPrint()
                {
                    No = i++,
                    paymentID = item.objectId.ToString().Substring(item.objectId.ToString().Length - 5),
                    fullpaymentID = (ObjectId)item.objectId,
                    customerName = item.customerName,
                    Date = item.dueDate,
                    StaffID = item.staffUsername,
                    Services = paymentBillBUS.GetNumServicesFromID((ObjectId)item.objectId),
                    State = item.state,
                    CouponPercent = item.couponDiscount,
                    TotalMoney = String.Format("{0:#,0}", item.totalMoney) + " VNĐ"
                };
                paymentPrint.Add(newPaymentBillPrint);
            }

            listPayment.ItemsSource = paymentPrint;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listPayment.ItemsSource);
            view.Filter = PaymentFilter;
        }
        private bool PaymentFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchTxtBox.Text))
            {
                return true;
            }
            else
            {
                if (cbbSearchBy.SelectedIndex == 0)
                    return (item as PaymentPrint).paymentID.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 1)
                    return (item as PaymentPrint).customerName.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 2)
                    return (item as PaymentPrint).Date.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 3)
                    return (item as PaymentPrint).StaffID.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                else
                    return true;
            }
        }
        private void SearchTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listPayment.ItemsSource).Refresh();
        }

        private void listPayment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listPayment.SelectedItem == null)
                return;
            PaymentPrint paymentBill = listPayment.SelectedItems[0] as PaymentPrint;
            if (paymentBill != null)
            {
                PaymentBill_View paymentBill_View = new PaymentBill_View(paymentBill.fullpaymentID);
                paymentBill_View.ShowDialog();
            }
        }
    }
}
