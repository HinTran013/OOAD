using BUS;
using DigitalPhotographyManagementSystem.UserControls.Accounting;
using DigitalPhotographyManagementSystem.UserControls.AdminOnly;
using DigitalPhotographyManagementSystem.UserControls.Marketing;
using DigitalPhotographyManagementSystem.UserControls.Transaction;
using DigitalPhotographyManagementSystem.View;
using DTO;
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
            AdsTxt.Text = adCampaignBUS.CountAllCampaigns().ToString();
            IdeasTxt.Text = ideaBUS.CountAllIdeas().ToString();
            IssuesTxt.Text = issueReportBUS.CountAllUnsolvedIssues().ToString();
            InvoicesTxt.Text = invoiceBUS.CountAllIncompleteInvoices().ToString();
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
    }
}
