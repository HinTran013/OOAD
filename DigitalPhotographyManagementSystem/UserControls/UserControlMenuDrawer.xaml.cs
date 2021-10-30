using DigitalPhotographyManagementSystem.ServiceClass;
using DigitalPhotographyManagementSystem.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DigitalPhotographyManagementSystem.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlMenuDrawer.xaml
    /// </summary>
    public partial class UserControlMenuDrawer : UserControl
    {
        ItemMenu itemmenu;
        DashboardMain _context;
        public UserControlMenuDrawer(ItemMenu itemMenu, DashboardMain context)
        {
            InitializeComponent();
            InitializeComponent();
            _context = context;

            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;
            ListViewItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;
            itemmenu = itemMenu;
            this.DataContext = itemMenu;
        }
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _context.SwitchScreen(((TextBlock)sender).Tag);
        }

        private void ListViewItemMenu_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _context.SwitchScreen(((ItemMenu)((ListBoxItem)sender).DataContext).Screen);
        }

        private void ExpanderMenu_Expanded(object sender, RoutedEventArgs e)
        {
            var storyBoard = this.Resources["StoryboardDrawingMenu"] as Storyboard;
            storyBoard.Begin(this);
        }

        private void ExpanderMenu_Collapsed(object sender, RoutedEventArgs e)
        {
            var storyBoard = this.Resources["StoryboardDrawingMenu_Rev"] as Storyboard;
            storyBoard.Begin(this);
        }
    }
}
