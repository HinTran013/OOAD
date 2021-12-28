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
using System.Text.RegularExpressions;

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
        private int i = 1;
        public ListOfPriceChanges()
        {
            InitializeComponent();
            DateTimeTxt.Text = "Date time: " + DateTime.Now.ToString("dd/MM/yyyy");

            showInitData();
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

        private void showInitData()
        {
            i = 1;
            List<servicesDTO> services = new List<servicesDTO>();
            services = servicesBUS.GetAllServices();
            

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
            view.Filter = PriceFilter;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            services newService = new services()
            {
                No = i++,
                name = "",
                price = 0,
                description = ""
            };
            
            if(servicesBUS.InserNewService(newService.name, newService.price, newService.description))
            {
                
                showServices.Add(newService);
                showInitData();
                var messageBoxResult = MsgBox.Show("Success", "Add service successfully!", MessageBoxTyp.Information);
            }
            
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            int initTotalService = listService.Items.Count;

            if(listService.SelectedItems.Count != 0)
            {
                for (int i = listService.SelectedItems.Count - 1; i >= 0; i--)
                {
                    var selectedService = listService.SelectedItems[i] as services;

                    if (servicesBUS.DeleteOneServiceByID(selectedService.fullID))
                    {
                        showServices.Remove(selectedService);
                    }
                }
            }
            else
            {
                var messageBoxResult = MsgBox.Show("Warning", "Please select atleast one service to delete!", MessageBoxTyp.Warning);
            }

            if(initTotalService > listService.Items.Count)
            {
                var messageBoxResult = MsgBox.Show("Success", "Delete service successfully!", MessageBoxTyp.Information);
            }

            showInitData();
        }

        private void priceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //To limit the characters which are the numbers in the textbox
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private bool PriceFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchTxtBox.Text))
            {
                return true;
            }
            else
            {
                if (cbbSearchBy.SelectedIndex == 0)
                    return (item as services).name.IndexOf(SearchTxtBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                else
                    return true;
            }
        }

        private void SearchTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listService.ItemsSource).Refresh();
        }
    }
}
