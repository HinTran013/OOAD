using BUS;
using DigitalPhotographyManagementSystem.View;
using DTO;
using MongoDB.Bson;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public ObjectId fullInvoiceID { get; set; }
        public string customerName { get; set; }
        public string Date { get; set; }
        public string StaffID { get; set; }
        public long Services { get; set; }
        public int TotalMoney { get; set; }
    }
    public partial class PrintPhoto : System.Windows.Controls.UserControl
    {
        private ObservableCollection<InvoicePrint> invoicePrint;
        private staffDTO Account;
        public PrintPhoto(staffDTO acc)
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            Account = acc;
            DateTimeTxt.Text = "Date time: " + DateTime.Now.ToString("dd/MM/yyyy");
            List<invoiceDTO> invoices = new List<invoiceDTO>();
            invoices = invoiceBUS.GetAllUnprintedInvoices();
            int i = 1;
            invoicePrint = new ObservableCollection<InvoicePrint>();
            foreach (invoiceDTO item in invoices)
            {
                var newInvoicePrint = new InvoicePrint()
                {
                    No = i++,
                    invoiceID = item.objectId.ToString().Substring(item.objectId.ToString().Length - 5),
                    fullInvoiceID = (ObjectId)item.objectId,
                    customerName = item.customerName,
                    Date = item.date,
                    StaffID = item.staffUsername,
                    Services = invoiceBUS.GetNumServicesFromID((ObjectId)item.objectId)
                };
                invoicePrint.Add(newInvoicePrint);
            }

            listInvoice.ItemsSource = invoicePrint;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listInvoice.ItemsSource);
            view.Filter = InvoiceFilter;
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
                    return (item as InvoicePrint).invoiceID.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 1)
                    return (item as InvoicePrint).customerName.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 2)
                    return (item as InvoicePrint).StaffID.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                else
                    return true;
            }
        }
        private void DoneBtn_Click(object sender, RoutedEventArgs e)
        {
            var invoice = (sender as FrameworkElement).DataContext as InvoicePrint;
            if (invoice != null)
            {
                string valueStr;
                if (MsgBox.Show("ENTER FUND", "Type in the fund of printing this photo (in VNĐ)") == MessageBoxResult.OK)
                {
                    valueStr = MsgBox.Value;
                }
                else
                {
                    return;
                }
                int value;
                if (!String.IsNullOrEmpty(valueStr))
                {
                    if (Regex.IsMatch(valueStr, @"^\d+$"))
                    {
                        value = int.Parse(valueStr);
                        if (value <= 0)
                        {
                            MsgBox.Show("Please input fund larger than 0", MessageBoxTyp.Warning);
                            return;
                        }
                    }
                    else
                    {
                        MsgBox.Show("Please input only numbers", MessageBoxTyp.Warning);
                        return;
                    }
                }
                else
                {
                    MsgBox.Show("Please input value", MessageBoxTyp.Warning);
                    return;
                }
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel|*.xlsx|Excel 2003|*.xls";
                openFileDialog.FilterIndex = 1;
                openFileDialog.ShowDialog();
                invoiceBUS.UpdateStateInvoiceFromID(invoice.fullInvoiceID, "PRINT");
                fundBillBUS.AddFundBill(new fundBillDTO
                (
                    DateTime.Now.ToString("dd/MM/yyyy"),
                    value,
                    Account.username,
                    invoice.fullInvoiceID
                ));
                invoicePrint.Remove(invoice);
                MsgBox.Show("Updated the new state of invoice!", MessageBoxTyp.Information);             
            }
        }

        private void listInvoice_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listInvoice.SelectedItem == null)
                return;
            InvoicePrint invoice = listInvoice.SelectedItems[0] as InvoicePrint;
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

        private void ImportBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel|*.xlsx|Excel 2003|*.xls";
            openFileDialog.FilterIndex = 1;
            string filePath = null;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (String.IsNullOrEmpty(openFileDialog.FileName))
                {
                    MsgBox.Show("Invalid filepath!", MessageBoxTyp.Error);
                    return;
                }
                if (!File.Exists(openFileDialog.FileName))
                {
                    MsgBox.Show("File does not exist!", MessageBoxTyp.Error);
                    return;
                }
                filePath = openFileDialog.FileName;
            }
            else return;
            ObservableCollection<InvoicePrint> invoicePrintedList = new ObservableCollection<InvoicePrint>();
            try
            {
                var package = new ExcelPackage(new FileInfo(filePath));

                ExcelWorksheet workSheet = package.Workbook.Worksheets[0];

                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    try
                    {
                        ObjectId objectId = ObjectId.Parse(workSheet.Cells[i, 1].Value.ToString());
                        int fund = 0;
                        if (workSheet.Cells[i,5].Value != null)
                        {
                            var fundTemp = int.Parse(workSheet.Cells[i, 5].Value.ToString());
                            fund = fundTemp;
                        }
                        else fund = 0;


                        InvoicePrint printedInvoice = invoicePrint.Where(x => x.fullInvoiceID == objectId).FirstOrDefault();
                        printedInvoice.TotalMoney = fund;
                        fundBillBUS.AddFundBill(new fundBillDTO
                        (
                            DateTime.Now.ToString("dd/MM/yyyy"),
                            printedInvoice.TotalMoney,
                            Account.username,
                            objectId
                        ));
                        invoiceBUS.UpdateStateInvoiceFromID(objectId, "PRINT");
                        invoicePrint.Remove(invoicePrint.SingleOrDefault(x => x.fullInvoiceID == objectId));
                    }
                    catch (Exception exe)
                    {
                        Console.WriteLine(exe);
                    }
                }
                MsgBox.Show("Successfully update state of invoice!", MessageBoxTyp.Information);
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee);
                MsgBox.Show("Error while file's loading!", MessageBoxTyp.Error);
            }
        }

        private void ExportBtn_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel|*.xlsx|Excel 2003|*.xls";
            saveFileDialog.FileName = "UnprintedPhotoList_" + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = saveFileDialog.FileName;
                if (string.IsNullOrEmpty(filePath))
                {
                    MsgBox.Show("Invalid filepath!", MessageBoxTyp.Error);
                    return;
                }
            }
            else return;

            try
            {
                ObservableCollection<InvoicePrint> invoiceUnprintedList = new ObservableCollection<InvoicePrint>(listInvoice.ItemsSource.Cast<InvoicePrint>());
                using (ExcelPackage p = new ExcelPackage())
                {
                    p.Workbook.Properties.Author = Account.name;
                    p.Workbook.Properties.Title = "Unprinted Photos List - " + DateTime.Now.ToString("dd/MM/yyyy");
                    p.Workbook.Worksheets.Add("KTS print sheet");
                    ExcelWorksheet ws = p.Workbook.Worksheets[0];
                    ws.Name = "KTS print sheet";
                    ws.Cells.Style.Font.Size = 11;
                    ws.Cells.Style.Font.Name = "Calibri";
                    string[] arrColumnHeader = 
                    {
                        "ID",
                        "Customer's name",
                        "Date created",
                        "StaffID",
                        "Fund"
                    };
                    int colIndex = 1;
                    int rowIndex = 1;
                    foreach (var item in arrColumnHeader)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];
                        var fill = cell.Style.Fill;
                        fill.PatternType = ExcelFillStyle.Solid;
                        fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                        var border = cell.Style.Border;
                        border.Bottom.Style =
                            border.Top.Style =
                            border.Left.Style =
                            border.Right.Style = ExcelBorderStyle.Thin;
                        cell.Value = item;
                        colIndex++;
                    }
                    foreach (var item in invoiceUnprintedList)
                    {
                        colIndex = 1;
                        rowIndex++;                  
                        ws.Cells[rowIndex, colIndex++].Value = item.fullInvoiceID.ToString();
                        ws.Cells[rowIndex, colIndex++].Value = item.customerName.ToString();
                        ws.Cells[rowIndex, colIndex++].Value = item.Date.ToString();
                        ws.Cells[rowIndex, colIndex++].Value = item.StaffID.ToString();
                    }
                    ws.Cells.AutoFitColumns();
                    Byte[] bin = p.GetAsByteArray();
                    File.WriteAllBytes(filePath, bin);
                }
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                int i = 1;
            Return:
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    foreach (InvoicePrint invoicePrint in invoiceUnprintedList)
                    {
                        List<photoDTO> photos = photoBUS.getListOfPhotoDTOsByInvoiceID(invoicePrint.fullInvoiceID);
                        if (photos == null)
                        {
                            continue;
                        }
                        foreach (photoDTO photo in photos)
                        {
                            using (System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(photo.photoContent)))
                            {
                                image.Save(folderBrowser.SelectedPath + "/" + invoicePrint.invoiceID + "_photo" + i + ".jpg", ImageFormat.Jpeg);
                            }
                            i++;
                        }
                    }
                }
                else
                {
                    if (MsgBox.Show("Cancel downloading photos for this excel?", MessageBoxTyp.ConfirmationWithYesNo) == MessageBoxResult.No)
                    {
                        goto Return;
                    }
                    else
                    {
                        MsgBox.Show("Export completed! (Without photos)", MessageBoxTyp.Information);
                        return;
                    }
                }
                MsgBox.Show("Export completed!", MessageBoxTyp.Information);
            }
            catch (Exception EE)
            {
                Console.WriteLine(EE);
                MsgBox.Show("Error while file's loading", MessageBoxTyp.Error);
            }
            
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (cbbSearchBy.IsDropDownOpen)
            {
                cbbSearchBy.IsDropDownOpen = false;
            }

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!cbbSearchBy.IsDropDownOpen)
            {
                cbbSearchBy.IsDropDownOpen = true;
            }
        }

        private void DownloadBtn_Click(object sender, RoutedEventArgs e)
        {
            List<photoDTO> photos = photoBUS.getListOfPhotoDTOsByInvoiceID(((sender as FrameworkElement).DataContext as InvoicePrint).fullInvoiceID);
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            int i = 1;
            if (photos == null)
            {
                MsgBox.Show("Selected invoice id has no photos", MessageBoxTyp.Error);
                return;
            }
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                foreach (photoDTO photo in photos)
                {
                    using (System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(photo.photoContent)))
                    {
                        image.Save(folderBrowser.SelectedPath + "/photo" + i + ".jpg", ImageFormat.Jpeg);
                    }
                    i++;
                }
                (sender as System.Windows.Controls.Button).Background = null;
                (sender as System.Windows.Controls.Button).Foreground = System.Windows.Media.Brushes.DarkGreen;
                (sender as System.Windows.Controls.Button).BorderBrush = System.Windows.Media.Brushes.DarkGreen;
                (sender as System.Windows.Controls.Button).Content = "Downloaded!";
                MsgBox.Show("Successfully downloaded images for printing!", MessageBoxTyp.Information);
            }
            else
                return;
        }
    }
}
