using Engine.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Engine.ViewModels
{
    public class QuickMenuViewModel : BaseViewModel, INotifyPropertyChanged
    {

        #region Fields
        private ICommand _saveCharacterAs;
        private ICommand _saveCharacter;
        private ICommand _loadCharacter;
        private ICommand _exitProgram;
        private ICommand _addQuest;
        private ICommand _fullHistory;
        private ICommand _questLog;

        private Window _mainWindow;



        #endregion

        #region Constructors
        public QuickMenuViewModel()
        {
            _mainWindow = Application.Current.MainWindow;
        }
        #endregion

        #region Public Properties/Commands
        public ICommand QuestLogCommand
        {
            get
            {
                if (_questLog == null)
                {
                    _questLog = new RelayCommand(
                        param => QuestLog()
                    );
                }
                return _questLog;
            }
        }

        public ICommand FullHistoryCommand
        {
            get
            {
                if (_fullHistory == null)
                {
                    _fullHistory = new RelayCommand(
                        param => FullHistory()
                    );
                }
                return _fullHistory;
            }
        }

        public ICommand AddQuestCommand
        {
            get
            {
                if (_addQuest == null)
                {
                    _addQuest = new RelayCommand(
                        param => AddQuest()
                    );
                }
                return _addQuest;
            }
        }

        public ICommand SaveCharacterCommand
        {
            get
            {
                if (_saveCharacter == null)
                {
                    _saveCharacter = new RelayCommand(
                        param => SaveCharacter(param)
                    );
                }
                return _saveCharacter;
            }
        }

        public ICommand SaveCharacterAsCommand
        {
            get
            {
                if (_saveCharacterAs == null)
                {
                    _saveCharacterAs = new RelayCommand(
                        param => SaveCharacterAs()
                    );
                }
                return _saveCharacterAs;
            }
        }

        public ICommand LoadCharacterCommand
        {
            get
            {
                if (_loadCharacter == null)
                {
                    _loadCharacter = new RelayCommand(
                        param => LoadCharacter()
                    );
                }
                return _loadCharacter;
            }
        }

        public ICommand ExitProgramCommand
        {
            get
            {
                if (_exitProgram == null)
                {
                    _exitProgram = new RelayCommand(
                        param => ExitProgram()
                    );
                }
                return _exitProgram;
            }
        }
        #endregion

        #region Methods
        private void SaveCharacterAs()
        {
            // Create OpenFileDialog 
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
            {
                // Set filter for file extension and default file extension 
                DefaultExt = ".xml",
                Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*"

            };

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected/new file name and write file to it
            if (result == true)
            {
                string filename = dlg.FileName;
                DataHandler.SaveToXml(this.UserCharacter, filename);
                AppSettings.UpdateSaveLocation(filename);
            }
            CloseMenu();

        }

        private void SaveCharacter(object sender)
        {
            if (AppSettings.SaveLocation == null)
            {
                SaveCharacterAs();
            }
            else
            {
                DataHandler.SaveToXml(this.UserCharacter, AppSettings.SaveLocation);
            }
            CloseMenu();

        }

        private void LoadCharacter()
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
                AppSettings.UpdateSaveLocation(filename);
                this.UserCharacter = loadedChar;

                SendToLoadScreen();
                CloseMenu();
            }

        }

        private void CloseMenu()
        {
            MaterialDesignThemes.Wpf.DrawerHost.CloseDrawerCommand.Execute(null, null);
        }

        private void SendToLoadScreen()
        {
            AppSettings.LoadDestination = "Dashboard";
            AppSettings.LoadDuration = 1;
            NavigateTo("Load");
        }

        private void FullHistory()
        {
            NavigateTo("Full History");
        }

        private void QuestLog()
        {
            NavigateTo("Quest Log");
        }

        private void AddQuest()
        {
            NavigateTo("New Quest");
        }

        private void ExitProgram()
        {
            Application.Current.Shutdown();
        }
        #endregion

    }
}
