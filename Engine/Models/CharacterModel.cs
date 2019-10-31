using Engine.Models;
using Engine.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Engine.ViewModels
{
    [DataContract]
    public partial class CharacterModel : MyObservableObject
    {
        #region Fields

        [DataMember]
        private Dictionary<int, int> _attributeValue = new Dictionary<int, int> { }; // (Attribute ID: Value)
        [DataMember]
        private Dictionary<int, int> _skillValue = new Dictionary<int, int> { }; // (Skill ID: Value)
        [DataMember]
        private List<EventRecordModel> _eventHistory = new List<EventRecordModel> { };
        [DataMember]
        private List<QuestModel> _quests = new List<QuestModel> { };
        [DataMember]
        private List<ContactModel> _characterContacts = new List<ContactModel> { };

        [DataMember]
        private String _name;
        [DataMember]
        private String _description;
        [DataMember]
        private int _currentXP;
        [DataMember]
        private string _imgName;

        #endregion

        #region Constructors
        public CharacterModel()
        {
            SetDefaultAttributeValues();
            SetDefaultSkillValues();
            SetDefaultImage();
        }

        public CharacterModel(string name, List<StatRow> attributes, List<StatRow> skills, string description)
        {
            // Set Attributes/Skills
            foreach (StatRow r in attributes)
                this.AttributeValue[DataHandler.getAttributeId(r.StatName)] = r.StatValue;
            foreach (StatRow r in skills)
                this.SkillValue[DataHandler.getSkillId(r.StatName)] = r.StatValue;

            // Set name
            this.Name = name;
            // Description
            this.Description = description;
        }
        #endregion

        #region Properties

        public Dictionary<int, int> AttributeValue
        {
            get { return _attributeValue; }
            set
            {
                if(value != _attributeValue)
                {
                    _attributeValue = value;
                    OnPropertyChanged("AttributeValue");
                }
            }
        }

        public Dictionary<int, int> SkillValue
        {
            get { return _skillValue; }
            set
            {
                if (value != _skillValue)
                {
                    _skillValue = value;
                    OnPropertyChanged("SkillValue");
                }
            }
        }

        public List<EventRecordModel> EventHistory
        {
            get { return _eventHistory; }
            set
            {
                if (value != _eventHistory)
                {
                    _eventHistory = value;
                    OnPropertyChanged("EventHistory");
                }
            }
        }

        public String Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public int CurrentXP
        {
            get { return _currentXP; }
            set
            {
                if (value != _currentXP)
                {
                    _currentXP = value;
                    OnPropertyChanged("CurrentXP");
                }
            }
        }

        public List<QuestModel> Quests
        {
            get { return _quests; }
            set
            {
                if (value != _quests)
                {
                    _quests = value;
                    OnPropertyChanged("Quests");
                }
            }
        }

        public List<ContactModel> CharacterContacts
        {
            get { return _characterContacts; }
            set
            {
                if (value != _characterContacts)
                {
                    _characterContacts = value;
                    OnPropertyChanged("CharacterContacts");
                }
            }
        }

        public String Description
        {
            get { return _description; }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public String ImgName
        {
            get { return _imgName; }
            set
            {
                if (value != _imgName)
                {
                    _imgName = value;
                    OnPropertyChanged("ImgName");
                }
            }
        }

        #endregion


        #region Initial attribute/skills
        private void SetDefaultAttributeValues()
        {
            // Set all attributes to 0 (default)
            foreach (int key in AppSettings.Attributes.Keys)
            {
                this.AttributeValue.Add(key, 0);
            }
        }

        private void SetDefaultSkillValues()
        {
            // Set all skills to 0 (default)
            foreach (int key in AppSettings.Skills.Keys)
            {
                this.SkillValue.Add(key, 0);
            }
        }

        private void SetDefaultImage()
        {
            this.ImgName = AppSettings.ContactImageFullPath + "default.png";
        }
        #endregion

        #region Getters (contacts, quests, etc)
        public ContactModel GetCharacterContact(int id)
        {
            foreach (ContactModel c in this.CharacterContacts)
            {
                if (c.Id == id)
                    return c;
            }
            // Contact not found with that id
            return new ContactModel();
        }

        public QuestModel GetQuest(string targetTitle)
        {
            foreach (QuestModel q in this.Quests)
                if (q.Title == targetTitle)
                    return q;
            return new QuestModel();
        }

        public QuestModel GetQuest(int targetId)
        {
            foreach (QuestModel q in this.Quests)
                if (q.Id == targetId)
                    return q;
            return new QuestModel();
        }

        public ContactModel GetContact(int targetId)
        {
            foreach (ContactModel c in this.CharacterContacts)
            {
                if (c.Id == targetId)
                {
                    return c;
                }
            }
            return new ContactModel();
        } 
        #endregion

        public string SetImage()
        {
            this.ImgName = DataHandler.UploadImageAndGetPath();
            return this.ImgName;
        }
    }
}
