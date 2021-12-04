using BUS;
using DTO;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        static LoginWindow __instance = null;

        public LoginWindow()
        {
            InitializeComponent();
            Uname_txt.Focus();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            staffDTO staff = new staffDTO();
            if (staffBUS.Login(Uname_txt.Text.ToLower(), Pass_txt.Password.ToLower()))
            {
                staff = staffBUS.GetStaffByUsername(Uname_txt.Text.ToLower());
                DashboardMain.GetInstance(staff).Show();
                this.Close();
            }
            else
            {
                var messageBoxResult = MsgBox.Show("Warning", "Wrong username or password. Try again!", MessageBoxTyp.Warning);
                Uname_txt.Text = "";
                Pass_txt.Password = "";
                Uname_txt.Focus();
            }
        }
        public static LoginWindow GetInstance()
        {
            if (__instance == null) __instance = new LoginWindow();
            return __instance;
        }
    }
}
