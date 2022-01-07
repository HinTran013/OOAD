using BUS;
using DigitalPhotographyManagementSystem.View;
using DTO;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DigitalPhotographyManagementSystem.UserControls.Technical
{
    /// <summary>
    /// Interaction logic for ListOfInvoices.xaml
    /// </summary>
    /// 
    public class InvoiceCreated
    {
        public int No { get; set; }
        public string invoiceID { get; set; }
        public ObjectId fullInvoiceID { get; set; }
        public string customerName { get; set; }
        public string Date { get; set; }
        public string StaffID { get; set; }
        public long Services { get; set; }
    }

    public partial class ListOfInvoices : System.Windows.Controls.UserControl
    {
        private ObservableCollection<InvoiceCreated> invoiceCreated;
        private List<string> realInvoiceID = new List<string>();
        public ListOfInvoices()
        {
            InitializeComponent();
            initData();
        }

        private bool InvoiceFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchTxtBox.Text))
            {
                return true;
            }
            else
            {
                if (cbbSearchBy.SelectedIndex == 0)
                    return (item as InvoiceCreated).invoiceID.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 1)
                    return (item as InvoiceCreated).customerName.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 2)
                    return (item as InvoiceCreated).StaffID.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                else
                    return true;
            }
        }

        private void listInvoice_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var invoice = listInvoice.SelectedItems[0] as InvoiceCreated;
            if (invoice != null)
            {
                Invoice_View invoice_View = new Invoice_View(invoice.fullInvoiceID);
                invoice_View.ShowDialog();
            }
        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listInvoice.ItemsSource).Refresh();
        }

        private void UploadBtn_Click(object sender, RoutedEventArgs e)
        {
            var invoice = (sender as FrameworkElement).DataContext as InvoiceCreated;
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;
            open.Filter = "Image Files(*.jpg; *.jpeg; *.png;)|*.jpg; *.jpeg; *.png;";

            if(open.ShowDialog() == DialogResult.OK)
            {
                foreach(var image in open.FileNames)
                {
                    byte[] binaryContent = File.ReadAllBytes(image);
                    photoBUS.AddNewPhoto(new photoDTO(
                        invoice.fullInvoiceID,
                        binaryContent
                        ));
                }
                DoneFunction(sender, e);
            }
        }

        private void DoneFunction(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            var invoice = (sender as FrameworkElement).DataContext as InvoiceCreated;
            int index = listInvoice.Items.IndexOf(item);
            if (invoice != null)
            {
                invoiceBUS.UpdateStateInvoiceFromID(invoice.fullInvoiceID, "SHOT");
                invoiceCreated.Remove(invoice);

                MsgBox.Show("Updated the new state of invoice!", MessageBoxTyp.Information);
            }
        }

        private bool initData()
        {
            try
            {
                List<invoiceDTO> invoices = new List<invoiceDTO>();
                invoices = invoiceBUS.GetAllCreatedStateInvoices();
                int i = 1;
                invoiceCreated = new ObservableCollection<InvoiceCreated>();

                foreach (invoiceDTO item in invoices)
                {
                    var newInvoiceCreated = new InvoiceCreated()
                    {
                        No = i++,
                        invoiceID = item.objectId.ToString().Substring(item.objectId.ToString().Length - 5),
                        fullInvoiceID = (ObjectId)item.objectId,
                        customerName = item.customerName,
                        Date = item.date,
                        StaffID = item.staffUsername,
                        Services = invoiceBUS.GetNumServicesFromID((ObjectId)item.objectId)
                    };
                    realInvoiceID.Add(item.objectId.ToString());
                    invoiceCreated.Add(newInvoiceCreated);
                }

                listInvoice.ItemsSource = invoiceCreated;

                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listInvoice.ItemsSource);
                view.Filter = InvoiceFilter;
                return true;
            }
            catch 
            {
                return false;
            }
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (initData())
            {
                MsgBox.Show("Refresh successfully!", MessageBoxTyp.Information);
            }
            else
            {
                MsgBox.Show("Refresh failed!", MessageBoxTyp.Error);
            }
        }
    }
}
