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
using System.Windows.Navigation;
using System.Windows.Shapes;

using DigitalPhotographyManagementSystem.View;
using DTO;
using BUS;

namespace DigitalPhotographyManagementSystem.UserControls.Accounting
{
    /// <summary>
    /// Interaction logic for FundBills.xaml
    /// </summary>
    public partial class FundBills : UserControl
    {
               
        public FundBills()
        {
            InitializeComponent();

            setID();
            DateTimeTxt.Text = "Date time: " + DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void setID()
        {
            DateTime now = DateTime.Now;

            int d = (int)System.DateTime.Now.DayOfWeek;
            string day = d.ToString();

            int m = now.Month;
            string month = m.ToString();

            int y = now.Year % 2020;
            string year = y.ToString();

            int h = now.Hour;
            string hour = h.ToString();

            int min = now.Minute;
            string minute = min.ToString();

            int s = now.Second;
            string second = s.ToString();

            string ID = day + month + year + hour + minute + second;
            FundIDTxt.Text = ID;
        }

        private void ResetForm()
        {
            //FundIDTxt.Text = String.Empty;
            TypeCbb.SelectedItem = null;
            CostTxt.Text = String.Empty;
            DescriptionTxt.Text = String.Empty;
        }

        private bool CheckForm()
        {
            if(String.IsNullOrEmpty(TypeCbb.Text))
            {
                var messageBoxResult = MsgBox.Show("Warning",
                                                    "Please select a Bill Type",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImg.Warning);
                return false;
            }
            else if(String.IsNullOrEmpty(CostTxt.Text))
            {
                var messageBoxResult = MsgBox.Show("Warning",
                                                    "Please fill the cost of the bill",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImg.Warning);
                return false;
            }

            return true;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            ResetForm();
        }

        private void ReportBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForm())
            {
                if(String.IsNullOrEmpty(DescriptionTxt.Text))
                {
                    DescriptionTxt.Text = "There is no description.";
                }

                fundBillDTO fundDTO = new fundBillDTO(
                    FundIDTxt.Text,
                    DateTimeTxt.Text,
                    TypeCbb.Text.ToString(),
                    double.Parse(CostTxt.Text.ToString()),
                    DescriptionTxt.Text,
                    "helloooooooooo");

                fundBillBUS.AddFundBill(fundDTO);

                var messageBoxResult = MsgBox.Show(
                    "Notification", 
                    "Save successful",
                    MessageBoxButton.OK, 
                    MessageBoxImg.None);

                ResetForm();
                setID();
            }
        }
    }
}
