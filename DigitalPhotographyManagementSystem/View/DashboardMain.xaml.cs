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
            var menuInput = new List<SubItem>();
            menuInput.Add(new SubItem("Fund Bdsill"));
            menuInput.Add(new SubItem("Fund Bdsidssdll"));
            var home = new ItemMenu("    HOME", menuInput, PackIconKind.Home);
            var dwda = new ItemMenu("    dsad", menuInput, PackIconKind.Abc);
            SideMenu.Children.Add(new UserControlMenuDrawer(home, this));
            SideMenu.Children.Add(new UserControlMenuDrawer(dwda, this));
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
    }
}
