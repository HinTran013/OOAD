using BUS;
using DigitalPhotographyManagementSystem.View;
using DTO;
using MongoDB.Bson;
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

namespace DigitalPhotographyManagementSystem.UserControls.AdminOnly
{
    /// <summary>
    /// Interaction logic for invoiceDetailWindow.xaml
    /// </summary>
    /// 
    public class reportModel
    {
        public int No { get; set; }
        public string serviceType { get; set; }
        public double oldPrice { get; set; }
        public double newPrice { get; set; }
    }

    public partial class invoiceDetailWindow : Window
    {
        ObjectId fullID;
        int i = 1;

        public invoiceDetailWindow()
        {
            InitializeComponent();
        }

        public invoiceDetailWindow(ObjectId ID)
        {
            InitializeComponent();
            SubjectTxt.IsEnabled = false;
            fullID = ID;
            reportPricesDTO report = reportPricesBUS.GetReportFromID(ID);

            SubjectTxt.Text = report.subject;
            DateTimeTxt.Text = "Date created: " + report.date;

            
            foreach (reportPricesDetailDTO item in report.reportDetails)
            {
                listTypes.Items.Add(new reportModel()
                {
                    No = i++,
                    serviceType = item.serviceType,
                    oldPrice = item.oldPrice,
                    newPrice = item.newPrice,
                });
            }
        }

        private void ShutdownBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void approveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var item in listTypes.Items)
                {
                    var itemValue = item as reportModel;

                    servicesBUS.UpdateServicePriceByName(itemValue.serviceType, itemValue.newPrice);
                }

                var messageBoxResult = MsgBox.Show("Success", "Approve successfully!", MessageBoxTyp.Information);

                changeReportState();
            }
            catch
            {
                var messageBoxResult = MsgBox.Show("Fail", "Approve failed!", MessageBoxTyp.Error);
            }
        }

        private void rejectBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                changeReportState();
                var messageBoxResult = MsgBox.Show("Success", "Reject successfully!", MessageBoxTyp.Information);
            }
            catch
            {
                var messageBoxResult = MsgBox.Show("Fail", "Reject failed!", MessageBoxTyp.Error);
            }
        }

        private void changeReportState()
        {
            reportPricesBUS.UpdateStateById(fullID, true);
            this.Close();
        }
    }
}
