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
using System.Windows.Shapes;

namespace CharSheet
{
    /// <summary>
    /// Interaction logic for LevelUpWindow.xaml
    /// </summary>
    public partial class LevelUpWindow : Window
    {
        public int numOfLevels;
        public StackPanel result;
        public LevelUpWindow()
        {
            InitializeComponent();
        }

        public LevelUpWindow(int numOfLevels)
        {
            InitializeComponent();

            this.numOfLevels = numOfLevels;
        }
    }
}
