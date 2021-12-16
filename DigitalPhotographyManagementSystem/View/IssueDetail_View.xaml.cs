using DigitalPhotographyManagementSystem.UserControls.Transaction;
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
    /// Interaction logic for IssueDetail_View.xaml
    /// </summary>
    public partial class IssueDetail_View : Window
    {
        private IssuePrint issuePrint;

        public IssueDetail_View(IssuePrint issue)
        {
            InitializeComponent();
            issuePrint = issue;
            IssueNameTxt.Text = issue.Title;
            IssuesReportIDTxt.Text = issue.issueID;
            DescTxt.Text = issue.desc;
            IssueTypeTxt.Text = issue.Type;
            DateTxt.Text = "Date: " + issue.Date;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
