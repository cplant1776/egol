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
using CharSheet.objects;

namespace CharSheet.Pages
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {

        public MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public StartPage()
        {

        }

        public void NewCharacter_Click(object sender, EventArgs e)
        {
            // Navigate to Dashboard
            mainWindow.NavigateTo(AppSettings.pagePaths["Dashboard"], NavigationService.GetNavigationService(this));
        }

        private void LoadCharacter_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                // Set filter for file extension and default file extension 
                DefaultExt = ".xml",
                Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*"

            };

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and load a character with it
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                mainWindow.Load(filename);
            }
            // Navigate to dashboard
            mainWindow.NavigateTo(AppSettings.pagePaths["Dashboard"], NavigationService.GetNavigationService(this));
        }
    }
}
