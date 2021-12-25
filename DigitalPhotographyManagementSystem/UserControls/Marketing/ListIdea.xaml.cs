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

namespace DigitalPhotographyManagementSystem.UserControls.Marketing
{
    /// <summary>
    /// Interaction logic for ListIdea.xaml
    /// </summary>
    public class IdeaPrint
    {
        public int No { get; set; }
        public string ideaID { get; set; }
        public ObjectId fullIdeadID { get; set; }
        public string ideaSubject { get; set; }
        public string Date { get; set; }
        public string StaffID { get; set; }
        public string desc { get; set; }
    }
    public partial class ListIdea : UserControl
    {
        private ObservableCollection<IdeaPrint> ideaPrints;
        public ListIdea()
        {
            InitializeComponent();
            DateTimeTxt.Text = "Date time: " + DateTime.Now.ToString("dd/MM/yyyy");
            ideaPrints = new ObservableCollection<IdeaPrint>();
            PopulateData();
            listIdeas.ItemsSource = ideaPrints;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listIdeas.ItemsSource);
            view.Filter = IdeaFilter;
        }
        private void PopulateData()
        {
            List<ideaDTO> ideas = ideaBUS.GetAllIdeas();
            int i = 1;
            foreach (ideaDTO item in ideas)
            {
                var newIdeaPrint = new IdeaPrint()
                {
                    No = i++,
                    ideaID = item.objectId.ToString().Substring(item.objectId.ToString().Length - 5),
                    fullIdeadID = (ObjectId)item.objectId,
                    ideaSubject = item.ideaSubject,
                    Date = item.ideaDate,
                    StaffID = item.staffUsername,
                    desc = item.ideaDescription
                };
                ideaPrints.Add(newIdeaPrint);
            }
        }
        private bool IdeaFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchTxtBox.Text))
            {
                return true;
            }
            else
            {
                if (cbbSearchBy.SelectedIndex == 0)
                    return (item as IdeaPrint).ideaID.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 1)
                    return (item as IdeaPrint).ideaSubject.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 2)
                    return (item as IdeaPrint).Date.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 3)
                    return (item as IdeaPrint).StaffID.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                else
                    return true;
            }
        }
        private void SearchTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listIdeas.ItemsSource).Refresh();
        }

        private void listIdeas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listIdeas.SelectedItem == null)
                return;
            IdeaPrint idea = listIdeas.SelectedItems[0] as IdeaPrint;
            if (idea != null)
            {
                IdeaDetail_View ideaDetail_View = new IdeaDetail_View(idea);
                ideaDetail_View.ShowDialog();
            }
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (ideaPrints != null)
                ideaPrints.Clear();
            PopulateData();
        }
    }
}
