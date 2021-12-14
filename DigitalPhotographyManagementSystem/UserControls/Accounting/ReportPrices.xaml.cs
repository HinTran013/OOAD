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
using DigitalPhotographyManagementSystem.View;
using System.Text.RegularExpressions;

namespace DigitalPhotographyManagementSystem.UserControls.Accounting
{
    /// <summary>
    /// Interaction logic for ReportPrices.xaml
    /// </summary>
    /// 

    public class NewServicePrice
    {
        public long No { get; set; }

        public string Type { get; set; }
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }
    }

    public partial class ReportPrices : UserControl
    {
        bool isNewForm = true;

        reportPricesDTO reportForm;

        List<NewServicePrice> types;
        long NoType = 0;
        DateTime timeNow;

        List<servicesDTO> services;

        private staffDTO accountStaff;

        public ReportPrices(staffDTO staff = null)
        {
            InitializeComponent();

            
            timeNow = DateTime.Now;
            DateTimeTxt.Text = "Date time: " + timeNow.ToString("dd/MM/yyyy");

            SetTypes();
            accountStaff = staff;

            ResetForm();
        }

        private void SetTypes()
        {
            services = servicesBUS.GetAllServices();

            foreach (servicesDTO item in services)
            {
                TypeCbb.Items.Add(item.name);
            }
        }


        private bool CheckInputs()
        {
            if (String.IsNullOrEmpty(TypeCbb.Text))
            {
                var messageBoxResult = MsgBox.Show("Warning",
                                                    "Please select a Photo Type",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImg.Warning);
                return false;
            }
            else if (String.IsNullOrEmpty(OldTxt.Text))
            {
                var messageBoxResult = MsgBox.Show("Warning",
                                                    "Please fill the old price of the Photo Type",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImg.Warning);
                return false;
            }
            else if (String.IsNullOrEmpty(NewTxt.Text))
            {
                var messageBoxResult = MsgBox.Show("Warning",
                                                    "Please fill the new price of the Photo Type",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImg.Warning);
                return false;
            }

            return true;
        }

        private bool CheckForm()
        {
            if (String.IsNullOrEmpty(SubjectTxt.Text))
            {
                var messageBoxResult = MsgBox.Show("Warning",
                                                    "Please fill in the Subject of Report Price",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImg.Warning);
                return false;
            }

            //if (CheckInputs()) return false;

            return true;
        }

        private void ResetInputs()
        {
            NewTxt.Text = String.Empty;
            OldTxt.Text = String.Empty;
            TypeCbb.SelectedItem = null;
        }

        private void ResetForm()
        {
            ResetInputs();
            SubjectTxt.Text = String.Empty;

            NoType = 1;
            listTypes.Items.Clear();
            types = new List<NewServicePrice>();
        }

        private bool LockInputs()
        {
            isNewForm = false;
            return isNewForm;
        }

        private bool UnlockInputs()
        {
            isNewForm = true;
            return isNewForm;
        }

        private void AddForm()
        {
            if (isNewForm == true)
            {
                LockInputs();

                List<servicesDTO> listNew = new List<servicesDTO>();
                List<double> priceList = new List<double>();
                foreach(NewServicePrice item in types)
                {
                    listNew.Add(new servicesDTO()
                    {
                        serviceID = null,
                        name = item.Type,
                        price = item.OldPrice
                    });
                    priceList.Add(item.NewPrice);
                }

                reportForm = new reportPricesDTO(
                    timeNow.ToString("dd/MM/yyyy"),
                    SubjectTxt.Text,
                    listNew,
                    priceList,
                    accountStaff.username);

                reportPricesBUS.AddReportPrices(reportForm);
            }
        }

        //private void AddNewPrices()
        //{
        //    List<reportPricesDetailDTO> listNew = new List<reportPricesDetailDTO>();

        //    foreach (NewServicePrice item in types)
        //    {
        //        listNew.Add(new reportPricesDetailDTO(
        //            reportForm.reportPriceID,
        //            item.Type,
        //            item.OldPrice,
        //            item.NewPrice));
        //    }

        //    foreach (reportPricesDetailDTO item in listNew)
        //    {
        //        reportPricesDetailBUS.AddNewReportPrice(item);
        //    }
        //}

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if(CheckInputs())
            {
                types.Add(new NewServicePrice()
                {
                    No = NoType++,
                    Type = TypeCbb.Text,
                    OldPrice = double.Parse(OldTxt.Text.ToString()),
                    NewPrice = double.Parse(NewTxt.Text.ToString())
                });

                listTypes.Items.Add(types[types.Count - 1]);

                ResetInputs();
            }
        }

        private void ReportBtn_Click(object sender, RoutedEventArgs e)
        {
            if(types.Count == 0)
            {
                var messageBoxResult = MsgBox.Show("Warning",
                                                   "There is no types required to change!",
                                                   MessageBoxButton.OK,
                                                   MessageBoxImg.Warning);
                return;
            }

            if(CheckForm())
            {
                AddForm();

                //AddNewPrices();

                var messageBoxResult = MsgBox.Show(
                    "Notification",
                    "Save successful",
                    MessageBoxButton.OK,
                    MessageBoxImg.None);

                ResetForm();
                ResetInputs();
                UnlockInputs();

            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            UnlockInputs();
            ResetForm();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(sender.ToString());
            //Console.WriteLine(sender.GetType().ToString()); 
            //var btn = sender as Button;
            //Console.WriteLine(btn.Name.ToString());
            //btn.Name = "selectedBtn";

            var selected = listTypes.SelectedItem;

            //listTypes.Items.IndexOf(selected)

            if(selected != null)
            {
                types.RemoveAt(listTypes.SelectedIndex);
                listTypes.Items.Remove(selected);
            }
        }

        private void TxtNum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //To limit the characters which are the numbers in the textbox
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TypeCbb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeCbb.SelectedItem == null) return;

            var selected = TypeCbb.SelectedIndex;

            OldTxt.Text = services[selected].price.ToString();
        }
    }
}
