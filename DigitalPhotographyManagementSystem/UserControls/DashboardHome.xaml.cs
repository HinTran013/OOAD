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
            IssuesTxt.Text = issueReportBUS.CountAllIssueReport().ToString();
            InvoicesTxt.Text = invoiceBUS.CountAllInvoices().ToString();
        }

        private void RevenueBD_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //RevenueTxt.Text += "000";
        }

        private void PopupList(object sender, MouseButtonEventArgs e)
        {
            View.AdsList adsList = new View.AdsList();
            adsList.Show();
        }
    }
}
