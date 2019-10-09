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

namespace CharSheet.Pages
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        public MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        public Dashboard()
        {
            InitializeComponent();

            GenerateAttributeRows();
            GenerateSkillRows();
            GenerateExpPlot();
            GenerateHistory();
            GenerateSkillDropdown();
        }

        private void GenerateExpPlot()
        {
            MyPlotter myPlotter = new MyPlotter();
            ExpPlot.Model = myPlotter.MyModel;
        }

        private void GenerateHistory()
        {
            foreach(HistoryEntry e in mainWindow.currentCharacter.eventHistory)
            {
                HistoryStack.Children.Add(e.GenerateHistoryEntryTextBlock());
            }
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

        public void GenerateSkillDropdown()
        {
            List<String> skills = new List<String> { };
            foreach (int skill in mainWindow.currentCharacter.skillValue.Keys)
            {
                // Create row from current character's values
                var skillDesc = DataHandler.getSkillDesc(skill);
                skills.Add(skillDesc);
            };

            SkillDropdown.ItemsSource = skills;
        }

        private void TESTER_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Load("../../saves/tester2.xml");
        }
    }
}
