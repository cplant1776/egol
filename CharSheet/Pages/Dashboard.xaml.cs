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
            List<HistoryEntry> entries = new List<HistoryEntry>
            {
                new HistoryEntry("Description 1 is going here. Woohoo!"),
                new HistoryEntry("Description 2 is going here. Woohoo!"),
                new HistoryEntry("Description 3 is going here. Woohoo!"),
                new HistoryEntry("Description 4 is going here. Woohoo!"),
                new HistoryEntry("Description 5 is going here. Woohoo!"),
                new HistoryEntry("Description 6 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!"),
                new HistoryEntry("Description 7 is going here. Woohoo!")
            };

            foreach(HistoryEntry e in entries)
            {
                HistoryStack.Children.Add(e.GenerateHistoryEntryTextBlock());
            }
        }

        private void GenerateAttributeRows()
        {
            List<AttributeRow> rows = new List<AttributeRow>
            {
                new AttributeRow("Constitution", 99),
                new AttributeRow("Strength", 99),
                new AttributeRow("Agility", 99),
                new AttributeRow("Intelligence", 99),
                new AttributeRow("Wisdom", 99),
                new AttributeRow("Charisma", 99),
                new AttributeRow("Luck", 99)
            };

            foreach (AttributeRow r in rows)
            {
                AttributeStack.Children.Add(r.GenerateAttributeRow());
            }
        }

        private void GenerateSkillRows()
        {
            List<SkillRow> rows = new List<SkillRow>
            {
                new SkillRow("Skill1", 99),
                new SkillRow("Skill2", 99),
                new SkillRow("Skill3", 99),
                new SkillRow("Skill4", 99),
                new SkillRow("Skill5", 99),
                new SkillRow("Skill6", 99),
                new SkillRow("Skill17", 99),
                new SkillRow("Skill18", 99),
                new SkillRow("Skill19", 99),
                new SkillRow("Skill110", 99),
                new SkillRow("Skill111", 99),
                new SkillRow("Skill112", 99),
                new SkillRow("Skill113", 99)
            };

            foreach (SkillRow r in rows)
            {
                SkillStack.Children.Add(r.GenerateSkillRow());
            }
        }

        public void GenerateSkillDropdown()
        {
            List<String> skills = new List<String>
            {
                "Skill1",
                "Skill2",
                "Skill3",
                "Skill4",
                "Skill5",
                "Skill6",
                "Skill7",
                "Skill8",
                "Skill9",
                "Skill10",
                "Skill11",
                "Skill12",
                "Skill13"
            };

            SkillDropdown.ItemsSource = skills;
        }
    }
}
