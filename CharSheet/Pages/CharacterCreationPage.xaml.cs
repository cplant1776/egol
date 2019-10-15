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
using CharSheet.classes;
using static CharSheet.classes.AttributeRow;
using static CharSheet.classes.SkillRow;

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
            SetButtonEvents();
        }

        private void GenerateAttributeRows()
        {
            foreach (KeyValuePair<int, int> entry in mainWindow.CurrentCharacter.AttributeValue)
            {
                // Create row from current character's values
                var newRow = new AttributeRow(DataHandler.getAttributeDesc(entry.Key), entry.Value);
                AttributeStack.Children.Add(newRow.GenerateAttributeRow());
            };
        }

        private void GenerateSkillRows()
        {
            // Iterate through skill dictionary
            foreach (KeyValuePair<int, int> entry in mainWindow.CurrentCharacter.SkillValue)
            {
                // Create row from current character's values
                var newRow = new SkillRow(DataHandler.getSkillDesc(entry.Key), entry.Value);
                SkillStack.Children.Add(newRow.GenerateSkillRow());
            };
        }

        private void SetButtonEvents()
        {
            BindAttributeButtons();
            BindSkillButtons();
        }

        private void BindAttributeButtons()
        {
            // Iterate through each row
            foreach (Grid g in AttributeStack.Children)
            {

                // Find plus and minus buttons
                AttributeModifierButton plusBtn = g.Children.OfType<AttributeModifierButton>().Where(i => Grid.GetColumn(i) == 3).FirstOrDefault();
                AttributeModifierButton minusBtn = g.Children.OfType<AttributeModifierButton>().Where(i => Grid.GetColumn(i) == 4).FirstOrDefault();

                // Bind their click event
                plusBtn.Click += Plus_Click;
                minusBtn.Click += Minus_Click;
            }
        }

        private void BindSkillButtons()
        {
            // Iterate through each row
            foreach (Grid g in SkillStack.Children)
            {

                // Find plus and minus buttons
                SkillModifierButton plusBtn = g.Children.OfType<SkillModifierButton>().Where(i => Grid.GetColumn(i) == 3).FirstOrDefault();
                SkillModifierButton minusBtn = g.Children.OfType<SkillModifierButton>().Where(i => Grid.GetColumn(i) == 4).FirstOrDefault();

                // Bind their click event
                plusBtn.Click += Plus_Click;
                minusBtn.Click += Minus_Click;
            }
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            Grid sendingGrid = (sender as Button).Parent as Grid;
            TextBlock elementValue = sendingGrid.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 2).FirstOrDefault();

            // Increment displayed value
            elementValue.Text = (Convert.ToInt32(elementValue.Text) + 1).ToString();

        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            Grid sendingGrid = (sender as Button).Parent as Grid;
            TextBlock elementValue = sendingGrid.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 2).FirstOrDefault();

            int iElementValue = Convert.ToInt32(elementValue.Text);

            // Decrement displayed value (cant go below 0)
            if (iElementValue > 0)
            {
                elementValue.Text = (iElementValue - 1).ToString();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.NavigateTo(AppSettings.pagePaths["Start"], this.NavigationService);
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            // Set attribute values
            foreach (Grid attributeRow in AttributeStack.Children)
            {
                // Get attribute name & value
                string attributeName = attributeRow.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 1).First().Text;
                int attributeValue = Convert.ToInt32(attributeRow.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 2).First().Text);

                // Update atrtibute value on current character
                int attributeId = DataHandler.getAttributeId(attributeName);
                mainWindow.CurrentCharacter.AttributeValue[attributeId] = attributeValue;
            }

            // Set skill values
            foreach (Grid skillRow in SkillStack.Children)
            {
                // Get attribute name & value
                string skillName = skillRow.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 1).First().Text;
                int skillValue = Convert.ToInt32(skillRow.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 2).First().Text);

                // Update atrtibute value on current character
                int skillId = DataHandler.getSkillId(skillName);
                mainWindow.CurrentCharacter.SkillValue[skillId] = skillValue;
            }

            // Set name
            mainWindow.CurrentCharacter.Name = CharacterName.Text;
            // Description
            mainWindow.CurrentCharacter.Description = CharacterDescription.Text;

            // Go to dashboard
            mainWindow.NavigateTo(AppSettings.pagePaths["Dashboard"], this.NavigationService);
        }
    }
}
