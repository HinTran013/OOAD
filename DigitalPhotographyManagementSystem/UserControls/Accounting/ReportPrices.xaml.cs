﻿using System;
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
            staffBUS.AddNewStaff(new staffDTO("2", "test", "1/1/2001", true, "hihi@gm.com", "234234234", "23tr", "Ai ma biet"));
        }
    }
}
