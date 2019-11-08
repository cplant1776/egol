using Engine.Models;
using Engine.Utils;
using Engine.Utils.test;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Engine.ViewModels
{
    public class CharacterCreationViewModel : BaseViewModel, IPageViewModel
    {

        #region Fields
        private List<StatRow> _attributeRows = new List<StatRow> { };
        private List<StatRow> _skillRows = new List<StatRow> { };
        private string _imgName;
        private string _characterName;
        private string _characterDescription;
        private ICommand _doneCommand;
        private ICommand _cancelCommand;
        private ICommand _plusAttribute;
        private ICommand _minusAttribute;
        private ICommand _plusSkill;
        private ICommand _minusSkill;
        private ICommand _addImage;
        #endregion

        #region Constructors
        public CharacterCreationViewModel()
        {
            //this.UserCharacter = new DummyCharacter();
            GenerateStatRows();
            this.ImgName = this.UserCharacter.ImgName;
        }
        #endregion

        #region Public Properties/Commands
        public string ImgName { get { return _imgName; } set { _imgName = value; OnPropertyChanged("ImgName"); } }

        public List<StatRow> AttributeRows { get { return _attributeRows; } set { _attributeRows = value; } }

        public List<StatRow> SkillRows { get { return _skillRows; } set { _skillRows = value; } }

        public string CharacterName { get { return _characterName; } set { _characterName = value; } }
        public string CharacterDescription { get { return _characterDescription; } set { _characterDescription = value; } }

        public string Name
        {
            get { return "CharacterCreation"; }
        }

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
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(
                        param => Cancel()
                    );
                }
                return _cancelCommand;
            }
        }
        public ICommand PlusAttributeCommand
        {
            get
            {
                if (_plusAttribute == null)
                {
                    _plusAttribute = new RelayCommand(
                        param => PlusAttribute(param)
                    );
                }
                return _plusAttribute;
            }
        }
        public ICommand MinusAttributeCommand
        {
            get
            {
                if (_minusAttribute == null)
                {
                    _minusAttribute = new RelayCommand(
                        param => MinusAttribute(param)
                    );
                }
                return _minusAttribute;
            }
        }
        public ICommand PlusSkillCommand
        {
            get
            {
                if (_plusSkill == null)
                {
                    _plusSkill = new RelayCommand(
                        param => PlusSkill(param)
                    );
                }
                return _plusSkill;
            }
        }
        public ICommand MinusSkillCommand
        {
            get
            {
                if (_minusSkill == null)
                {
                    _minusSkill = new RelayCommand(
                        param => MinusSkill(param)
                    );
                }
                return _minusSkill;
            }
        }
        public ICommand AddImageCommand
        {
            get
            {
                if (_addImage == null)
                {
                    _addImage = new RelayCommand(
                        param => AddImage()
                    );
                }
                return _addImage;
            }
        }
        #endregion



        private void GenerateStatRows()
        {
            // Generate attribute rows
            foreach (KeyValuePair<int, int> entry in this.UserCharacter.AttributeValue)
            {
                this.AttributeRows.Add(new StatRow(name: DataHandler.getAttributeDesc(entry.Key), value: entry.Value, isSkill: false));
            }

            // Generate skill rows
            foreach (KeyValuePair<int, int> entry in this.UserCharacter.SkillValue)
            {
                this.SkillRows.Add(new StatRow(name: DataHandler.getSkillDesc(entry.Key), value: entry.Value, isSkill: true));
            }
        }

        public void Done()
        {
            bool isMissingFields = CheckForMissingFields();
            if (isMissingFields)
            {

            }
            else
            {
                UserCharacter = new CharacterModel(
                    name: CharacterName,
                    description: CharacterDescription,
                    attributes: AttributeRows,
                    skills: SkillRows,
                    imgName: ImgName
                    );
                NavigateTo("Dashboard");
            }

        }

        private bool CheckForMissingFields()
        {
            string missingFields = "";
            string errorTitle = "Missing Fields";

            if (String.IsNullOrEmpty(CharacterName))
                missingFields += "* Character Name\n";
            if (String.IsNullOrEmpty(CharacterDescription))
                missingFields += "* Character Description\n";

            if (String.IsNullOrEmpty(missingFields))
            {
                return false;
            }
            else
            {
                DialogHost.Show(new ErrorDialogViewModel(msg: "Please make sure the following fields are filled: \n\n" + missingFields, title: errorTitle));
                return true;
            }
        }

        public void Cancel()
        {
            //NavigateToStart();
        }

        public void PlusAttribute(object sender)
        {
            // Find Sending Row
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow row)
                {
                    // Get sent attribute
                    StatRow tar = (StatRow)row.Item;
                    //Find target attribute in current character
                    foreach (StatRow r in this.AttributeRows)
                    {
                        if (r.StatName == tar.StatName)
                        {
                            r.StatValue++; //Update attribute value
                            break;
                        }
                    }
                    break;
                }
        }

        public void MinusAttribute(object sender)
        {
            // Find Sending Row
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow row)
                {
                    // Get sent attribute
                    StatRow tar = (StatRow)row.Item;
                    // Value cant go below zero
                    if (tar.StatValue > 0)
                    {
                        //Find target attribute in current character
                        foreach (StatRow r in this.AttributeRows)
                        {
                            if (r.StatName == tar.StatName)
                            {
                                r.StatValue--; //Update attribute value
                                break;
                            }
                        }
                    }
                    break;
                }
        }

        public void PlusSkill(object sender)
        {
            // Find Sending Row
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow row)
                {
                    // Get sent attribute
                    StatRow tar = (StatRow)row.Item;
                    //Find target attribute in current character
                    foreach (StatRow r in this.SkillRows)
                    {
                        if (r.StatName == tar.StatName)
                        {
                            r.StatValue++; //Update attribute value
                            break;
                        }
                    }
                    break;
                }
        }

        public void MinusSkill(object sender)
        {
            // Find Sending Row
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow row)
                {
                    // Get sent attribute
                    StatRow tar = (StatRow)row.Item;
                    // Value cant go below zero
                    if (tar.StatValue > 0)
                    {
                        //Find target attribute in current character
                        foreach (StatRow r in this.SkillRows)
                        {
                            if (r.StatName == tar.StatName)
                            {
                                r.StatValue--; //Update attribute value
                                break;
                            }
                        }
                    }
                    break;
                }
        }

        public void AddImage()
        {
            ImgName = this.UserCharacter.SetImage();
        }

    }
}
