using CharSheet.classes.data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace CharSheet.classes
{
    [DataContract]
    [KnownType(typeof(Character))]
    public class Character : INotifyPropertyChanged
    {
        public DataHandler dataHandler = new DataHandler();

        [DataMember]
        public Dictionary<int, int> _attributeValue; // (Attribute ID: Value)
        [DataMember]
        public Dictionary<int, int> _skillValue; // (Skill ID: Value)
        [DataMember]
        public List<HistoryEntry> _eventHistory;
        [DataMember]
        private List<Milestone> _milestones;
        [DataMember]
        private List<Quest> _quests = new List<Quest> { };
        [DataMember]
        private List<Contact> _characterContacts = new List<Contact> { };

        [DataMember]
        private String _name;
        [DataMember]
        private int _currentXP;

        public Dictionary<int, int> AttributeValue
        {
            get { return _attributeValue; }
            set
            {
                _attributeValue = value;
                OnPropertyChanged(() => AttributeValue);
            }
        }

        public Dictionary<int, int> SkillValue
        {
            get { return _skillValue; }
            set
            {
                _skillValue = value;
                OnPropertyChanged(() => SkillValue);
            }
        }

        public List<HistoryEntry> EventHistory
        {
            get { return _eventHistory; }
            set
            {
                _eventHistory = value;
                OnPropertyChanged(() => EventHistory);
            }
        }

        public List<Milestone> Milestones
        {
            get { return _milestones; }
            set
            {
                _milestones = value;
                OnPropertyChanged(() => Milestones);
            }
        }

        public String Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(() => Name);
            }
        }

        public int CurrentXP
        {
            get { return _currentXP; }
            set
            {
                _currentXP = value;
                OnPropertyChanged(() => CurrentXP);
            }
        }

        public List<Quest> Quests
        {
            get { return _quests; }
            set
            {
                _quests = value;
                OnPropertyChanged(() => Quests);
            }
        }

        public List<Contact> CharacterContacts
        {
            get { return _characterContacts; }
            set
            {
                _characterContacts = value;
                OnPropertyChanged(() => CharacterContacts);
            }
        }

        //Create New Character
        public Character()
        {
            this.Name = "Generic Bob";
            this.AttributeValue = dataHandler.GenerateNewAttributeValue();
            this.SkillValue = dataHandler.GenerateNewSkillValue();
            this.EventHistory = dataHandler.GenerateNewEventHistory();
            this.Milestones = dataHandler.GenerateNewMilestones();
            this.CurrentXP = 460;
        }

        public void Add(HistoryEntry entry)
        {
            EventHistory.Add(entry);
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        // Just so we can call it via lambda which is nicer
        public void OnPropertyChanged<T>(Expression<Func<T>> propertyNameExpression)
        {
            OnPropertyChanged(((MemberExpression)propertyNameExpression.Body).Member.Name);
        }

    }
}
