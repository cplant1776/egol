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
using System.Windows.Shapes;

namespace CharSheet
{
    /// <summary>
    /// Interaction logic for LevelUpWindow.xaml
    /// </summary>
    public partial class LevelUpWindow : Window
    {
        public int numOfLevels;
        public Dictionary<int, int> AttributeValue;
        public Dictionary<int, int> SkillValue;

        public LevelUpWindow()
        {
            InitializeComponent();
        }

        public LevelUpWindow(int numOfLevels, Dictionary<int, int> attributeValues, Dictionary<int, int> skillValues)
        {
            InitializeComponent();

            this.numOfLevels = numOfLevels;
            this.AttributeValue = attributeValues;
            this.SkillValue = skillValues;
        }
    }
}
