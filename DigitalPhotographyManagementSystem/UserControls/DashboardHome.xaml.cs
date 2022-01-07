using BUS;
using DigitalPhotographyManagementSystem.UserControls.Accounting;
using DigitalPhotographyManagementSystem.UserControls.AdminOnly;
using DigitalPhotographyManagementSystem.UserControls.Marketing;
using DigitalPhotographyManagementSystem.UserControls.Transaction;
using DigitalPhotographyManagementSystem.View;
using DTO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DigitalPhotographyManagementSystem.UserControls
{
    /// <summary>
    /// Interaction logic for DashboardHome.xaml
    /// </summary>
    public partial class DashboardHome : UserControl
    {
        public DashboardHome()
        {
            InitializeComponent();
            PopulateData();
        }
        private void PopulateData()
        {
            RevenueTxt.Text = "VNĐ " + String.Format("{0:#,0}", CalculateRevenue(DateTime.Now.ToString("MM")));
            ExpenTxt.Text = "VNĐ " + String.Format("{0:#,0}", CalculateExpenditure(DateTime.Now.ToString("MM")));
            AdsTxt.Text = adCampaignBUS.CountAllRunningCampaigns().ToString();
            IdeasTxt.Text = ideaBUS.CountAllIdeas().ToString();
            IssuesTxt.Text = issueReportBUS.CountAllUnsolvedIssues().ToString();
            InvoicesTxt.Text = invoiceBUS.CountAllIncompleteInvoices().ToString();
        }
        private double CalculateRevenue(string month)
        {
            List<paymentBillDTO> paymentBills = paymentBillBUS.GetAllPaymentBills();
            List<paymentBillDTO> completedPaymentBills = new List<paymentBillDTO>();
            List<paymentBillDTO> overduedPaymentBills = new List<paymentBillDTO>();

            double rev = 0;
            if (paymentBills != null)
            {
                completedPaymentBills = paymentBills.FindAll(x => x.state == "COMPLETED" && x.lastModified.ToString("MM") == month);
                overduedPaymentBills = paymentBills.FindAll(x => x.state == "OVERDUE" && x.lastModified.ToString("MM") == month);
                foreach (var bill in completedPaymentBills)
                {
                    rev += bill.totalMoney;
                }
                foreach (var bill in overduedPaymentBills)
                {
                    rev += bill.totalMoney;
                }
            }
            return rev;
        }
        private double CalculateExpenditure(string month)
        {
            List<fundBillDTO> fundBills = new List<fundBillDTO>();
            double exp = 0;
            fundBills = fundBillBUS.GetAllFundBills().FindAll(x => DateTime.Parse(x.date, new CultureInfo("vi-VN", true)).ToString("MM") == month);
            if (fundBills != null)
                foreach(var bill in fundBills)
                {
                    exp += bill.totalMoney;
                }
            return exp;
        }
        private void AdsBD_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListAdsCampaign listAdsCampaign = new ListAdsCampaign();
            ListContainers listContainers = new ListContainers(listAdsCampaign);
            listContainers.ShowDialog();
        }

        private void IdeasBD_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListIdea listIdea = new ListIdea();
            ListContainers listContainers = new ListContainers(listIdea);
            listContainers.ShowDialog();
        }

        private void InvoicesBD_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListInvoices listInvoices = new ListInvoices();
            ListContainers listContainers = new ListContainers(listInvoices);
            listContainers.ShowDialog();
        }

        private void IssuesBD_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListIssues listIssues = new ListIssues();
            ListContainers listContainers = new ListContainers(listIssues);
            listContainers.ShowDialog();
        }

        private void RevenueBD_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ManagePaymentBill listPaymentBill = new ManagePaymentBill();
            ListContainers listContainers = new ListContainers(listPaymentBill);
            listContainers.ShowDialog();
        }

        private void ExpenBD_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListFund listFund = new ListFund();
            ListContainers listContainers = new ListContainers(listFund);
            listContainers.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PopulateData();
            MsgBox.Show("Success", "Refresh service successfully!", MessageBoxTyp.Information);
        }
    }
}
