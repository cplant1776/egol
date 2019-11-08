using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Engine.ViewModels
{
    public class ErrorDialogViewModel : BaseViewModel
    {

        #region Fields
        private string _errorMessage;
        private string _errorTitle;

        private ICommand _doneCommand;
        #endregion

        #region Constructors
        public ErrorDialogViewModel()
        {

        }

        public ErrorDialogViewModel(string msg, string title)
        {
            ErrorMessage = msg;
            ErrorTitle = title;
        }
        #endregion

        #region Public Properties/Commands
        public string ErrorMessage { get { return _errorMessage; } set { _errorMessage = value; OnPropertyChanged("ErrorMessage"); } }
        public string ErrorTitle { get { return _errorTitle; } set { _errorTitle = value; OnPropertyChanged("ErrorTitle"); } }

        public ICommand DoneCommand
        {
            get
            {
                if (_doneCommand == null)
                {
                    _doneCommand = new RelayCommand(
                        param => Done()
                    );
                }
                return _doneCommand;
            }
        }
        #endregion

        #region Methods
        private void Done()
        {
            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
        }
        #endregion

    }
}
