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
using DTO;
using BUS;
namespace DigitalPhotographyManagementSystem.UserControls.Accounting
{
    /// <summary>
    /// Interaction logic for ReportPrices.xaml
    /// </summary>
    public partial class ReportPrices : UserControl
    {
        public ReportPrices()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            fundBillBUS.AddFundBill(new fundBillDTO("huhu", "13/8/2022", 2324234D, "asdsadasdas", "ai ma biet"));
        }
    }
}
