using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BUS;
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
        public ISeries[] IssueSeries { get; set; }
            = new ISeries[]
            {
                new PieSeries<double>
                {
                    Name = "Solved Issues",
                    Values = new double[] { issueReportBUS.CountAllSolvedIssues() },
                    Fill = new SolidColorPaint(SKColors.DarkGreen),
                    Stroke = new SolidColorPaint(SKColors.White) { StrokeThickness = 3 },
                    HoverPushout = 30
                },
                new PieSeries<double>
                {
                    Name = "Unsolved Issues",
                    Values = new double[] { issueReportBUS.CountAllUnsolvedIssues() },
                    Fill = new SolidColorPaint(SKColors.Red),
                    Stroke = new SolidColorPaint(SKColors.White) { StrokeThickness = 3 },
                    HoverPushout = 30
                }
            };
        public ISeries[] InvoiceSeries { get; set; }
            = new ISeries[]
            {   
                new PieSeries<double>
                {
                    Name = "Finished Invoices",
                    Values = new double[] { 7 },
                    Fill = new SolidColorPaint(SKColors.DarkGreen),
                    Stroke = new SolidColorPaint(SKColors.White) { StrokeThickness = 3 },
                    HoverPushout = 30
                },
                new PieSeries<double>
                {
                    Name = "Unfinished Invoices",
                    Values = new double[] { 3 },
                    Fill = new SolidColorPaint(SKColor.Parse("F2B705")),
                    Stroke = new SolidColorPaint(SKColors.White) { StrokeThickness = 3 },
                    HoverPushout = 30,
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
