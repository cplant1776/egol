using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace CharSheet
{
    /// <summary>
    /// Interaction logic for LevelUpPage.xaml
    /// </summary>
    public partial class LevelUpPage : Page, INotifyPropertyChanged
    {

        public MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public LevelUpPage()
        {
            InitializeComponent();

            GenerateAttributeRows();
        }

        private void GenerateAttributeRows()
        {
            foreach (KeyValuePair<int, int> entry in mainWindow.currentCharacter.attributeValue)
            {
                // Create row from current character's values
                var newRow = new AttributeRow(DataHandler.getAttributeDesc(entry.Key), entry.Value);
                AttributeStack.Children.Add(newRow.GenerateAttributeRow());
            };
        }

    }
}
