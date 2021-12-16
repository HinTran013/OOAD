using DigitalPhotographyManagementSystem.UserControls.Marketing;
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
    /// Interaction logic for IdeaDetail_View.xaml
    /// </summary>
    public partial class IdeaDetail_View : Window
    {
        private IdeaPrint ideaPrint;
        public IdeaDetail_View(IdeaPrint idea)
        {
            InitializeComponent();
            ideaPrint = idea;
            IdeaSubjTxt.Text = idea.ideaSubject;
            DescTxt.Text = idea.desc;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
