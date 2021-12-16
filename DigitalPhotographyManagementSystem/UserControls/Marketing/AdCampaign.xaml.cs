using BUS;
using DigitalPhotographyManagementSystem.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DigitalPhotographyManagementSystem.UserControls.Marketing
{
    /// <summary>
    /// Interaction logic for AdCampaign.xaml
    /// </summary>
    public partial class AdCampaign : UserControl
    {
        static AdCampaign __instance = null;
        private staffDTO Account;
        public AdCampaign(staffDTO account)
        {
            InitializeComponent();
            DateTimeTxt.Text = "Date time: " + DateTime.Now.ToString("dd/MM/yyyy");
            Account = account;
        }
        private bool CheckInput()
        {
            if (AdsCampNameTxt.Text.Equals(""))
            {
                MsgBox.Show("Warning", "Campaign's name field is empty.", MessageBoxButton.OK, MessageBoxImg.Warning);
                AdsCampNameTxt.Focus();
                return false;
            }
            if (AdsCampTypeCbb.SelectedItem == null)
            {
                MsgBox.Show("Warning", "Campaign's type combobox is not selected.", MessageBoxButton.OK, MessageBoxImg.Warning);
                AdsCampTypeCbb.IsDropDownOpen = true;
                return false;
            }
            if (StartDate.SelectedDate == null)
            {
                MsgBox.Show("Warning", "Campaign's type combobox is not selected.", MessageBoxButton.OK, MessageBoxImg.Warning);
                StartDate.IsDropDownOpen = true;
                return false;
            }
            if (EndDate.SelectedDate == null)
            {
                MsgBox.Show("Warning", "Campaign's type combobox is not selected.", MessageBoxButton.OK, MessageBoxImg.Warning);
                EndDate.IsDropDownOpen = true;
                return false;
            }
            return true;
        }
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput() == true)
            {
                adCampaignDTO adCampaign = new adCampaignDTO(AdsCampNameTxt.Text, ((DateTime)StartDate.SelectedDate).ToString("dd/MM/yyyy"), ((DateTime)EndDate.SelectedDate).ToString("dd/MM/yyyy"), AdsCampTypeCbb.SelectedValue.ToString(), Account.username, DescTxt.Text);
                if (adCampaignBUS.AddNewAdCampaign(adCampaign))
                {
                    MsgBox.Show("Campaign successfully submitted!", MessageBoxTyp.Information);
                    AdsCampNameTxt.Text = "";
                    DescTxt.Text = "";
                    AdsCampTypeCbb.SelectedItem = null;
                    StartDate.SelectedDate = null;
                    EndDate.SelectedDate = null;
                    AdsCampNameTxt.Focus();
                }
                else
                    MsgBox.Show("Error while submitting, please try again!", MessageBoxTyp.Information);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            AdsCampNameTxt.Text = "";
            DescTxt.Text = "";
            AdsCampTypeCbb.SelectedItem = null;
            StartDate.SelectedDate = null;
            EndDate.SelectedDate = null;
        }

        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EndDate.SelectedDate != null)
            {
                if (StartDate.SelectedDate == null)
                {
                    MsgBox.Show("Warning","Must have Start Date first!", MessageBoxButton.OK, MessageBoxImg.Warning);
                    EndDate.SelectedDate = null;
                    StartDate.Focus();
                }
                else if (StartDate.SelectedDate > EndDate.SelectedDate)
                {
                    MsgBox.Show("Warning","End Date must be the same or later than Start Date.", MessageBoxButton.OK, MessageBoxImg.Warning);
                    EndDate.SelectedDate = null;
                    EndDate.Focus();
                }
            }
        }
        public static AdCampaign GetInstance(staffDTO acc)
        {
            if (__instance == null) __instance = new AdCampaign(acc);
            return __instance;
        }
        public static void Release()
        {
            __instance = null;
        }
    }
}
