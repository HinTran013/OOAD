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
using static DigitalPhotographyManagementSystem.ServiceClass.ItemMenu;
using DTO;
using System.Windows.Media.Animation;
using DigitalPhotographyManagementSystem.UserControls.Account_Information;

namespace DigitalPhotographyManagementSystem.View
{
    /// <summary>
    /// Interaction logic for DashboardMain.xaml
    /// </summary>
    public partial class DashboardMain : Window
    {
        static DashboardMain __instance = null;
        private staffDTO Account;

        public staffDTO _Account { get => Account; set => Account = value; }

        public DashboardMain(staffDTO acc)
        {
            InitializeComponent();
            Account = acc;
            TxtDeptName.Text = Account.type;
            var home = new ItemMenu("HOME", PackIconKind.Home, CommandType.UControl, new DashboardHome());
                   
            //ABOUT popup
            var about = new ItemMenu("ABOUT", PackIconKind.About, CommandType.About);

            //Update account 
            var updateAccount = new ItemMenu("ACCOUNT INFO", PackIconKind.User, CommandType.UControl, new AccountInformation(acc));

            //LOGOUT option
            var logout = new ItemMenu("LOG OUT", PackIconKind.Logout, CommandType.LogOut);

            //EXIT option
            var exit = new ItemMenu("EXIT", PackIconKind.Shutdown, CommandType.Exit);

            UserControlSingleItem UCHome = new UserControlSingleItem(home, this);
            var storyBoard = UCHome.Resources["StoryboardChooseItem"] as Storyboard;
            storyBoard.Begin(UCHome);
            SideMenu.Children.Add(UCHome);

            if (Account.type == "Admin")
            {
                var menuMarketDept = new List<SubItem>();
                menuMarketDept.Add(new SubItem("Propose New Ideas", IdeaProposing.GetInstance(Account)));
                menuMarketDept.Add(new SubItem("Ad Campaign", AdCampaign.GetInstance(Account)));
                menuMarketDept.Add(new SubItem("Print Photos", new PrintPhoto(Account)));
                menuMarketDept.Add(new SubItem("Create Coupon", CreateCoupon.GetInstance()));
                menuMarketDept.Add(new SubItem("Manage Ad Campaigns", new ListAdsCampaign()));
                menuMarketDept.Add(new SubItem("Manage Ideas", new ListIdea()));

                var marketSubMenu = new ItemMenu("MARKETING DEPT", menuMarketDept, PackIconKind.Megaphone);
                var menuTransDept = new List<SubItem>();
                menuTransDept.Add(new SubItem("Create Invoice", new InvoiceCreating(Account)));
                menuTransDept.Add(new SubItem("Photo Delivery", new PhotoDelivery()));
                menuTransDept.Add(new SubItem("Technical Issues Resolve", new IssuesReport(Account)));
                menuTransDept.Add(new SubItem("Manage Issue Reports", new ListIssues()));
                var transSubMenu = new ItemMenu("TRANSACTION DEPT", menuTransDept, PackIconKind.Coins);

                var menuAccDept = new List<SubItem>();
                menuAccDept.Add(new SubItem("Create Payment Bill", new ListPaymentBill(Account)));
                menuAccDept.Add(new SubItem("Report Price Of Photos", new ReportPrices(Account)));
                var accSubMenu = new ItemMenu("ACCOUNTING DEPT", menuAccDept, PackIconKind.Calculator);

                var menuTechDept = new List<SubItem>();
                menuTechDept.Add(new SubItem("Detail Of Invoice", new ListOfInvoices()));
                var techSubMenu = new ItemMenu("TECHINCAL DEPT", menuTechDept, PackIconKind.HammerScrewdriver);

                var menuAdmin = new List<SubItem>();
                menuAdmin.Add(new SubItem("Create Account", new CreateAccount()));
                menuAdmin.Add(new SubItem("Manage Accounts", new ListAccounts()));
                menuAdmin.Add(new SubItem("Manage Price-Change Requests", new ListOfPriceChanges()));
                var AdminSubMenu = new ItemMenu("ADMINISTRATOR", menuAdmin, PackIconKind.Administrator);

                SideMenu.Children.Add(new UserControlMenuDrawer(transSubMenu, this));
                SideMenu.Children.Add(new UserControlMenuDrawer(techSubMenu, this));
                SideMenu.Children.Add(new UserControlMenuDrawer(marketSubMenu, this));
                SideMenu.Children.Add(new UserControlMenuDrawer(accSubMenu, this));
                SideMenu.Children.Add(new UserControlMenuDrawer(AdminSubMenu, this));
            }
            else
            {
                TxtDeptName.Text = Account.type;
                if (Account.type == "Accounting")
                {
                    var paymentBill = new ItemMenu("Create Payment Bill", PackIconKind.Payment, CommandType.UControl, new ListPaymentBill(Account));
                    var report = new ItemMenu("Report Price Of Photos", PackIconKind.Report, CommandType.UControl, new ReportPrices());

                }
                else if (Account.type == "Marketing")
                {
                    var propose = new ItemMenu("Propose New Ideas", PackIconKind.Idea, CommandType.UControl, IdeaProposing.GetInstance(Account));
                    var adCamp = new ItemMenu("Ad Campaign", PackIconKind.Ads, CommandType.UControl, AdCampaign.GetInstance(Account));
                    var print = new ItemMenu("Print Photos", PackIconKind.Printer, CommandType.UControl, new PrintPhoto(Account));
                    var adList = new ItemMenu("Manage Ad Campaigns", PackIconKind.GoogleAds, CommandType.UControl, new ListAdsCampaign());
                    var ideaList = new ItemMenu("Manage Ideas", PackIconKind.HeadIdea, CommandType.UControl, new ListIdea());
                    SideMenu.Children.Add(new UserControlSingleItem(propose, this));
                    SideMenu.Children.Add(new UserControlSingleItem(adCamp, this));
                    SideMenu.Children.Add(new UserControlSingleItem(print, this));
                    SideMenu.Children.Add(new UserControlSingleItem(adList, this));
                    SideMenu.Children.Add(new UserControlSingleItem(ideaList, this));
                }
                else if (Account.type == "Technical")
                {
                    var invoice = new ItemMenu("Detail Of Invoice", PackIconKind.Invoice, CommandType.UControl, new ListOfInvoices());
                    SideMenu.Children.Add(new UserControlSingleItem(invoice, this));
                }
                else if (Account.type == "Transaction")
                {
                    var invoice = new ItemMenu("Create Invoice", PackIconKind.Invoice, CommandType.UControl, new InvoiceCreating(Account));
                    var photoDel = new ItemMenu("Photo Delievery", PackIconKind.TruckDelivery, CommandType.UControl, new PhotoDelivery());
                    var issue = new ItemMenu("Technical Issues Resolve", PackIconKind.GitIssue, CommandType.UControl, new IssuesReport(Account));
                    var listIssue = new ItemMenu("Manage Issue Reports", PackIconKind.ReportProblem, CommandType.UControl, new ListIssues());
                    SideMenu.Children.Add(new UserControlSingleItem(invoice, this));
                    SideMenu.Children.Add(new UserControlSingleItem(photoDel, this));
                    SideMenu.Children.Add(new UserControlSingleItem(issue, this));
                    SideMenu.Children.Add(new UserControlSingleItem(listIssue, this));
                }
            }

            SideMenu.Children.Add(new UserControlSingleItem(updateAccount, this));
            SideMenu.Children.Add(new UserControlSingleItem(about, this));
            SideMenu.Children.Add(new UserControlSingleItem(logout, this));
            SideMenu.Children.Add(new UserControlSingleItem(exit, this));

            TxtStaffName.Text = Account.name;
            SwitchScreen(home.Screen);
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
        public static DashboardMain GetInstance(staffDTO acc)
        {
            if (__instance == null) __instance = new DashboardMain(acc);
            return __instance;
        }
        internal void OpenOutterWindow(CommandType commandType, object sender)
        {

            DashboardMain dashboardMain = this;
            switch (commandType)
            {
                case CommandType.UControl:
                    dashboardMain.SwitchScreen(sender);
                    break;
                case CommandType.About:
                    AboutBox aboutBox = new AboutBox();
                    aboutBox.ShowDialog();
                    break;
                case CommandType.Exit:
                    var messageBoxResult1 = MsgBox.Show("Exit", "Are you sure you want to exit?",
                                MessageBoxButton.YesNo, MessageBoxImg.Warning);
                    if (messageBoxResult1 == MessageBoxResult.Yes)
                    {
                        //DBConnection_BUS.CloseConnection();
                        Application.Current.Shutdown();
                    }
                    break;
                case CommandType.LogOut:
                    var messageBoxResult2 = MsgBox.Show("Log out", "Are you sure you want to log out?",
                                MessageBoxButton.YesNo, MessageBoxImg.Warning);
                    if (messageBoxResult2 == MessageBoxResult.Yes)
                    {
                        //DBConnection_BUS.CloseConnection();

                        this.Release();
                        LoginWindow login = new LoginWindow();
                        login.Show();
                        dashboardMain.Close();
                    }
                    break;
            }
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
        private void Release()
        {
            __instance = null;
            AdCampaign.Release();
            IdeaProposing.Release();
            GC.Collect();
        }
    }
}
