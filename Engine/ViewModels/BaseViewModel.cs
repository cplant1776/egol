using Engine.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Engine.ViewModels
{

        public class BaseViewModel : MyObservableObject
        {
            private Dictionary<string, int> _pageIndex = new Dictionary<string, int>
            {
                { "Start", 0 },
                { "Character Creation", 1 },
                { "Dashboard", 2 }
            };

        private static CharacterModel _userCharacter;
            static BaseViewModel()
            {
                _userCharacter = new CharacterModel();
            }

            public CharacterModel UserCharacter
            {
                get { return _userCharacter; }
                set
                {
                    if(value != _userCharacter)
                        {
                            _userCharacter = value;
                            OnPropertyChanged("UserCharacter");
                        }
                }
            }

        protected void NavigateTo(string viewName)
        {
            Window mainWindow = (Window)Application.Current.MainWindow;
            ApplicationViewModel viewModel = (ApplicationViewModel)mainWindow.DataContext;
            if (viewModel.ChangePageCommand.CanExecute(viewModel.PageViewModels[_pageIndex[viewName]]))
            {
                viewModel.ChangePageCommand.Execute(viewModel.PageViewModels[_pageIndex[viewName]]);
            }
        }
    }
}
