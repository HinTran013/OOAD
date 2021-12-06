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
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DigitalPhotographyManagementSystem.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlSingleItem.xaml
    /// </summary>
    public partial class UserControlSingleItem : UserControl
    {
        ItemMenu itemmenu;
        DashboardMain _context;
        public UserControlSingleItem(ItemMenu itemMenu, DashboardMain context)
        {
            InitializeComponent();
            _context = context;

            itemmenu = itemMenu;
            this.DataContext = itemMenu;
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            var Thicc = new Thickness(0, 6, 0, 0);
            border.BeginAnimation(FrameworkElement.MarginProperty,null);
            border.Margin = Thicc;
        }

        private void StoryboardRev_Completed(object sender, EventArgs e)
        {
            var Thicc = new Thickness(0, 6, 230, 6);
            border.BeginAnimation(FrameworkElement.MarginProperty, null);
            border.Margin = Thicc;
        }

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            /* The HOME single item tag (on the left side) 
                wont be switched to other single item tags, 
                unless you switch to CONTROL FORM           */
            var SingleItems = this._context.SideMenu.Children.OfType<UserControlSingleItem>();
            if (_context._Account.type == "Admin")
                foreach (var item in SingleItems)
                {
                    if (item != this._context.SideMenu.Children[0] && item.border.Margin.Right == 0)
                    {
                        var storyBoard_rev = item.Resources["StoryboardChooseItem_Rev"] as Storyboard;
                        storyBoard_rev.Begin(item);
                    }
                    //
                    if (item == this && item.border.Margin.Right == 230)
                    {
                        var storyBoard = item.Resources["StoryboardChooseItem"] as Storyboard;
                        storyBoard.Begin(item);
                    }
                }
            else
            {
                foreach (var item in SingleItems)
                {
                    if (item == this && item.border.Margin.Right == 230)
                    {
                        var storyBoard = item.Resources["StoryboardChooseItem"] as Storyboard;
                        storyBoard.Begin(item);

                    }
                    else if (item != this && item.border.Margin.Right == 0)
                    {
                        var storyBoard_rev = item.Resources["StoryboardChooseItem_Rev"] as Storyboard;
                        storyBoard_rev.Begin(item);
                    }
                }
            }
            //_context.TxtDeptName.Text = AccountDept
            _context.TxtTitle.Text = BtnTxt.Text.ToUpper();
            _context.TxtTitleSmall.Text = BtnTxt.Text;
            if (BtnTxt.Text == "HOME")
            {
                _context.TxtDeptName.Text = _context._Account.type;
            }
            _context.OpenOutterWindow(itemmenu.commandType, ((Button)sender).Tag);

            if (_context._Account.type == "Admin")
                foreach (var item in SingleItems)
                {
                    if (item != this._context.SideMenu.Children[0] && item.border.Margin.Right == 0)
                    {
                        var storyBoard_rev = item.Resources["StoryboardChooseItem_Rev"] as Storyboard;
                        storyBoard_rev.Begin(item);
                    }
                }
            else
                foreach (var item in SingleItems)
                {
                    if (item == this && item.border.Margin.Right == 0)
                    {
                        var storyBoard_rev = item.Resources["StoryboardChooseItem_Rev"] as Storyboard;
                        storyBoard_rev.Begin(item);
                    }
                }
        }
    }
}
