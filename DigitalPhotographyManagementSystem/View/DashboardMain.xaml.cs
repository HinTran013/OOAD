using DigitalPhotographyManagementSystem.ServiceClass;
using DigitalPhotographyManagementSystem.UserControls;
using DigitalPhotographyManagementSystem.UserControls.Accounting;
using DigitalPhotographyManagementSystem.UserControls.Technical;
using DigitalPhotographyManagementSystem.UserControls.AdminOnly;
using DigitalPhotographyManagementSystem.UserControls.Marketing;
using DigitalPhotographyManagementSystem.UserControls.Transaction;
using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for DashboardMain.xaml
    /// </summary>
    public partial class DashboardMain : Window
    {
        static DashboardMain __instance = null;
        public DashboardMain()
        {
            InitializeComponent();
            var menuMarketDept = new List<SubItem>();
            menuMarketDept.Add(new SubItem("Propose New Ideas", new IdeaProposing()));
            menuMarketDept.Add(new SubItem("Ad Campaign", new AdCampaign() ));
            menuMarketDept.Add(new SubItem("Print Photos", new PrintPhoto()));
            var marketSubMenu = new ItemMenu("MARKETING DEPT", menuMarketDept, PackIconKind.Megaphone);

            var menuTransDept = new List<SubItem>();
            menuTransDept.Add(new SubItem("Create Invoice", new InvoiceCreating()));
            menuTransDept.Add(new SubItem("Photo delivery", new PhotoDelivery()));            
            menuTransDept.Add(new SubItem("Technical issues resolve", new IssuesReport()));
            var transSubMenu = new ItemMenu("TRANSACTION DEPT", menuTransDept, PackIconKind.Coins);

            var menuAccDept = new List<SubItem>();
            menuAccDept.Add(new SubItem("Create Payment Bill", new CalculateBills()));
            menuAccDept.Add(new SubItem("Create Fund Bill", new FundBills()));
            menuAccDept.Add(new SubItem("Report Price of Photos", new ReportPrices()));
            var accSubMenu = new ItemMenu("ACCOUNTING DEPT", menuAccDept, PackIconKind.Calculator);

            var menuTechDept = new List<SubItem>();
            menuTechDept.Add(new SubItem("Detail Of Invoice", new ListOfInvoices()));
            var techSubMenu = new ItemMenu("TECHINCAL DEPT", menuTechDept, PackIconKind.HammerScrewdriver);

            var menuAdmin = new List<SubItem>();
            menuAdmin.Add(new SubItem("Create Account", new CreateAccount()));
            menuAdmin.Add(new SubItem("Manage Accounts", new ListAccounts()));
            menuAdmin.Add(new SubItem("Manage Price Changes", new ListOfPriceChanges()));
            var AdminSubMenu = new ItemMenu("ADMINISTRATOR", menuAdmin, PackIconKind.Administrator);

            SideMenu.Children.Add(new UserControlMenuDrawer(marketSubMenu, this));
            SideMenu.Children.Add(new UserControlMenuDrawer(transSubMenu, this));
            SideMenu.Children.Add(new UserControlMenuDrawer(accSubMenu, this));
            SideMenu.Children.Add(new UserControlMenuDrawer(techSubMenu, this));
            SideMenu.Children.Add(new UserControlMenuDrawer(AdminSubMenu, this));
        }
        internal void SwitchScreen(object sender)
        {
            //Switch the screen of the DockPanel (name: MainContent)
            var screen = (UserControl)sender;

            if (screen != null)
            {
                MainContent.Children.Clear();
                MainContent.Children.Add(screen);
            }
        }
        public static DashboardMain GetInstance()
        {
            if (__instance == null) __instance = new DashboardMain();
            return __instance;
        }

        private void ShutdownBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
