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
using System.Windows.Navigation;

namespace ContactManagerUI
{
    /// <summary>
    /// Interaction logic for CreditsWindow.xaml
    /// </summary>
    public partial class CreditsWindow : Window
    {
        public CreditsWindow() => InitializeComponent();

        private void CreditsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            VersionNumberTextBlock.Text = $"v.{Utilities.AddVersionNumber()}";
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e) => System.Diagnostics.Process.Start(e.Uri.ToString());
    }
}
