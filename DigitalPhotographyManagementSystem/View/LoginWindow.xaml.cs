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
            //For testing and references
            //Account_DTO account = new Account_DTO(Uname_txt.Text, Pass_txt.Password);
            
            if (/*Account_BUS.Login(account)*/ Uname_txt.Text.ToLower().Equals("admin") && Pass_txt.Password.Equals("12345"))
            {
                DashboardMain dashboardMain = new DashboardMain();
                dashboardMain.Show();
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
    }
}
