using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharSheet.classes.data
{
    class DummyCharacter : Character
    {
        private int NUM_OF_CONTACTS = 10;
        private int NUM_OF_QUESTS = 30;
        private int NUM_OF_EVENTS = 15;
        private int MAX_MILESTONE_VALUE = 5;
        private int MAX_REP_GAIN = 100;
        private int MAX_NUM_LOREM_PARAGRAPHS = 6;
        private DateTime START_DATE = new DateTime(2019, 1, 1);
        private int DATE_RANGE;
        private int NUM_OF_SKILLS = 12;
        private int NUM_OF_ATTRIBUTES = 8;
        private static Random rnd = new Random();

        public Dictionary<int, string> AttributeStringDict = new Dictionary<int, string> { };
        public Dictionary<string, int> AttributeIdDict = new Dictionary<string, int> { };
        public Dictionary<int, string> SkillsStringDict = new Dictionary<int, string> { };
        public Dictionary<string, int> SkillsIdDict = new Dictionary<string, int> { };


        public DummyCharacter()
        {
            DummyResources.InitializeSettings();
            this.Name = "Average Joe";
            this.CurrentXP = 0;
            this.ImgName = "../media/dummy/AverageJoe.png";

            this.CharacterContacts = GenerateContacts();
            this.Quests = GenerateQuests();
            this.EventHistory = GenerateEventHistory();
            this.EventHistory.Sort((x, y) => x.Timestamp.CompareTo(y.Timestamp)); // Sort history by date
            this.AttributeValue = GenerateAttributeValues();
            this.SkillValue = GenerateSkillValues();

            GenerateAttributeAndSkillDicts();
            this.CurrentXP = GetXPFromHistory();
        }

        public List<Contact> GenerateContacts()
        {
            DATE_RANGE = (DateTime.Today - START_DATE).Days;

            List<Contact> contactList = new List<Contact> { };
            for (int i = 0; i < NUM_OF_CONTACTS; i++)
            {
                contactList.Add(new Contact(
                    name: GetRandomName(),
                    description: GetRandomDescription(),
                    imgName: GetRandomImage(),
                    id: GetRandomId()
                    )
                );
            }
            return contactList;
        }

        public List<Quest> GenerateQuests()
        {
            List<Quest> questList = new List<Quest> { };

            for (int i = 0; i < NUM_OF_QUESTS; i++)
            {
                questList.Add(new Quest(
                    title: GetRandomQuestTitle(),
                    description: GetRandomDescription(),
                    xpValue: GetRandomXPValue(),
                    contactId: GetRandomContactId(),
                    reputationValue: RandomIntUnder(MAX_REP_GAIN),
                    deadline: GetRandomDateTime(),
                    status: GetRandomStatus(),
                    id: GetRandomId()
                    )
                );
            }

            return questList;
        }

        public List<EventRecord> GenerateEventHistory()
        {
            List<EventRecord> eventRecords = new List<EventRecord> { };
            for(int i=0; i < NUM_OF_EVENTS; i++)
            {
                eventRecords.Add(GenerateRandomEvent());
            }
            return eventRecords;
        }

        public Dictionary<int, int> GenerateAttributeValues()
        {
            Dictionary<int, int> AttributeValues = new Dictionary<int, int> { };
            for(int i=1; i < NUM_OF_ATTRIBUTES+1; i++)
            {
                AttributeValues[i] = RandomIntUnder(20);
            }
            return AttributeValues;
        }

        public Dictionary<int, int> GenerateSkillValues()
        {
            Dictionary<int, int> SkillValues = new Dictionary<int, int> { };
            for (int i = 1; i < NUM_OF_SKILLS+1; i++)
            {
                SkillValues[i] = RandomIntUnder(20);
            }
            return SkillValues;
        }

        public void GenerateAttributeAndSkillDicts()
        {
            string k;
            for(int i=1; i < NUM_OF_ATTRIBUTES+1; i++)
            {
                k = "Attribute " + i.ToString();
                this.AttributeStringDict[i] = k;
                this.AttributeIdDict[k] = i;
            }
            for (int i = 1; i < NUM_OF_SKILLS+1; i++)
            {
                k = "Skill " + i.ToString();
                this.SkillsStringDict[i] = k;
                this.SkillsIdDict[k] = i;
            }
        }

        public int GetXPFromHistory()
        {
            int totalXP = 0;
            foreach(Quest q in this.Quests)
            {
                if (q.Status == 2) //If quest is completed
                    totalXP += q.XPValue;
            }
            return totalXP;
        }

        public EventRecord GenerateRandomEvent()
        {
            int i = rnd.Next(101);
            if (i < 10) //Small chance of generating a milestone
                return GetRandomMilestone();
            else
                return GetRandomXPEvent();
        }

        public XPEvent GetRandomXPEvent()
        {
            int eventId = GetRandomQuestId();

            return new XPEvent(
                    description: GetRandomXPEventDescription(),
                    eventId: eventId,
                    value: GetEventValue(eventId),
                    primarySkill: RandomIntUnder(NUM_OF_SKILLS),
                    timestamp: GetRandomDateTime()
                );
        }

        public Milestone GetRandomMilestone()
        {
            return new Milestone(
                description: "Milestone description",
                eventId: GetRandomId(),
                value: RandomIntUnder(MAX_MILESTONE_VALUE),
                attributeId: RandomIntUnder(NUM_OF_ATTRIBUTES),
                timestamp: GetRandomDateTime()
                );
        }

        public int RandomIntUnder(int max)
        {
            return rnd.Next(0, max);
        }

        public int GetRandomQuestId()
        {
            int i = rnd.Next(this.Quests.Count);
            return this.Quests[i].Id;
        }

        public string GetRandomImage()
        {
            int i = RandomIntUnder(100);
            if (i < 25) // Chance to not have a portrait
                return "";
            else
            {
                int j = RandomIntUnder(DummyResources.ContactImages.Count);
                return DummyResources.ContactImages[j];
            }
        }

        public int GetEventValue(int targetId)
        {
            foreach (Quest q in this.Quests)
            {
                if (q.Id == targetId)
                    return q.XPValue;
            }
            return -1;
        }

        public string GetRandomXPEventDescription()
        {
            return "Completed " + GetRandomQuestTitle();
        }

        public string GetRandomName()
        {
            int i = rnd.Next(DummyResources.ContactNames.Count);
            return DummyResources.ContactNames[i];
        }

        public string GetRandomDescription()
        {
            string description = "";
            for(int i=0; i < rnd.Next(1, MAX_NUM_LOREM_PARAGRAPHS); i++)
            {
                description += Faker.Lorem.Paragraph();
                description += "\n\n";
            }
            return description;
        }

        public int GetRandomId()
        {
            return rnd.Next(1, 60000);
        }

        public int GetRandomContactId()
        {
            int i = rnd.Next(this.CharacterContacts.Count);
            return this.CharacterContacts[i].Id;
        }

        public string GetRandomQuestTitle()
        {
            int i = rnd.Next(DummyResources.QuestTitles.Count);
            return DummyResources.QuestTitles[i];
        }

        public int GetRandomXPValue()
        {
            int i = rnd.Next(DummyResources.XPSelectableValues.Count);
            return DummyResources.XPSelectableValues[i];
        }

        public DateTime GetRandomDateTime()
        {
            // Returns random date between 01/01/2019 and today
            return START_DATE.AddDays(rnd.Next(DATE_RANGE));
        }

        public int GetRandomStatus()
        {
            // 0 - Accepted
            // 1 - Active
            // 2 - Completed
            return rnd.Next(0, 3);
        }

        public static class DummyResources
        {
            public static string json;
            public static List<string> ContactNames;
            public static List<string> ContactImages;
            public static List<string> QuestTitles;
            public static List<string> DailiesTitles;
            public static List<int> XPSelectableValues;
            public static void InitializeSettings()
            {
                json = File.ReadAllText("classes/data/DummyDataResources.json");
                var jObject = JObject.Parse(json);

                // Contact Names
                var jToken = jObject.GetValue("SampleContactNames");
                ContactNames = (List<String>)jToken.ToObject(typeof(List<String>));

                // Contact images
                jToken = jObject.GetValue("SampleContactImages");
                ContactImages = (List<String>)jToken.ToObject(typeof(List<String>));

                // Quest Titles
                jToken = jObject.GetValue("SampleQuestTitles");
                QuestTitles= (List<String>)jToken.ToObject(typeof(List<String>));

                // Daily Titles
                jToken = jObject.GetValue("SampleDailies");
                DailiesTitles = (List<String>)jToken.ToObject(typeof(List<String>));

                // XP values allowed
                jToken = jObject.GetValue("XPSelectableValues");
                XPSelectableValues = (List<int>)jToken.ToObject(typeof(List<int>));
            }
        }
    }
}
