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
        private ContactModel _newContact;

        private ICommand _newContactCancel;
        private ICommand _newContactDone;
        private ICommand _newContactAddImage;
        #endregion

        #region Constructors
        public NewContactViewModel()
        {
            _newContact = new ContactModel();
        }
        #endregion

        #region Public Commands
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

        #region Public Properties
        public ContactModel NewContact
        {
            get
            {
                return _newContact;
            }
            set
            {
                _newContact = value;
                OnPropertyChanged("NewContact");
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
                name: this.NewContact.Name,
                description: this.NewContact.Description,
                reputation: this.NewContact.Reputation,
                imgName: this.NewContact.ImgName
                );

            // Add new contact to character's contact list
            this.UserCharacter.CharacterContacts.Add(newContact);

            // Close dialog host
            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void AddImage()
        {
            this.NewContact.ImgName = this.NewContact.SetImage();
        }

        #endregion

    }
}
