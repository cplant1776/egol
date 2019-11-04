using Engine.Models;
using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Engine.ViewModels
{
    public class LevelUpViewModel : BaseViewModel, IPageViewModel, INotifyPropertyChanged
    {

        #region Fields
        private ObservableCollection<StatRow> _attributeRows = new ObservableCollection<StatRow> { };
        private ObservableCollection<StatRow> _skillRows = new ObservableCollection<StatRow> { };
        private int _numOfLevels;
        private int _attributePointsToDistribute;
        private int _skillPointsToDistribute;

        private ICommand _plusAttribute;
        private ICommand _minusAttribute;
        private ICommand _plusSkill;
        private ICommand _minusSkill;
        private ICommand _doneCommand;
        #endregion

        #region Constructors
        public LevelUpViewModel()
        {
            GenerateStatRows();
            this.NumOfLevels = AppSettings.NumOfLevelsOnLevelUp;
            this.AttributePointsToDistribute = NumOfLevels * AppSettings.AttributePointsPerLevel;
            this.SkillPointsToDistribute = NumOfLevels * AppSettings.SkillPointsPerLevel;
        }
        #endregion

        #region Public Properties/Commands
        public string Name{ get { return "CharacterCreation"; } }

        public ObservableCollection<StatRow> AttributeRows { get { return _attributeRows; } set { _attributeRows = value; } }

        public ObservableCollection<StatRow> SkillRows { get { return _skillRows; } set { _skillRows = value; } }

        public int AttributePointsToDistribute { get { return _attributePointsToDistribute; } set { _attributePointsToDistribute = value; OnPropertyChanged("HelpText"); } }
        public int SkillPointsToDistribute { get { return _skillPointsToDistribute; } set { _skillPointsToDistribute = value; OnPropertyChanged("HelpText"); } }

        public int NumOfLevels {
            get
            {
                return _numOfLevels;
            }
            set
            {
                _numOfLevels = value;
                OnPropertyChanged("NumOfLevels");
                OnPropertyChanged("HelpText");
            }
        }

        public string HelpText {
            get
            {
                return "Distirbute " + AttributePointsToDistribute.ToString()
                    + " attribute points and " + SkillPointsToDistribute.ToString()
                    + " skill points."; 
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
        private void GenerateStatRows()
        {
            // Generate attribute rows
            foreach (KeyValuePair<int, int> entry in this.UserCharacter.AttributeValue)
            {
                this._attributeRows.Add(new StatRow(name: DataHandler.getAttributeDesc(entry.Key), value: entry.Value, isSkill: false));
            }

            // Generate skill rows
            foreach (KeyValuePair<int, int> entry in this.UserCharacter.SkillValue)
            {
                this.SkillRows.Add(new StatRow(name: DataHandler.getSkillDesc(entry.Key), value: entry.Value, isSkill: true));
            }
        }

        public void PlusAttribute(object sender)
        {
            if (this.AttributePointsToDistribute > 0)
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
                                this.AttributePointsToDistribute--;
                                break;
                            }
                        }
                        break;
                    }
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
                    // Value cant go below zero or starting value
                    if (tar.StatValue > 0 && tar.StatValue > tar.StartingValue)
                    {
                        //Find target attribute in current character
                        foreach (StatRow r in this.AttributeRows)
                        {
                            if (r.StatName == tar.StatName)
                            {
                                r.StatValue--; //Update attribute value
                                this.AttributePointsToDistribute++;
                                break;
                            }
                        }
                    }
                    break;
                }
        }

        public void PlusSkill(object sender)
        {
            if (SkillPointsToDistribute > 0)
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
                                this.SkillPointsToDistribute--;
                                break;
                            }
                        }
                        break;
                    }
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
                    // Value cant go below zero or starting value
                    if (tar.StatValue > 0 && tar.StatValue > tar.StartingValue)
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

        public void Done()
        {
            // Update character stats
            foreach(StatRow r in this.SkillRows)
            {
                this.UserCharacter.SkillValue[DataHandler.getSkillId(r.StatName)] = r.StatValue;
            }
            foreach (StatRow r in this.AttributeRows)
            {
                this.UserCharacter.AttributeValue[DataHandler.getAttributeId(r.StatName)] = r.StatValue;
            }
            //Close dialog
            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
        }
        #endregion

    }
}
