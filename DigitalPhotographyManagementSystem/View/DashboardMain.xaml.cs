using DigitalPhotographyManagementSystem.ServiceClass;
using DigitalPhotographyManagementSystem.UserControls;
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
            menuMarketDept.Add(new SubItem("Propose new ideas"));
            menuMarketDept.Add(new SubItem("Ad campaign"));
            menuMarketDept.Add(new SubItem("Print photos", new PrintPhoto()));
            var marketSubMenu = new ItemMenu("  MARKETING DEPT", menuMarketDept, PackIconKind.Speaker);

            var menuTransDept = new List<SubItem>();
            menuTransDept.Add(new SubItem("Create invoice"));
            menuTransDept.Add(new SubItem("Photo delivery", new PhotoDelivery()));
            menuTransDept.Add(new SubItem("Technical issues resolve"));
            var transSubMenu = new ItemMenu("  TRANSACTION DEPT", menuTransDept, PackIconKind.Coins);

            var menuAccDept = new List<SubItem>();
            menuAccDept.Add(new SubItem("Create bill"));
            menuAccDept.Add(new SubItem("Manage"));
            menuAccDept.Add(new SubItem("Report price"));
            var accSubMenu = new ItemMenu("  ACCOUNTING DEPT", menuAccDept, PackIconKind.Calculator);

            var menuTechDept = new List<SubItem>();
            var techSubMenu = new ItemMenu("  TECHINCAL DEPT", menuTechDept, PackIconKind.Toolbox);


            SideMenu.Children.Add(new UserControlMenuDrawer(marketSubMenu, this));
            SideMenu.Children.Add(new UserControlMenuDrawer(transSubMenu, this));
            SideMenu.Children.Add(new UserControlMenuDrawer(accSubMenu, this));
            SideMenu.Children.Add(new UserControlMenuDrawer(techSubMenu, this));
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
