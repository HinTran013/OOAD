using DigitalPhotographyManagementSystem.UserControls.AdminOnly;
using BUS;
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

namespace DigitalPhotographyManagementSystem.View
{
    /// <summary>
    /// Interaction logic for FundBill_View.xaml
    /// </summary>
    public partial class FundBill_View : Window
    {
        private FundPrint fundPrint;
        public FundBill_View(FundPrint fundprint)
        {
            InitializeComponent();
            fundPrint = fundprint;
            StaffNameTxt.Text = "Customer: " + staffBUS.GetStaffByUsername(fundPrint.StaffID).name;
            DateTimeTxt.Text = fundPrint.Date;
            BillIDTxt.Text = fundPrint.FundID;
            InvoiceIDTxt.Text = fundPrint.InvoiceID;
            MoneyTxt.Text = String.Format("{0:#,0}", fundPrint.Cost) + " VNĐ";

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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
                    printDialog.PrintVisual(MainGrid, "fund bill");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }
    }
}
