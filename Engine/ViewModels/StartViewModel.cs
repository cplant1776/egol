using Engine.Utils;
using Engine.Utils.test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Engine.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        #region Fields

        private ICommand _loadCharacterCommand;
        private ICommand _newCharacterCommand;
        private ICommand _generateCharacterCommand; 

        #endregion

        #region Constructors
        public StartViewModel()
        {

        }
        #endregion

        #region Public Properties/Commands
        public ICommand LoadCharacterCommand
        {
            get
            {
                if (_loadCharacterCommand == null)
                {
                    _loadCharacterCommand = new RelayCommand(
                        param => LoadCharacter()
                    );
                }
                return _loadCharacterCommand;
            }
        }

        public ICommand NewCharacterCommand
        {
            get
            {
                if (_newCharacterCommand == null)
                {
                    _newCharacterCommand = new RelayCommand(
                        param => NewCharacter()
                    );
                }
                return _newCharacterCommand;
            }
        }

        public ICommand GenerateCharacterCommand
        {
            get
            {
                if (_generateCharacterCommand == null)
                {
                    _generateCharacterCommand = new RelayCommand(
                        param => GenerateCharacter()
                    );
                }
                return _generateCharacterCommand;
            }
        } 
        #endregion

        public void NewCharacter()
        {
            // NAVIGATE TO CHARACTER CREATION
        }

        public void LoadCharacter()
        {
            CharacterModel loadedChar = new CharacterModel();
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".xml",
                Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*"
            };

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and load a character with it
            if (result == true)
            {
                string filename = dlg.FileName;
                loadedChar = DataHandler.LoadCharacterFromXml(filename);
            }

            this.UserCharacter = loadedChar;
        }

        public void GenerateCharacter()
        {
            this.UserCharacter = new DummyCharacter();
        }
    }
}
