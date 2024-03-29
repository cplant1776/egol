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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Engine.ViewModels;

namespace Egol.Views
{
    /// <summary>
    /// Interaction logic for QuestLogView.xaml
    /// </summary>
    public partial class QuestLogView : UserControl
    {
        private readonly QuestLogViewModel _viewModel = new QuestLogViewModel();
        public QuestLogView()
        {
            DataContext = _viewModel;
            InitializeComponent();
        }

        /*
         * SelectedItem is readonly in a tree view, so cannot bind directly to its value in the view model
         * 
         */
        private void UpdateSelectedQuest(object sender, MouseButtonEventArgs args)
        {
            this._viewModel.UpdateSelectedQuest(sender);
        }
    }
}
