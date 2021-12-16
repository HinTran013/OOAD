using BUS;
using DTO;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DigitalPhotographyManagementSystem.UserControls.Transaction
{
    /// <summary>
    /// Interaction logic for ListIssues.xaml
    /// </summary>
    public class IssuePrint
    {
        public int No { get; set; }
        public string issueID { get; set; }
        public ObjectId fullIssueID { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public string StaffID { get; set; }
        public string desc { get; set; }
        public bool IsSolved { get; set; }
    }
    public partial class ListIssues : UserControl
    {
        private ObservableCollection<IssuePrint> issuePrints;
        public ListIssues()
        {
            InitializeComponent();
            DateTimeTxt.Text = "Date time: " + DateTime.Now.ToString("dd/MM/yyyy");
            List<issueReportDTO> issueReports = issueReportBUS.GetAllIssueReports();
            int i = 1;
            issuePrints = new ObservableCollection<IssuePrint>();
            foreach (issueReportDTO item in issueReports)
            {
                var newissueReportPrint = new IssuePrint()
                {
                    No = i++,
                    issueID = item.objectId.ToString().Substring(item.objectId.ToString().Length - 5),
                    fullIssueID = (ObjectId)item.objectId,
                    Title = item.title,
                    Date = item.date,
                    Type = item.issueType,
                    StaffID = item.staffUsername,
                    desc = item.description,
                    IsSolved = (bool)item.isSolved
                };
                issuePrints.Add(newissueReportPrint);
            }

            listIssue.ItemsSource = issuePrints;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listIssue.ItemsSource);
            view.Filter = IssueFilter;
        }
        private bool IssueFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchTxtBox.Text))
            {
                return true;
            }
            else
            {
                if (cbbSearchBy.SelectedIndex == 0)
                    return (item as IssuePrint).issueID.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 1)
                    return (item as IssuePrint).Title.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 2)
                    return (item as IssuePrint).Date.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 3)
                    return (item as IssuePrint).Type.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;




                else
                    return true;
            }
        }
        private void SearchTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listIssue.ItemsSource).Refresh();
        }

        private void listIssue_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
