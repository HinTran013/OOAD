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

namespace DigitalPhotographyManagementSystem.UserControls.Accounting
{
    /// <summary>
    /// Interaction logic for CalculateBill.xaml
    /// </summary>
    public partial class CalculateBill : Window
    {
        DateTime timeNow;
        staffDTO accountStaff;
        int numService;

        public CalculateBill(ObjectId? id, staffDTO staff)
        {
            InitializeComponent();

            timeNow = DateTime.Now;
            DateTimeTxt.Text = "Date: " + timeNow.ToString("dd/MM/yyyy");
            accountStaff = staff;
            numService = 1;

            invoiceDTO invoice = invoiceBUS.GetInvoiceFromID((ObjectId)id);
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

            List<invoiceDetailDTO> details = invoice.invoiceDetails;
            List<ServiceItem> services = new List<ServiceItem>();
            foreach (invoiceDetailDTO item in details)
            {
                services.Add(new ServiceItem
                {
                    No = numService++,
                    ServiceType = item.service,
                    Quantity = item.unitQuantity,
                    Price = servicesBUS.GetPriceOfServiceType(item.service),
                    Total = (double)(item.unitQuantity * servicesBUS.GetPriceOfServiceType(item.service))
                });
            }
            listService.ItemsSource = services;
        }
        

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
