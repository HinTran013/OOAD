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
    /// Interaction logic for ListContainers.xaml
    /// </summary>
    public partial class ListContainers : Window
    {

        public ListContainers(UserControl UC)
        {
            InitializeComponent();
            MainContent.Children.Add(UC);          
            DockPanel.SetDock(UC, Dock.Bottom);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
