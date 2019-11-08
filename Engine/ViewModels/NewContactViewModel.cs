using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Engine.ViewModels
{
    public class NewContactViewModel : BaseViewModel
    {

        #region Fields
        private string _newContactName;
        private string _newContactDescription;
        private int _newContactReputation;
        private string _newContactImgName;

        private ICommand _newContactCancel;
        private ICommand _newContactDone;
        private ICommand _newContactAddImage;
        #endregion

        #region Constructors
        public NewContactViewModel()
        {
            this.NewContactImgName = ContactModel.GetDefaultImgName();
        }
        #endregion

        #region Public Properties/Commands
        public string NewContactName { get { return _newContactName; } set { _newContactName = value; OnPropertyChanged("NewContactName"); OnPropertyChanged("DoneButtonIsEnabled"); } }
        public string NewContactDescription { get { return _newContactDescription; } set { _newContactDescription = value; OnPropertyChanged("NewContactDescription"); OnPropertyChanged("DoneButtonIsEnabled"); } }
        public int NewContactReputation { get { return _newContactReputation; } set { _newContactReputation = value; OnPropertyChanged("NewContactReputation");  } }
        public string NewContactImgName { get { return _newContactImgName; } set { _newContactImgName = value; OnPropertyChanged("NewContactImgName");  } }

        public bool DoneButtonIsEnabled
        {
            get
            {
                if (String.IsNullOrEmpty(NewContactDescription) || String.IsNullOrEmpty(NewContactName))
                    return false;
                else
                    return true;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (_newContactCancel == null)
                {
                    _newContactCancel = new RelayCommand(
                        param => Cancel()
                    );
                }
                return _newContactCancel;
            }
        }

        public ICommand DoneCommand
        {
            get
            {
                if (_newContactDone == null)
                {
                    _newContactDone = new RelayCommand(
                        param => Done()
                    );
                }
                return _newContactDone;
            }
        }

        public ICommand AddImageCommand
        {
            get
            {
                if (_newContactAddImage == null)
                {
                    _newContactAddImage = new RelayCommand(
                        param => AddImage()
                    );
                }
                return _newContactAddImage;
            }
        }
        #endregion

        #region Methods
        private void Cancel()
        {
            // Close dialog host
            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Done()
        {
            ContactModel newContact = new ContactModel(
                name: this.NewContactName,
                description: this.NewContactDescription,
                reputation: this.NewContactReputation,
                imgName: this.NewContactImgName
                );

            // Add new contact to character's contact list
            this.UserCharacter.CharacterContacts.Add(newContact);

            // Close dialog host
            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void AddImage()
        {
            this.NewContactImgName = DataHandler.UploadImageAndGetPath();
        }

        #endregion

    }
}
