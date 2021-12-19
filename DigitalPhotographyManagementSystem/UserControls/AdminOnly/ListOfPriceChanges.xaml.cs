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
using DTO;
using BUS;
using DigitalPhotographyManagementSystem.View;

namespace DigitalPhotographyManagementSystem.UserControls.AdminOnly
{
    /// <summary>
    /// Interaction logic for ListOfPriceChanges.xaml
    /// </summary>
    /// 
    public class services
    {
        public int No { get; set; }
        public ObjectId fullID { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string description { get; set; }
    }

    public partial class ListOfPriceChanges : UserControl
    {
        private ObservableCollection<services> showServices;

        public ListOfPriceChanges()
        {
            InitializeComponent();

            List<servicesDTO> services = new List<servicesDTO>();
            services = servicesBUS.GetAllServices();
            int i = 1;

            showServices = new ObservableCollection<services>();

            foreach (servicesDTO item in services)
            {
                var newShowService = new services()
                {
                    No = i++,
                    name = item.name,
                    fullID = (ObjectId)item.serviceID,
                    price = item.price,
                    description = item.description
                };
                showServices.Add(newShowService);
            }

            listService.ItemsSource = showServices;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listService.ItemsSource);
        }

        private void confirmChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            var rowItem = (sender as Button).DataContext as services;
            servicesDTO newService = new servicesDTO(
                rowItem.fullID,
                rowItem.name,
                rowItem.price,
                rowItem.description);
            servicesBUS.ReplaceOneService(newService);
            var messageBoxResult = MsgBox.Show("Success", "Change service information successfully!", MessageBoxTyp.Information);
        }
    }
}
