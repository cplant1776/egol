using CharSheet.classes.data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace CharSheet.classes.data
{
    [DataContract]
    [KnownType(typeof(Character))]
    public partial class Character : INotifyPropertyChanged
    {
        [DataMember]
        private Dictionary<int, int> _attributeValue = new Dictionary<int, int> { }; // (Attribute ID: Value)
        [DataMember]
        private Dictionary<int, int> _skillValue = new Dictionary<int, int> { }; // (Skill ID: Value)
        [DataMember]
        private List<EventRecord> _eventHistory = new List<EventRecord> { };
        [DataMember]
        private List<Quest> _quests = new List<Quest> { };
        [DataMember]
        private List<Contact> _characterContacts = new List<Contact> { };

        [DataMember]
        private String _name;
        [DataMember]
        private String _description;
        [DataMember]
        private int _currentXP;
        [DataMember]
        private string _imgName;

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

        public List<EventRecord> EventHistory
        {
            get { return _eventHistory; }
            set
            {
                _eventHistory = value;
                OnPropertyChanged(() => EventHistory);
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

        public String Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(() => Description);
            }
        }

        public String ImgName
        {
            get { return _imgName; }
            set
            {
                _imgName = value;
                OnPropertyChanged(() => ImgName);
            }
        }

        //Create New Character With Default Stats
        public Character()
        {
            this.Name = "Generic Bob";
            SetDefaultAttributeValues();
            SetDefaultSkillValues();
            this.CurrentXP = 0;
            this.ImgName = AppSettings.ContactImageFullPath + "default.png";
        }

        private void SetDefaultAttributeValues()
        {
            // Set all attributes to 0 (default)
            foreach(int key in AppSettings.Attributes.Keys)
            {
                this.AttributeValue.Add(key, 0);
            }

            //debugging
            this.AttributeValue[2] = 1;
        }

        private void SetDefaultSkillValues()
        {
            // Set all skills to 0 (default)
            foreach (int key in AppSettings.Skills.Keys)
            {
                this.SkillValue.Add(key, 0);
            }
        }

        public Contact GetCharacterContact(int id)
        {
            foreach(Contact c in this.CharacterContacts)
            {
                if (c.Id == id)
                    return c;
            }
            // Contact not found with that id
            return new Contact();
        }

        public Quest GetQuest(string targetTitle)
        {
            foreach (Quest q in this.Quests)
                if (q.Title == targetTitle)
                    return q;
            return new Quest();
        }

        public Quest GetQuest(int  targetId)
        {
            foreach (Quest q in this.Quests)
                if (q.Id == targetId) 
                    return q;
            return new Quest();
        }

        public Contact GetContact(int targetId)
        {
            foreach (Contact c in this.CharacterContacts)
            {
                if (c.Id == targetId)
                {
                    return c;
                }
            }
            return new Contact();
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
