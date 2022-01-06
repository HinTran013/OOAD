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

using DTO;
using BUS;
using DigitalPhotographyManagementSystem.View;
using System.Globalization;

namespace DigitalPhotographyManagementSystem.UserControls.Accounting
{
    /// <summary>
    /// Interaction logic for CalculateBill.xaml
    /// </summary>
    public partial class CalculateBill : Window
    {
        staffDTO accountStaff;
        int numService;
        double billTotal;
        CultureInfo culture;
        invoiceDTO invoice;
        double couponDiscount = 0;

        public CalculateBill(ObjectId? id, staffDTO staff)
        {
            InitializeComponent();

            accountStaff = staff;
            numService = 1;
            billTotal = 0;

            culture = new CultureInfo("");
            culture.NumberFormat.CurrencySymbol = "";

            invoice = invoiceBUS.GetInvoiceFromID((ObjectId)id);
            SetInfo(invoice);

        }

        private void SetInfo(invoiceDTO invoice)
        {
            BillIDTxt.Text = invoice.objectId.ToString().Substring(invoice.objectId.ToString().Length - 5);
            NameTxt.Text = invoice.customerName;
            PhoneTxt.Text = invoice.customerPhoneNo;
            AddressTxt.Text = invoice.customerAddress;
            EmailTxt.Text = invoice.customerEmail;
            RequestTxt.Text = invoice.customerRequestDetail;
            DateDueTxt.Text = "Due Date: " + invoice.date;

            List<invoiceDetailDTO> details = invoice.invoiceDetails;
            List<ServiceItem> services = new List<ServiceItem>();
            foreach (invoiceDetailDTO item in details)
            {
                ServiceItem ser = new ServiceItem
                {
                    No = numService++,
                    ServiceType = item.service,
                    Quantity = item.unitQuantity,
                    Price = servicesBUS.GetPriceOfServiceType(item.service),
                    Total = (double)(item.unitQuantity * servicesBUS.GetPriceOfServiceType(item.service)),
                };
                ser.totalStr = ser.Total.ToString("C0", culture) + " VND";
                services.Add(ser);
            }
            listService.ItemsSource = services;

            SumTxt.Text = SumTotal(0.0, services).ToString("C0", culture) + " VND";
        }
        
        private double SumTotal(double discount, List<ServiceItem> services = null)
        {
            if(services != null)
            {
                foreach (ServiceItem item in services)
                {
                    billTotal += item.Total;
                }
            }

            return billTotal*(double)((100.0 - discount)/100);
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void CheckBtn_Click(object sender, RoutedEventArgs e)
        {
            if(couponBUS.CheckCouponWithCode(CouponTxt.Text) > 0 && !couponBUS.CheckCouponDate(CouponTxt.Text))
            {
                couponDiscount = couponBUS.CheckCouponWithCode(CouponTxt.Text);
                SumTxt.Text = SumTotal(couponDiscount).ToString("C0", culture) + " VND";
                MsgBox.Show("Applying coupon code succeeded!", MessageBoxTyp.Information);
            }
            else
            {
                MsgBox.Show("Coupon code is invalid!", MessageBoxTyp.Error);
                SumTxt.Text = SumTotal(0.0).ToString("C0", culture) + " VND";
                CouponTxt.Text = null;
            }
        }

        private void PayBtn_Click(object sender, RoutedEventArgs e)
        {
            if(PayBtn.IsEnabled == false)
            {
                MsgBox.Show("The bill has already been paid! Please close the tab!", MessageBoxTyp.Information);
                return;
            }
            
            invoiceBUS.UpdateStateInvoiceFromID((ObjectId)invoice.objectId, "CALC");

            List<billDetailDTO> details = new List<billDetailDTO>();
            foreach (invoiceDetailDTO item in invoice.invoiceDetails)
            {
                billDetailDTO singleDetail = new billDetailDTO
                {
                    service = item.service,
                    unitQuantity = item.unitQuantity
                };
                singleDetail.unitPrice = (int)servicesBUS.GetPriceOfServiceType(singleDetail.service) * singleDetail.unitQuantity;

                details.Add(singleDetail);
            };

            paymentBillDTO payment = new paymentBillDTO
            (
                invoice.customerName,
                invoice.customerAddress,
                invoice.customerPhoneNo,
                invoice.customerEmail,
                invoice.customerRequestDetail,
                invoice.staffUsername,
                "NOTDUE",
                invoice.date,
                details,
                couponDiscount
            );

            paymentBillBUS.AddNewPaymentBill(payment);

            MsgBox.Show("The bill has been paid! Please tell the customer to remember to pick up their photos!", MessageBoxTyp.Information);

            PayBtn.IsEnabled = false;
            CouponTxt.IsReadOnly = true;

            this.DialogResult = true;
        }
    }
}
