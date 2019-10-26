using CharSheet.classes.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CharSheet.classes.display
{
    public class QuickMenuActions
    {

        MainWindow mainWindow = (MainWindow) Application.Current.MainWindow;

        public QuickMenuActions()
        {

        }

        public void Save(Character character)
        {
            if (AppSettings.SaveDestination == null)
            {
                SaveAs();
            }
            else
            {
                DataHandler.SaveToXml(character, AppSettings.SaveDestination);
            }
        }

        public void SaveAs()
        {

        }

        public void Load()
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
                string filename = dlg.FileName;
                mainWindow.Load(filename); // Set main window's character to loaded character
            }
            // Navigate to dashboard
            //NavigateToPage("Dashboard");
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
