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
using System.Windows.Shapes;

using DTO;
using DAL;

namespace DigitalPhotographyManagementSystem.View
{
    /// <summary>
    /// Interaction logic for AdsList.xaml
    /// </summary>
    public partial class AdsList : Window
    {
        
        public AdsList()
        {
            InitializeComponent();

            
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DetailBtn_Click(object sender, RoutedEventArgs e)
        {
            AdsDetail_View detail = new AdsDetail_View();
            detail.ShowDialog();
        }
    }
}