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
using DigitalPhotographyManagementSystem.View;
using MongoDB.Bson;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;

namespace DigitalPhotographyManagementSystem.UserControls
{
    /// <summary>
    /// Interaction logic for PhotoDelivery.xaml
    /// </summary>
    public class InvoiceDelivery
    {
        public int No { get; set; }
        public ObjectId billID { get; set; }
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
        List<paymentBillDTO> listBill;
        ObservableCollection<InvoiceDelivery> listInvoiceDelivery;

        int num;

        public PhotoDelivery(staffDTO staff)
        {
            InitializeComponent();

            var newCulture = new CultureInfo("");
            newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            CultureInfo.DefaultThreadCurrentCulture = newCulture;
            CultureInfo.DefaultThreadCurrentUICulture = newCulture;
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;


            timeNow = DateTime.Now;
            DateTimeTxt.Text = timeNow.ToString("dd/MM/yyyy");
            accountStaff = staff;

            SetList();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listInvoice.ItemsSource);
            view.Filter = InvoiceFilter;
        }

        private bool InvoiceFilter(object item)
        {
            if (string.IsNullOrEmpty(SearchTxt.Text)) return true;
            else
            {
                if (SearchCbb.SelectedIndex == 0)
                    return (item as InvoiceDelivery).invoiceID.IndexOf(SearchTxt.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (SearchCbb.SelectedIndex == 1)
                    return (item as InvoiceDelivery).customerName.IndexOf(SearchTxt.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (SearchCbb.SelectedIndex == 2)
                    return (item as InvoiceDelivery).dueDate.IndexOf(SearchTxt.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (SearchCbb.SelectedIndex == 3)
                    return (item as InvoiceDelivery).state.IndexOf(SearchTxt.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                else
                    return true;
            }
        }

        private bool CheckDueDates(paymentBillDTO item)
        {
            if (item != null)
            {
                if (item.state == "COMPLETED") return false;

                string time = item.dueDate + " 00:00:00 AM";
                if (DateTime.Compare(timeNow, DateTime.Parse(time)) <= 0)
                {
                    return false;
                }
                else
                {
                    paymentBillBUS.UpdateStateWithObjectId((ObjectId)item.objectId, "OVERDUE");
                    return true;
                }
            }
            return false;
        }

        private void SetList()
        {
            num = 1;

            listBill = paymentBillBUS.GetAllPaymentBills();
            
            listInvoiceDelivery = new ObservableCollection<InvoiceDelivery>();

            foreach (paymentBillDTO item in listBill)
            {
                if (CheckDueDates(item) == true) item.state = "OVERDUE"; 

                listInvoiceDelivery.Add(new InvoiceDelivery
                {
                    No = num++,
                    invoiceID = item.objectId.ToString().Substring(item.objectId.ToString().Length - 5),
                    customerName = item.customerName,
                    dueDate = item.dueDate,
                    state = item.state,
                    billID = (ObjectId)item.objectId
                });
            }

            listInvoice.ItemsSource = listInvoiceDelivery;
        }

        private void UpdateList()
        {
            num = 1;
            listBill = paymentBillBUS.GetAllPaymentBills();
            listInvoiceDelivery.Clear();

            listInvoice.ItemsSource = null;

            foreach (paymentBillDTO item in listBill)
            {
                listInvoiceDelivery.Add(new InvoiceDelivery
                {
                    No = num++,
                    invoiceID = item.objectId.ToString().Substring(item.objectId.ToString().Length - 5),
                    customerName = item.customerName,
                    dueDate = item.dueDate,
                    state = item.state,
                    billID = (ObjectId)item.objectId
                });
            }
            listInvoice.ItemsSource = listInvoiceDelivery;
        }

        private void listInvoice_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listInvoice.SelectedItem == null) return;

            InvoiceDelivery delivery = listInvoice.SelectedItems[0] as InvoiceDelivery;
            PaymentBill_View view = new PaymentBill_View(delivery.billID);
            view.ShowDialog();
        }

        private void DoneBtn_Click(object sender, RoutedEventArgs e)
        {
            var bill = (sender as FrameworkElement).DataContext as InvoiceDelivery;

            if (bill != null)
            {
                string str;
                if(bill.state != "COMPLETED")
                {
                    if(MsgBox.Show("Confirmation", "Please type 'COMPLETED' to finish the process!") == MessageBoxResult.OK)
                    {
                        str = MsgBox.Value;
                        if(str == "COMPLETED")
                        {
                            paymentBillBUS.UpdateStateWithObjectId(bill.billID, "COMPLETED");
                            UpdateList();

                            MsgBox.Show("Congratulation! The business process is done!", MessageBoxTyp.Information);
                        }
                        else
                        {
                            MsgBox.Show("Error! Please retry the confirmation!", MessageBoxTyp.Error);
                            return;
                        }
                    }
                }
            }
            else return;
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateList();
        }
    }
}
