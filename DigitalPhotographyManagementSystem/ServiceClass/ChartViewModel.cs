using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;

namespace DigitalPhotographyManagementSystem.ServiceClass
{
    class ChartViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<fundBillDTO> Fund
        {
            get;
            set;
        }

        public ObservableCollection<paymentBillDTO> RevExp
        {
            get;
            set;
        }
        public ChartViewModel()
        {
            RevExp = new ObservableCollection<paymentBillDTO>();
            Fund = new ObservableCollection<fundBillDTO>();
        }
        private object selectedItem = null;
        public object SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                NotifyPropertyChanged("SelectedItem");
            }
        }
        public IEnumerable<ISeries> RevExpSeries { get; set; } = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = new double[] { 2, 1, 3, 5, 3, 4, 10 },
                    Fill = null,
                    LineSmoothness = 1
                }
            };
        public ISeries[] IssueSeries { get; set; }
            = new ISeries[]
            {
                new PieSeries<double>
                {
                    Values = new double[] { 2 }
                },
                new PieSeries<double>
                {
                    Values = new double[] { 8 }
                }
            };
        public ISeries[] InvoiceSeries { get; set; }
            = new ISeries[]
            {
                new PieSeries<double>
                {
                    Values = new double[] { 3 }
                },
                new PieSeries<double>
                {
                    Values = new double[] { 7 }
                }
            };
        /*public void RevExpValues()
        {
            Dictionary<DateTime, int> SumMoney = paymentBillBUS.GetTotalMoney();
            foreach (var data in SumMoney)
            {
                RevExp[data.Key - new DateTime(1, 0, 0)].Bill = data.Value;
            }
        }*/
        public Axis[] XAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Name = "Date",
                    TextSize = 20
                }
            };

        public Axis[] YAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Labeler = (value) => "VNĐ " + value.ToString() + " M",
                    TextSize = 20
                }
            };
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
