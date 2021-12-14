using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using BUS;
using DTO;

namespace DigitalPhotographyManagementSystem.View
{
    /// <summary>
    /// Interaction logic for InvoiceCreating.xaml
    /// </summary>
    /// 

    public class ServiceItem
    {
        public int No { get; set; }
        public string ServiceType { get; set; }
        public int Quantity { get; set; }
        public double Price { set; get; }
    }

    public partial class InvoiceCreating : UserControl
    {
        DateTime timeNow;
        int serviceNo;
        List<ServiceItem> serList;
        List<servicesDTO> services;

        private staffDTO accountStaff;

        public InvoiceCreating(staffDTO staff)
        {
            InitializeComponent();

            accountStaff = staff;
            serList = new List<ServiceItem>();

            NewForm();
        }

        private void NewForm()
        {
            SetServices();

            serviceNo = 1;
            serList.Clear();

            timeNow = DateTime.Now;
            DateTxt.Text = "Date: " + timeNow.ToString("dd/MM/yyyy");
        }

        private void ResetInputs()
        {
            DueDateTxt.SelectedDate = null;
            NameTxt.Text = string.Empty;
            AddressTxt.Text = string.Empty;
            PhoneTxt.Text = string.Empty;
            EmailTxt.Text = string.Empty;
            RequestTxt.Text = string.Empty;

            serviceList.Items.Clear();
            ServiceCbb.SelectedItem = null;
        }

        private void SetServices()
        {
            services = servicesBUS.GetAllServices();

            foreach (servicesDTO item in services)
            {
                ServiceCbb.Items.Add(item.name);
            }
        }

        private void Num_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //To limit the characters which are the numbers in the textbox
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void RearrangeNo()
        {
            serviceNo = 1;

            for(int i = 0; i < serList.Count; i++)
            {
                serList[i].No = serviceNo++;
            }

            serviceList.Items.Clear();

            foreach(ServiceItem item in serList)
            {
                serviceList.Items.Add(item);
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if(ServiceCbb.SelectedItem != null)
            {
                for (int i = 0; i < serList.Count; i++)
                {
                    if(ServiceCbb.SelectedItem.ToString() == serList[i].ServiceType)
                    {
                        serList[i].Quantity++;
                        RearrangeNo();
                        ServiceCbb.SelectedItem = null;
                        return;
                    }
                }

                serList.Add(new ServiceItem()
                {
                    No = serviceNo++,
                    ServiceType = ServiceCbb.SelectedItem.ToString(),
                    Quantity = 1,
                    Price = services[ServiceCbb.SelectedIndex].price
                });

                serviceList.Items.Add(serList[serList.Count - 1]);

                ServiceCbb.SelectedItem = null;
            }
            else
            {
                var messageBoxResult = MsgBox.Show("Warning",
                                                   "Please select a service!",
                                                   MessageBoxButton.OK,
                                                   MessageBoxImg.Warning);
            }
            
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected = serviceList.SelectedItem;

            if(selected != null)
            {
                serList.RemoveAt(serviceList.SelectedIndex);
                serviceList.Items.Remove(selected);
            }

            for(int i= 0;i<serList.Count;i++)
            {
                Console.WriteLine(serList[i].ServiceType);
            }

            if(serviceList.Items.Count > 0) RearrangeNo();
        }

        private bool CheckInputs()
        {
            if(DueDateTxt.SelectedDate == null)
            {
                var messageBoxResult = MsgBox.Show("Warning",
                                                   "Please select a due date!",
                                                   MessageBoxButton.OK,
                                                   MessageBoxImg.Warning);

                return false;
            }
            if(NameTxt.Text == null)
            {
                var messageBoxResult = MsgBox.Show("Warning",
                                                   "Please enter the name of the customer!",
                                                   MessageBoxButton.OK,
                                                   MessageBoxImg.Warning);

                return false;
            }
            if (AddressTxt.Text == null)
            {
                var messageBoxResult = MsgBox.Show("Warning",
                                                   "Please enter the address of the customer!",
                                                   MessageBoxButton.OK,
                                                   MessageBoxImg.Warning);

                return false;
            }
            if (EmailTxt.Text == null)
            {
                var messageBoxResult = MsgBox.Show("Warning",
                                                   "Please enter the Email of the customer!",
                                                   MessageBoxButton.OK,
                                                   MessageBoxImg.Warning);

                return false;
            }
            if (PhoneTxt.Text == null)
            {
                var messageBoxResult = MsgBox.Show("Warning",
                                                   "Please provide the customer's phone number!",
                                                   MessageBoxButton.OK,
                                                   MessageBoxImg.Warning);

                return false;
            }

            if(serviceList.Items.Count == 0)
            {
                var messageBoxResult = MsgBox.Show("Warning",
                                                   "There aren't any services required! Please select at least 1 service!",
                                                   MessageBoxButton.OK,
                                                   MessageBoxImg.Warning);

                return false;
            }



            return true;
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if(CheckInputs())
            {
                List<invoiceDetailDTO> list = new List<invoiceDetailDTO>();
                foreach(ServiceItem item in serList)
                {
                    list.Add(new invoiceDetailDTO()
                    {
                        service = item.ServiceType,
                        unitQuantity = item.Quantity
                    });
                }

                invoiceDTO invoice = new invoiceDTO(
                    NameTxt.Text,
                    AddressTxt.Text,
                    PhoneTxt.Text.ToString(),
                    EmailTxt.Text,
                    RequestTxt.Text == "" ? "None" : RequestTxt.Text,
                    accountStaff.username,
                    "CREATED",
                    timeNow.ToString("dd/MM/yyyy"),
                    list);

                invoiceBUS.AddNewInvoice(invoice);

                var messageBoxResult = MsgBox.Show(
                    "Notification",
                    "Submit invoice successful",
                    MessageBoxButton.OK,
                    MessageBoxImg.None);

                ResetInputs();
                NewForm();
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            NewForm();
            ResetInputs();
        }
    }
}
