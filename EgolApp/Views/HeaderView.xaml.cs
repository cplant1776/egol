using Engine.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Hephaestus.Views
{
    /// <summary>
    /// Interaction logic for HeaderView.xaml
    /// </summary>
    public partial class HeaderView : UserControl, INotifyPropertyChanged
    {
        private HeaderViewModel _viewModel;
        public HeaderView()
        {
            _viewModel = new HeaderViewModel(backButtonIsVisible: BackButtonIsVisible,
                quickMenuButtonIsVisible: QuickMenuButtonIsVisible);
            DataContext = _viewModel;
            InitializeComponent();

        }

        public static readonly DependencyProperty IsSpinningProperty =
            DependencyProperty.Register(
            "IsSpinning", typeof(bool),
            typeof(HeaderView), 
            new PropertyMetadata(false, ValueChanged));

        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HeaderView control = (HeaderView)d;
            if (control.IsSpinning)
                control.IsSpinning = true;
            else
                control.IsSpinning = false;
        }

        public bool IsSpinning
        {
            get { return (bool)GetValue(IsSpinningProperty); }
            set { SetValue(IsSpinningProperty, value); }
        }

        public string BackButtonIsVisible
        {
            get { return (string)GetValue(BackButtonIsVisibleProperty); }
            set { SetValue(BackButtonIsVisibleProperty, value); OnPropertyChanged("BackButtonIsVisible"); }
        }

        // Using a DependencyProperty as the backing store for BackButtonIsVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackButtonIsVisibleProperty =
            DependencyProperty.Register("BackButtonIsVisible",
                typeof(string),
                typeof(HeaderView),
                new PropertyMetadata("balls"));

        public string QuickMenuButtonIsVisible
        {
            get { return (string)GetValue(QuickMenuButtonIsVisibleProperty); }
            set { SetValue(QuickMenuButtonIsVisibleProperty, value); OnPropertyChanged("QuickMenuButtonIsVisible"); }
        }

        // Using a DependencyProperty as the backing store for QuickMenuButtonIsVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QuickMenuButtonIsVisibleProperty =
            DependencyProperty.Register("QuickMenuButtonIsVisible",
                typeof(string),
                typeof(HeaderView),
                new PropertyMetadata("False"));


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, e);
            }
        }
    }
}
