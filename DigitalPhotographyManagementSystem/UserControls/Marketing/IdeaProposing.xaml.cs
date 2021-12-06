using BUS;
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

namespace DigitalPhotographyManagementSystem.View
{
    /// <summary>
    /// Interaction logic for IdeaProposing.xaml
    /// </summary>
    public partial class IdeaProposing : UserControl
    {
        static IdeaProposing __instance = null;
        private staffDTO Account;
        public IdeaProposing(staffDTO account)
        {
            InitializeComponent();
            DateTimeTxt.Text = "Date time: " + DateTime.Now.ToString("dd/MM/yyyy");
            Account = account;
        }

        public static IdeaProposing GetInstance(staffDTO acc)
        {
            if (__instance == null) __instance = new IdeaProposing(acc);
            return __instance;
        }
        public static void Release()
        {
            __instance = null;
        }
        private bool CheckInput()
        {
            if (IdeaSubjTxt.Text.Equals(""))
            {
                MsgBox.Show("Warning", "Idea's subject field is empty", MessageBoxButton.OK, MessageBoxImg.Warning);
                IdeaSubjTxt.Focus();
                return false;
            }
            if (DescTxt.Text.Equals(""))
            {
                MsgBox.Show("Warning", "Idea's description field is empty", MessageBoxButton.OK, MessageBoxImg.Warning);
                DescTxt.Focus();
                return false;
            }
            return true;
        }
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput())
            {
                ideaDTO idea = new ideaDTO(IdeaSubjTxt.Text, DescTxt.Text, DateTime.Now.ToString("dd/MM/yyyy"), Account.username);
                if (ideaBUS.AddNewIdea(idea))
                {
                    MsgBox.Show("Idea successfully submitted!", MessageBoxTyp.Information);
                    IdeaSubjTxt.Text = "";
                    DescTxt.Text = "";
                    IdeaSubjTxt.Focus();
                }
                else
                    MsgBox.Show("Error while submitting, please try again!", MessageBoxTyp.Information);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            IdeaSubjTxt.Text = "";
            DescTxt.Text = "";
        }
    }
}
