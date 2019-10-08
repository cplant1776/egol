using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CharSheet.objects
{
    public class SkillRow
    {
        public String SkillName { get; set; }
        public String PlusIconPath { get; set; }
        public String MinusIconPath { get; set; }
        public int SkillValue { get; set; }
        public int GridRow { get; set; }

        public SkillRow(String name, int val)
        {
            SkillName = name;
            SkillValue = val;

            PlusIconPath = "../../media/icons/Add_grey_16xMD.png";
            MinusIconPath = "../../media/icons/Subtract_16x.png";
        }

        public Grid GenerateSkillRow()
        {
            // Create grid
            Grid skillGrid = new Grid()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            // Col Definitions
            ColumnDefinition colDef1 = new ColumnDefinition
            {
                Width = new GridLength(1.4, GridUnitType.Star)
            };
            ColumnDefinition colDef2 = new ColumnDefinition
            {
                Width = new GridLength(2, GridUnitType.Star)
            };
            ColumnDefinition colDef3 = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            ColumnDefinition colDef4 = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            ColumnDefinition colDef5 = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            ColumnDefinition colDef6 = new ColumnDefinition
            {
                Width = new GridLength(1.4, GridUnitType.Star)
            };
            skillGrid.ColumnDefinitions.Add(colDef1);
            skillGrid.ColumnDefinitions.Add(colDef2);
            skillGrid.ColumnDefinitions.Add(colDef3);
            skillGrid.ColumnDefinitions.Add(colDef4);
            skillGrid.ColumnDefinitions.Add(colDef5);
            skillGrid.ColumnDefinitions.Add(colDef6);

            // Skill Name TextBlock
            TextBlock skillName = new TextBlock
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                FontSize = 16,
                Text = SkillName,
            };
            Grid.SetColumn(skillName, 1);

            // skill Value TextBlock
            TextBlock skillVal = new TextBlock()
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                FontSize = 20,
                Text = SkillValue.ToString()
            };
            Grid.SetColumn(skillVal, 2);

            // Plus Button
            Image plusImg = new Image
            {
                Source = new System.Windows.Media.Imaging.BitmapImage(
                 new Uri(PlusIconPath, UriKind.RelativeOrAbsolute))
            };
            Button plusBtn = new Button()
            {
                Margin = new System.Windows.Thickness(5, 5, 5, 5),
                Content = plusImg
            };

            Grid.SetColumn(plusBtn, 3);

            // Minus Button
            Image minusImg = new Image
            {
                Source = new System.Windows.Media.Imaging.BitmapImage(
                new Uri(MinusIconPath, UriKind.RelativeOrAbsolute))
            };
            Button minusBtn = new Button()
            {
                Margin = new System.Windows.Thickness(5, 5, 5, 5),
                Content = minusImg

            };
            Grid.SetColumn(minusBtn, 4);

            // Add Elements to Row
            skillGrid.Children.Add(skillName);
            skillGrid.Children.Add(skillVal);
            skillGrid.Children.Add(plusBtn);
            skillGrid.Children.Add(minusBtn);

            return skillGrid;
        }
    }
}
