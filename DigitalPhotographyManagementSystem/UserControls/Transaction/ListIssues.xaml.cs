using BUS;
using DigitalPhotographyManagementSystem.View;
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
        public string State { get; set; }
    }
    public partial class ListIssues : UserControl
    {
        private ObservableCollection<IssuePrint> issuePrints;
        public ListIssues()
        {
            InitializeComponent();
            DateTimeTxt.Text = "Date time: " + DateTime.Now.ToString("dd/MM/yyyy");
            issuePrints = new ObservableCollection<IssuePrint>();
            PopulateData();

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
                if (cbbSearchBy.SelectedIndex == 4)
                    return (item as IssuePrint).IsSolved.ToString().IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 5)
                    return (item as IssuePrint).State.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
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
            if (listIssue.SelectedItem == null)
                return;
            IssuePrint issue = listIssue.SelectedItems[0] as IssuePrint;
            if (issue != null)
            {
                IssueDetail_View issueDetail_View = new IssueDetail_View(issue);
                issueDetail_View.ShowDialog();
            }
        }

        private void SolvedBtn_Click(object sender, RoutedEventArgs e)
        {
            var issue = (sender as FrameworkElement).DataContext as IssuePrint;
            
            //Button btn = sender as Button;
            if (issue != null)
            {
                string str;
                bool newState = false;
                if (issue.IsSolved == false)
                {
                    if (MsgBox.Show("Confirmation", "Please type 'SOLVED' to unsolve") == MessageBoxResult.OK)
                    {
                        str = MsgBox.Value;
                        if (str == "SOLVED")
                        {
                            
                            newState = true;
                            issue.IsSolved = newState;

                            issueReportBUS.UpdateStateByID(issue.fullIssueID, newState);
                            //issuePrints.Remove(issue);
                            issuePrints[issuePrints.IndexOf(issue)].IsSolved = true;
                            issuePrints[issuePrints.IndexOf(issue)].State = (bool)issuePrints[issuePrints.IndexOf(issue)].IsSolved ? "SOLVED" : "ERROR";
                            CollectionViewSource.GetDefaultView(listIssue.ItemsSource).Refresh();

                            MsgBox.Show("Successfully solve the issue", MessageBoxTyp.Information);
                        }
                        else
                        {
                            MsgBox.Show("Error! Failed to solve the issue!", MessageBoxTyp.Error);
                            return;
                        }
                    }
                    else return;
                }
            }
        }
        private void PopulateData()
        {
            List<issueReportDTO> issueReports = issueReportBUS.GetAllIssueReports();
            int i = 1;
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
                    IsSolved = (bool)item.isSolved,
                    State = (bool)item.isSolved ? "SOLVED" : "ERROR"
                };
                issuePrints.Add(newissueReportPrint);
            }
        }
        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (issuePrints != null)
                issuePrints.Clear();
            PopulateData();
        }
    }
}
