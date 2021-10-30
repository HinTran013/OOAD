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
        public DashboardMain()
        {
            InitializeComponent();
        }
        /*internal void SwitchScreen(object sender)
        {
            //Switch the screen of the DockPanel (named: StackPanelMain)
            var screen = (UserControl)sender;

            if (screen != null)
            {
                MainContent.Children.Clear();
                MainContent.Children.Add(screen);
            }
        }*/
    }
}
