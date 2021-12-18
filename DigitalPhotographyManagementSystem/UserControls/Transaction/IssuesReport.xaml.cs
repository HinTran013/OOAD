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
using BUS;
using DTO;
using DigitalPhotographyManagementSystem.View;

namespace DigitalPhotographyManagementSystem.UserControls.Transaction
{
    /// <summary>
    /// Interaction logic for IssuesReport.xaml
    /// </summary>
    public partial class IssuesReport : UserControl
    {
        DateTime timeNow;
        staffDTO accountStaff;

        public IssuesReport(staffDTO staff)
        {
            InitializeComponent();

            timeNow = DateTime.Now;
            DateTimeTxt.Text = "Date: " + timeNow.ToString("dd/MM/yyyy");
            accountStaff = staff;

        }
        
        private void ResetInputs()
        {
            NameTxt.Text = string.Empty;
            TypeTxt.SelectedItem = null;
            DescriptionTxt.Text = string.Empty;
        }

        private bool CheckInputs()
        {
            if (string.IsNullOrEmpty(NameTxt.Text))
            {
                MsgBox.Show("Please specify the name/title of the issue!", MessageBoxTyp.Error);
                return false;
            }
            if(TypeTxt.SelectedItem == null)
            {
                MsgBox.Show("Please the type of the issue!", MessageBoxTyp.Error);
                return false;
            }
            if(string.IsNullOrEmpty(DescriptionTxt.Text))
            {
                MsgBox.Show("Please specify description of the issue to be clear!", MessageBoxTyp.Error);
                return false;
            }

            return true;
        }

        private void ReportBtn_Click(object sender, RoutedEventArgs e)
        {
            if(CheckInputs())
            {
                issueReportDTO report = new issueReportDTO(
                    NameTxt.Text,
                    timeNow.ToString("dd/MM/yyyy"),
                    TypeTxt.Text.ToString(),
                    DescriptionTxt.Text,
                    accountStaff.username,
                    false
                    );

                issueReportBUS.AddNewIssueReport(report);

                MsgBox.Show("Issue has been reported successfully!",MessageBoxTyp.Information);
                ResetInputs();
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            ResetInputs();
        }
    }
}
