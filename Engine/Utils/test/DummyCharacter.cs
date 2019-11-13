using Engine.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static Engine.ViewModels.EventRecordModel;

namespace Engine.Utils.test
{
    [DataContract]
    [KnownType(typeof(CharacterModel))]
    [KnownType(typeof(DummyCharacter))]
    [KnownType(typeof(QuestModel))]
    [KnownType(typeof(ContactModel))]
    [KnownType(typeof(XPEventModel))]
    [KnownType(typeof(MilestoneModel))]
    [KnownType(typeof(EventRecordModel))]
    class DummyCharacter : CharacterModel
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

        public List<ContactModel> GenerateContacts()
        {
            DATE_RANGE = (DateTime.Today - START_DATE).Days;
            string contactName = "";

            List<ContactModel> contactList = new List<ContactModel> { };
            for (int i = 0; i < NUM_OF_CONTACTS; i++)
            {
                // Make sure contact names are unique if possible
                if(NUM_OF_CONTACTS < DummyResources.ContactNames.Count)
                {
                    while (true)
                    {
                        contactName = GetRandomName();
                        if (contactList.Any(x => x.Name == contactName))
                            continue;
                        break;
                    }
                }
                else
                {
                    contactName = GetRandomName();
                }

                contactList.Add(new ContactModel(
                    name: contactName,
                    description: GetRandomDescription(),
                    reputation: RandomIntUnder(100),
                    imgName: GetRandomImage(),
                    id: GetRandomId()
                    )
                );
            }
            return contactList;
        }

        public List<QuestModel> GenerateQuests()
        {
            List<QuestModel> questList = new List<QuestModel> { };
            DateTime deadline = new DateTime();

            for (int i = 0; i < NUM_OF_QUESTS; i++)
            {
                if (RandomIntUnder(100) > 50)
                    deadline = GetRandomDateTime();
                else
                    deadline = new DateTime();

                questList.Add(new QuestModel(
                    title: GetRandomQuestTitle(),
                    description: GetRandomDescription(),
                    xpValue: GetRandomXPValue(),
                    contactId: GetRandomContactId(),
                    reputationValue: RandomIntUnder(MAX_REP_GAIN),
                    deadline: deadline,
                    status: GetRandomStatus(),
                    created: GetRandomDateTime(),
                    id: GetRandomId()
                    )
                );
            }

            return questList;
        }

        public List<EventRecordModel> GenerateEventHistory()
        {
            List<EventRecordModel> eventRecords = new List<EventRecordModel> { };
            List<QuestModel> completedQuests = new List<QuestModel> { };
            // Find completed quests
            completedQuests = this.Quests.Where(q => q.Status == (int)QuestModel.QuestStatus.COMPLETED).ToList();
            //foreach (QuestModel q in this.Quests)
            //{
            //    if (q.Status == (int)QuestModel.QuestStatus.COMPLETED)
            //        completedQuests.Add(q);
            //}
            foreach(QuestModel cq in completedQuests)
            {
                eventRecords.Add(GenerateCompletedEvent(cq));
            }
            // Sprinkle in a milestone
            eventRecords.Add(GetRandomMilestone());
            return eventRecords;
        }

        public Dictionary<int, int> GenerateAttributeValues()
        {
            Dictionary<int, int> AttributeValues = new Dictionary<int, int> { };
            for (int i = 1; i < NUM_OF_ATTRIBUTES + 1; i++)
            {
                AttributeValues[i] = RandomIntUnder(20);
            }
            return AttributeValues;
        }

        public Dictionary<int, int> GenerateSkillValues()
        {
            Dictionary<int, int> SkillValues = new Dictionary<int, int> { };
            for (int i = 1; i < NUM_OF_SKILLS + 1; i++)
            {
                SkillValues[i] = RandomIntUnder(20);
            }
            return SkillValues;
        }

        public void GenerateAttributeAndSkillDicts()
        {
            string k;
            for (int i = 1; i < NUM_OF_ATTRIBUTES + 1; i++)
            {
                k = "Attribute " + i.ToString();
                this.AttributeStringDict[i] = k;
                this.AttributeIdDict[k] = i;
            }
            for (int i = 1; i < NUM_OF_SKILLS + 1; i++)
            {
                k = "Skill " + i.ToString();
                this.SkillsStringDict[i] = k;
                this.SkillsIdDict[k] = i;
            }
        }

        public int GetXPFromHistory()
        {
            int totalXP = 0;
            foreach (QuestModel q in this.Quests)
            {
                if (q.Status == 2) //If quest is completed
                    totalXP += q.XPValue;
            }
            return totalXP;
        }

        public EventRecordModel GenerateCompletedEvent(QuestModel cq)
        {
            return new XPEventModel(
                    description: "Completed " + cq.Title,
                    eventId: cq.Id,
                    value: cq.XPValue,
                    primarySkill: RandomIntUnder(NUM_OF_SKILLS),
                    timestamp: cq.Created
                );
        }

        public EventRecordModel GenerateRandomEvent()
        {
            int i = rnd.Next(101);
            if (i < 10) //Small chance of generating a milestone
                return GetRandomMilestone();
            else
                return GetRandomXPEvent();
        }

        public XPEventModel GetRandomXPEvent()
        {
            int eventId = GetRandomQuestId();

            return new XPEventModel(
                    description: GetRandomXPEventDescription(),
                    eventId: eventId,
                    value: GetEventValue(eventId),
                    primarySkill: RandomIntUnder(NUM_OF_SKILLS),
                    timestamp: GetRandomDateTime()
                );
        }

        public MilestoneModel GetRandomMilestone()
        {
            return new MilestoneModel(
                description: "Sample Milestone",
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
            foreach (QuestModel q in this.Quests)
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
            for (int i = 0; i < rnd.Next(1, MAX_NUM_LOREM_PARAGRAPHS); i++)
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
                json = File.ReadAllText("Utils/test/DummyDataResources.json");
                var jObject = JObject.Parse(json);

                // Contact Names
                var jToken = jObject.GetValue("SampleContactNames");
                ContactNames = (List<String>)jToken.ToObject(typeof(List<String>));

                // Contact images
                jToken = jObject.GetValue("SampleContactImages");
                ContactImages = (List<String>)jToken.ToObject(typeof(List<String>));

                // Quest Titles
                jToken = jObject.GetValue("SampleQuestTitles");
                QuestTitles = (List<String>)jToken.ToObject(typeof(List<String>));

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
