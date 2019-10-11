using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CharSheet.classes
{
    public class AttributeRow
    {
        public String AttributeName { get; set; }
        public String PlusIconPath { get; set; }
        public String MinusIconPath { get; set; }
        public int AttributeValue { get; set; }
        public int GridRow { get; set; }

        public AttributeRow(String name, int val)
        {
            AttributeName = name;
            AttributeValue = val;

            PlusIconPath = "../../media/icons/Add_grey_16xMD.png";
            MinusIconPath = "../../media/icons/Subtract_16x.png";
        }

        public Grid GenerateAttributeRow()
        {
            // Create grid
            Grid attrGrid = new Grid()
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
            attrGrid.ColumnDefinitions.Add(colDef1);
            attrGrid.ColumnDefinitions.Add(colDef2);
            attrGrid.ColumnDefinitions.Add(colDef3);
            attrGrid.ColumnDefinitions.Add(colDef4);
            attrGrid.ColumnDefinitions.Add(colDef5);
            attrGrid.ColumnDefinitions.Add(colDef6);

            // Attr Name TextBlock
            TextBlock attrName = new TextBlock
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                FontSize = 16,
                Text = AttributeName
            };
            Grid.SetColumn(attrName, 1);

            // Attr Value TextBlock
            TextBlock attrVal = new TextBlock()
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                FontSize = 20,
                Text = AttributeValue.ToString()
            };
            Grid.SetColumn(attrVal, 2);

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
            attrGrid.Children.Add(attrName);
            attrGrid.Children.Add(attrVal);
            attrGrid.Children.Add(plusBtn);
            attrGrid.Children.Add(minusBtn);

            return attrGrid;
        }

        public Grid GenerateAttributeDisplayRow()
        {
            // Create grid
            Grid attrGrid = new Grid()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            // Col Definitions
            ColumnDefinition colDef1 = new ColumnDefinition
            {
                Width = new GridLength(4, GridUnitType.Star)
            };
            ColumnDefinition colDef2 = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
           
            attrGrid.ColumnDefinitions.Add(colDef1);
            attrGrid.ColumnDefinitions.Add(colDef2);

            // Attr Name TextBlock
            TextBlock attrName = new TextBlock
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                FontSize = 16,
                Text = AttributeName
            };
            Grid.SetColumn(attrName, 0);

            // Attr Value TextBlock
            TextBlock attrVal = new TextBlock()
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                FontSize = 20,
                Text = AttributeValue.ToString()
            };
            Grid.SetColumn(attrVal, 1);

            // Add Elements to Row
            attrGrid.Children.Add(attrName);
            attrGrid.Children.Add(attrVal);

            return attrGrid;
        }
    }
}
