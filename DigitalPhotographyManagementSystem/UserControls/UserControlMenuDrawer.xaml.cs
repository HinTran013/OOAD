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
            var SingleItems = this._context.SideMenu.Children.OfType<UserControlSingleItem>();
            foreach (var item in SingleItems)
            {
                if (item == this._context.SideMenu.Children[0] && item.border.Margin.Right == 0)
                {
                    var storyBoard_rev = item.Resources["StoryboardChooseItem_Rev"] as Storyboard;
                    storyBoard_rev.Begin(item);
                }
            }

            _context.SwitchScreen(((TextBlock)sender).Tag);
            switch(ExpanderMenu.Header.ToString().Replace(" ", ""))
            {
                case "ACCOUNTINGDEPT":
                    _context.TxtDeptName.Text = "Accounting Department";
                    break;
                case "MARKETINGDEPT":
                    _context.TxtDeptName.Text = "Marketing Department";
                    break;
                case "TRANSACTIONDEPT":
                    _context.TxtDeptName.Text = "Transaction Department";
                    break;
                case "TECHINCALDEPT":
                    _context.TxtDeptName.Text = "Technical Department";
                    break;
                case "ADMINISTRATOR":
                    _context.TxtDeptName.Text = "Administrator (CEO)";
                    break;
            }
            _context.TxtTitle.Text = ((TextBlock)sender).Text.ToUpper();
            _context.TxtTitleSmall.Text = ((TextBlock)sender).Text;
        }

        private void ListViewItemMenu_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _context.SwitchScreen(((ItemMenu)((ListBoxItem)sender).DataContext).Screen);
            switch (ExpanderMenu.Header.ToString().Replace(" ", ""))
            {
                case "ACCOUNTINGDEPT":
                    _context.TxtDeptName.Text = "Accounting Department";
                    break;
                case "MARKETINGDEPT":
                    _context.TxtDeptName.Text = "Marketing Department";
                    break;
                case "TRANSACTIONDEPT":
                    _context.TxtDeptName.Text = "Transaction Department";
                    break;
                case "TECHINCALDEPT":
                    _context.TxtDeptName.Text = "Technical Department";
                    break;
                case "ADMINISTRATOR":
                    _context.TxtDeptName.Text = "Administrator (CEO)";
                    break;
            }
            _context.TxtTitle.Text = ((TextBlock)sender).Text.ToUpper();
            _context.TxtTitleSmall.Text = ((TextBlock)sender).Text;
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
