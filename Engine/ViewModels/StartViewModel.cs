using Engine.Utils;
using Engine.Utils.test;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Engine.ViewModels
{
    public class StartViewModel : BaseViewModel, IPageViewModel
    {
        #region Fields
        private ICommand _loadCharacterCommand;
        private ICommand _generateCharacterCommand; 

        #endregion

        #region Constructors
        public StartViewModel()
        {

        }
        #endregion

        #region Public Properties/Commands
        public string Name
        {
            get { return "Start"; }
        }

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
            NavigateTo("Character Creation");
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
                try
                {
                    loadedChar = DataHandler.LoadCharacterFromXml(filename);
                    this.UserCharacter = loadedChar;
                    NavigateTo("Dashboard");
                }
                catch(Exception e)  //Error reading file
                {
                    Console.WriteLine("ERROR LOADING FILE: " + e);
                    string errorMessage = "There was a problem loading your file. Please make sure it is properly formatted.";
                    string errorTitle = "Cannot Load File";
                    DialogHost.Show(new ErrorDialogViewModel(msg: errorMessage, title: errorTitle));
                }
            }

            
        }

        public void GenerateCharacter()
        {
            this.UserCharacter = new DummyCharacter();
            NavigateTo("Dashboard");
        }
    }
}
