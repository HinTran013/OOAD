using BUS;
using DigitalPhotographyManagementSystem.View;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CreateCoupon.xaml
    /// </summary>
    public partial class CreateCoupon : UserControl
    {
        static CreateCoupon __instance = null;
        public CreateCoupon()
        {
            InitializeComponent();
            DateTimeTxt.Text = "Date time: " + DateTime.Now.ToString("dd/MM/yyyy");
        }
        private bool CheckInput()
        {
            if (CouponCodeTxt.Text.Equals(""))
            {
                MsgBox.Show("Warning", "Coupon's code field is empty.", MessageBoxButton.OK, MessageBoxImg.Warning);
                CouponCodeTxt.Focus();
                return false;
            }
            if (CouponTitleTxt.Text.Equals(""))
            {
                MsgBox.Show("Warning", "Coupon's title field is empty.", MessageBoxButton.OK, MessageBoxImg.Warning);
                CouponTitleTxt.Focus();
                return false;
            }
            if (CouponPerTxt.Text.Equals(""))
            {
                MsgBox.Show("Warning", "Coupon's percentage off field is empty.", MessageBoxButton.OK, MessageBoxImg.Warning);
                CouponTitleTxt.Focus();
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
            }
            return true;
        }
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput())
            {
                /* adCampaignDTO adCampaign = new adCampaignDTO(AdsCampNameTxt.Text, ((DateTime)StartDate.SelectedDate).ToString("dd/MM/yyyy"), ((DateTime)EndDate.SelectedDate).ToString("dd/MM/yyyy"), AdsCampTypeCbb.SelectedValue.ToString(), Account.username);
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
                    MsgBox.Show("Error while submitting, please try again!", MessageBoxTyp.Information);*/
                couponDTO coupon = new couponDTO(CouponCodeTxt.Text, CouponTitleTxt.Text, ((DateTime)StartDate.SelectedDate).ToString("dd/MM/yyyy"), ((DateTime)EndDate.SelectedDate).ToString("dd/MM/yyyy"), float.Parse(CouponPerTxt.Text), DescTxt.Text);
                if (couponBUS.AddNewCoupon(coupon))
                {
                    MsgBox.Show("Coupon successfully submitted!", MessageBoxTyp.Information);
                    CouponCodeTxt.Text = "";
                    DescTxt.Text = "";
                    CouponPerTxt.Text = "";
                    StartDate.SelectedDate = null;
                    EndDate.SelectedDate = null;
                    CouponTitleTxt.Text = "";
                    CouponCodeTxt.Focus();
                }
                else
                    MsgBox.Show("Error while submitting, please try again!", MessageBoxTyp.Information);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            CouponCodeTxt.Text = "";
            DescTxt.Text = "";
            CouponPerTxt.Text = "";
            StartDate.SelectedDate = null;
            EndDate.SelectedDate = null;
            CouponTitleTxt.Text = "";
        }

        private void CouponPerTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EndDate.SelectedDate != null)
            {
                if (StartDate.SelectedDate == null)
                {
                    MsgBox.Show("Warning", "Must have Start Date first!", MessageBoxButton.OK, MessageBoxImg.Warning);
                    EndDate.SelectedDate = null;
                    StartDate.Focus();
                }
                else if (StartDate.SelectedDate > EndDate.SelectedDate)
                {
                    MsgBox.Show("Warning", "End Date must be the same or later than Start Date.", MessageBoxButton.OK, MessageBoxImg.Warning);
                    EndDate.SelectedDate = null;
                    EndDate.Focus();
                }
            }
        }
        public static CreateCoupon GetInstance()
        {
            if (__instance == null) __instance = new CreateCoupon();
            return __instance;
        }
        public static void Release()
        {
            __instance = null;
        }
    }
}
