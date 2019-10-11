using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CharSheet.classes
{
    [DataContract]
    [KnownType(typeof(Character))]
    public class Character
    {
        public DataHandler dataHandler = new DataHandler();
        [DataMember]
        public Dictionary<int, int> attributeValue = new Dictionary<int, int> { }; // (Attribute ID, Value)
        [DataMember]
        public Dictionary<int, int> skillValue = new Dictionary<int, int>{ }; // (Skill ID, Value)
        [DataMember]
        public List<HistoryEntry> eventHistory = new List<HistoryEntry> { };
        [DataMember]
        public List<Milestone> milestones = new List<Milestone> { };

        [DataMember]
        public String name;
        [DataMember]
        public int currentXP;

        //Create New Character
        public Character()
        {
            this.attributeValue = dataHandler.GenerateNewAttributeValue();
            this.skillValue = dataHandler.GenerateNewSkillValue();
            this.eventHistory = dataHandler.GenerateNewEventHistory();
            this.milestones = dataHandler.GenerateNewMilestones();
            this.currentXP = 160;
        }

        public void Add(HistoryEntry entry)
        {
            eventHistory.Add(entry);
        }

    }
}
