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
using CharSheet.objects;

namespace CharSheet
{
    /// <summary>
    /// Interaction logic for CharacterCreation.xaml
    /// </summary>
    public partial class CharacterCreation : Page
    {

        public MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public CharacterCreation()
        {
            InitializeComponent();

            GenerateAttributeRows();
            GenerateSkillRows();
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

        private void GenerateSkillRows()
        {
            // Iterate through skill dictionary
            foreach (KeyValuePair<int, int> entry in mainWindow.currentCharacter.skillValue)
            {
                // Create row from current character's values
                var newRow = new SkillRow(DataHandler.getSkillDesc(entry.Key), entry.Value);
                SkillStack.Children.Add(newRow.GenerateSkillRow());
            };
        }
    }
}
