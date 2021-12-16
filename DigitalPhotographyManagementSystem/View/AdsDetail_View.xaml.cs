using DigitalPhotographyManagementSystem.UserControls.Marketing;
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
    /// Interaction logic for AdsDetail_View.xaml
    /// </summary>
    public partial class AdsDetail_View : Window
    {
        private AdCampaignPrint adCampaign;
        public AdsDetail_View(AdCampaignPrint adcampaign)
        {
            InitializeComponent();
            adCampaign = adcampaign;
            CampaignNameTxt.Text = adCampaign.campaignName;
            CampaignTypeTxt.Text = adCampaign.type;
            DateEndTxt.Text = adCampaign.DateEnd;
            DateStartTxt.Text = adCampaign.DateStart;
            DescTxt.Text = adCampaign.desc;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
