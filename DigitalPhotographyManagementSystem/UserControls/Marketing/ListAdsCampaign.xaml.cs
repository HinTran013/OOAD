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
    /// Interaction logic for ListAdsCampaign.xaml
    /// </summary>
    public class AdCampaignPrint
    {
        public int No { get; set; }
        public string adsID { get; set; }
        public ObjectId fullAdsID { get; set; }
        public string campaignName { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string StaffID { get; set; }
        public string type { get; set; }
        public string desc { get; set; }
    }
    public partial class ListAdsCampaign : UserControl
    {
        private ObservableCollection<AdCampaignPrint> adPrints;
        public ListAdsCampaign()
        {
            InitializeComponent();
            DateTimeTxt.Text = "Date time: " + DateTime.Now.ToString("dd/MM/yyyy");
            List<adCampaignDTO> adCampaigns = adCampaignBUS.GetAllAdCampaigns();
            int i = 1;
            adPrints = new ObservableCollection<AdCampaignPrint>();
            foreach (adCampaignDTO item in adCampaigns)
            {
                var newAdPrint = new AdCampaignPrint()
                {
                    No = i++,
                    adsID = item.objectId.ToString().Substring(item.objectId.ToString().Length - 5),
                    fullAdsID = (ObjectId)item.objectId,
                    campaignName = item.campaignName,
                    DateStart = item.dateStart,
                    DateEnd = item.dateEnd,
                    StaffID = item.staffUsername,
                    type = item.type,
                    desc = item.description
                };
                adPrints.Add(newAdPrint);
            }

            listAdCampaign.ItemsSource = adPrints;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listAdCampaign.ItemsSource);
            view.Filter = AdCampaginFilter;
        }
        private bool AdCampaginFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchTxtBox.Text))
            {
                return true;
            }
            else
            {
                if (cbbSearchBy.SelectedIndex == 0)
                    return (item as AdCampaignPrint).adsID.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 1)
                    return (item as AdCampaignPrint).campaignName.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 2)
                    return (item as AdCampaignPrint).DateStart.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 3)
                    return (item as AdCampaignPrint).DateEnd.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 4)
                    return (item as AdCampaignPrint).type.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (cbbSearchBy.SelectedIndex == 5)
                    return (item as AdCampaignPrint).StaffID.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                else
                    return true;
            }
        }
        private void SearchTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listAdCampaign.ItemsSource).Refresh();
        }

        private void listAdCampaign_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listAdCampaign.SelectedItem == null)
                return;
            AdCampaignPrint adCampaign = listAdCampaign.SelectedItems[0] as AdCampaignPrint;
            if (adCampaign != null)
            {
                AdsDetail_View invoice_View = new AdsDetail_View(adCampaign);
                invoice_View.ShowDialog();
            }
        }

        private void SearchTxtBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
