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
            List<AttributeRow> rows = new List<AttributeRow>
            {
                new AttributeRow("Constitution", 99, 2),
                new AttributeRow("Strength", 99, 1),
                new AttributeRow("Agility", 99, 2),
                new AttributeRow("Intelligence", 99, 3),
                new AttributeRow("Wisdom", 99, 4),
                new AttributeRow("Charisma", 99, 5),
                new AttributeRow("Luck", 99, 6)
            };

            AttributeRows.ItemsSource = rows;
        }

    }
}
