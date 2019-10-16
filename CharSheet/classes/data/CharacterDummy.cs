using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CharSheet.classes.data
{
    // In-memory character generated for tesitng purposes
    public partial class Character : INotifyPropertyChanged
    {
        public Character CharacterDummy()
        {
            this.Name = "Example Steve";
            this.CurrentXP = 1060;
            this.ImgName = AppSettings.ContactImageFullPath + "dog.png";

            this.CharacterContacts = GenerateContacts();
            this.Quests = GenerateQuests();
            this.SkillValue = GenerateSkills();
            this.AttributeValue = GenerateAttributes();
            this.EventHistory = GenerateEventHistory();


            return this;
        }

        private List<Contact> GenerateContacts()
        {
            List<Contact> contactList = new List<Contact> { };

            contactList.Add(new Contact(
                name: "Fuzzy Dunlop",
                description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Egestas integer eget aliquet nibh praesent tristique magna sit. Commodo odio aenean sed adipiscing diam donec adipiscing tristique. Commodo ullamcorper a lacus vestibulum sed arcu. Euismod elementum nisi quis eleifend quam adipiscing. Ullamcorper sit amet risus nullam. Facilisis sed odio morbi quis commodo. Platea dictumst vestibulum rhoncus est. Fames ac turpis egestas sed tempus urna et pharetra pharetra. At quis risus sed vulputate odio. Amet consectetur adipiscing elit duis.",
                reputation: 27,
                id : 2847
                    )
                );

            contactList.Add(new Contact(
                name: "Earnest Adrian",
                description: "Malesuada fames ac turpis egestas maecenas pharetra convallis. Posuere morbi leo urna molestie at. At erat pellentesque adipiscing commodo. Sed augue lacus viverra vitae congue eu consequat. In metus vulputate eu scelerisque felis imperdiet proin fermentum leo. Fermentum posuere urna nec tincidunt praesent semper feugiat nibh. Erat nam at lectus urna duis convallis. Venenatis urna cursus eget nunc. Bibendum enim facilisis gravida neque convallis a. Ullamcorper eget nulla facilisi etiam dignissim diam quis enim lobortis. Pulvinar mattis nunc sed blandit libero volutpat. Ultrices dui sapien eget mi proin sed libero enim. Hac habitasse platea dictumst quisque sagittis purus sit. Vel eros donec ac odio tempor orci dapibus ultrices. Semper eget duis at tellus. Id aliquet lectus proin nibh nisl. Ultrices eros in cursus turpis massa tincidunt dui. Sed risus pretium quam vulputate dignissim suspendisse. Sed velit dignissim sodales ut eu.",
                reputation: 0,
                id : 6923
                    )
                );

            contactList.Add(new Contact(
                name: "Mic 'The Arm' Daley",
                description: "Mi in nulla posuere sollicitudin aliquam. Auctor elit sed vulputate mi. Amet justo donec enim diam vulputate ut. Lorem donec massa sapien faucibus. Proin libero nunc consequat interdum varius sit. Turpis in eu mi bibendum neque. Porttitor rhoncus dolor purus non enim. Eget mi proin sed libero enim sed faucibus. Tincidunt id aliquet risus feugiat in. Massa sapien faucibus et molestie ac. Donec et odio pellentesque diam volutpat commodo sed egestas.",
                reputation: 0,
                id : 12
                    )
                );

            contactList.Add(new Contact(
                name: "Bertha Sheppard",
                description: "Tincidunt dui ut ornare lectus. At auctor urna nunc id cursus. Nascetur ridiculus mus mauris vitae ultricies. Nibh sed pulvinar proin gravida. Congue eu consequat ac felis donec. Dis parturient montes nascetur ridiculus mus mauris vitae ultricies leo. In metus vulputate eu scelerisque felis imperdiet proin fermentum leo. Arcu cursus euismod quis viverra nibh. Diam quam nulla porttitor massa id neque. Eget felis eget nunc lobortis mattis aliquam faucibus. Lacus sed turpis tincidunt id aliquet.",
                reputation: 52,
                imgName: AppSettings.ContactImageFullPath + "tester.png",
                id : 7927
                    )
                );

            return contactList;
        }

        private List<Quest> GenerateQuests()
        {
            List<Quest> questList = new List<Quest> { };

            questList.Add(new Quest(
                title : "Create Your First Quest",
                description : "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Faucibus vitae aliquet nec ullamcorper sit amet risus nullam. Nibh nisl condimentum id venenatis. Placerat orci nulla pellentesque dignissim enim sit. Aliquet sagittis id consectetur purus ut. Lectus magna fringilla urna porttitor rhoncus dolor. Id velit ut tortor pretium. Ullamcorper eget nulla facilisi etiam dignissim diam quis. Diam quam nulla porttitor massa id neque aliquam vestibulum morbi. Tellus at urna condimentum mattis pellentesque id. Sed elementum tempus egestas sed sed. Ultrices sagittis orci a scelerisque purus semper eget. Placerat duis ultricies lacus sed turpis tincidunt. Fusce ut placerat orci nulla pellentesque. Orci eu lobortis elementum nibh tellus molestie nunc. Mi tempus imperdiet nulla malesuada pellentesque elit. Diam maecenas ultricies mi eget mauris pharetra et ultrices.",
                xpValue : 5,
                contactId : 2847,
                reputationValue : 13,
                deadline: new DateTime(2008, 5, 1, 8, 30, 52),
                status : (int)Quest.QuestStatus.COMPLETED
                )
            );

            questList.Add(new Quest(
                title: "Do A Backflip",
                description: "At risus viverra adipiscing at in. Sit amet porttitor eget dolor morbi non arcu risus. Diam ut venenatis tellus in metus vulputate eu scelerisque. Elementum pulvinar etiam non quam lacus. Iaculis at erat pellentesque adipiscing commodo. Enim tortor at auctor urna nunc id cursus metus. Augue eget arcu dictum varius duis at consectetur lorem donec. Metus aliquam eleifend mi in nulla posuere sollicitudin aliquam ultrices. Est placerat in egestas erat imperdiet sed euismod nisi. Neque vitae tempus quam pellentesque nec nam. Dignissim suspendisse in est ante. Velit aliquet sagittis id consectetur purus ut faucibus pulvinar. Cras tincidunt lobortis feugiat vivamus. Viverra ipsum nunc aliquet bibendum. Natoque penatibus et magnis dis parturient montes nascetur ridiculus mus. Ornare aenean euismod elementum nisi.",
                xpValue: 17,
                contactId: 2847,
                reputationValue: 76,
                deadline: new DateTime(2009, 5, 1, 8, 30, 52),
                status: (int)Quest.QuestStatus.CURRENT
                )
            );

            questList.Add(new Quest(
                title: "Bowl a 300",
                description: "Montes nascetur ridiculus mus mauris vitae ultricies. Nisi vitae suscipit tellus mauris a. Phasellus vestibulum lorem sed risus ultricies. Purus faucibus ornare suspendisse sed nisi lacus. Nullam ac tortor vitae purus faucibus ornare. Aliquam ut porttitor leo a diam sollicitudin tempor id eu. Bibendum ut tristique et egestas quis ipsum suspendisse. Morbi tristique senectus et netus. Suspendisse interdum consectetur libero id. Mi eget mauris pharetra et. Leo duis ut diam quam nulla porttitor. Lorem ipsum dolor sit amet consectetur adipiscing elit pellentesque habitant. Ullamcorper eget nulla facilisi etiam dignissim diam quis enim. Leo a diam sollicitudin tempor id. Aliquam ut porttitor leo a diam sollicitudin tempor id.",
                xpValue: 125,
                contactId: 6923,
                reputationValue: 6,
                deadline: new DateTime(2010, 5, 1, 8, 30, 52),
                status: (int)Quest.QuestStatus.CURRENT
                )
            );

            questList.Add(new Quest(
                title: "Cook a Filet Mignon",
                description: "Eu sem integer vitae justo eget magna fermentum iaculis eu. Sapien nec sagittis aliquam malesuada bibendum arcu vitae elementum curabitur. Velit scelerisque in dictum non consectetur a erat nam at. Ultrices sagittis orci a scelerisque. Diam volutpat commodo sed egestas egestas fringilla. Porttitor leo a diam sollicitudin. Adipiscing bibendum est ultricies integer quis auctor elit. Sit amet consectetur adipiscing elit. Justo eget magna fermentum iaculis eu non. Eu lobortis elementum nibh tellus. Interdum velit laoreet id donec ultrices tincidunt arcu non sodales. Sapien nec sagittis aliquam malesuada bibendum arcu vitae elementum. Vitae congue eu consequat ac felis donec et. Vel pretium lectus quam id leo in vitae. Lacus laoreet non curabitur gravida. Erat imperdiet sed euismod nisi porta lorem mollis aliquam. Odio tempor orci dapibus ultrices in iaculis nunc sed.",
                xpValue: 10,
                contactId: 12,
                reputationValue: -10,
                deadline: new DateTime(2011, 5, 1, 8, 30, 52),
                status: (int)Quest.QuestStatus.COMPLETED
                )
            );

            questList.Add(new Quest(
                title: "Run a Marathon",
                description: "Adipiscing bibendum est ultricies integer quis auctor elit sed vulputate. Tristique risus nec feugiat in fermentum posuere urna nec tincidunt. Mauris sit amet massa vitae tortor condimentum lacinia quis. Nulla pellentesque dignissim enim sit amet venenatis. Faucibus in ornare quam viverra orci sagittis eu volutpat. Eros in cursus turpis massa tincidunt dui. Malesuada pellentesque elit eget gravida cum sociis natoque penatibus. Quis viverra nibh cras pulvinar mattis nunc sed. Eu nisl nunc mi ipsum faucibus vitae aliquet. Nisi scelerisque eu ultrices vitae auctor eu. Faucibus in ornare quam viverra orci sagittis. Mi eget mauris pharetra et. Egestas erat imperdiet sed euismod nisi porta lorem mollis aliquam. Turpis massa tincidunt dui ut ornare lectus sit. Vulputate dignissim suspendisse in est ante in nibh. Vel elit scelerisque mauris pellentesque pulvinar pellentesque habitant morbi. Orci phasellus egestas tellus rutrum tellus pellentesque eu. Bibendum ut tristique et egestas quis ipsum.",
                xpValue: 42,
                contactId: 7927,
                reputationValue: -19,
                deadline: new DateTime(2012, 5, 1, 8, 30, 52),
                status: (int)Quest.QuestStatus.ACCEPTED
                )
            );

            return questList;
        }

        private List<EventRecord> GenerateEventHistory()
        {
            List<EventRecord> result = new List<EventRecord> { };

            result.Add(new XPEvent(
                    description : "Lorem ipsum dolor sit amet",
                    value : 15,
                    primarySkill : 1,
                    timestamp : new DateTime(2008, 5, 1, 8, 30, 52)
                    )
                );

            result.Add(new XPEvent(
                    description: "Blandit volutpat maecenas volutpat blandit aliquam etiam",
                    value: 26,
                    primarySkill: 2,
                    timestamp: new DateTime(2009, 5, 1, 8, 30, 52)
                    )
                );

            result.Add(new XPEvent(
                    description: "Lobortis elementum nibh tellus molestie nunc",
                    value: 10,
                    primarySkill: 3,
                    timestamp: new DateTime(2010, 5, 1, 8, 30, 52)
                    )
                );

            result.Add(new XPEvent(
                    description: "Libero volutpat sed cras ornare",
                    value: 20,
                    timestamp: new DateTime(2011, 5, 1, 8, 30, 52)
                    )
                );

            result.Add(new Milestone(
                    description: "Molestie at elementum eu facilisis sed odio morbi quis commodo",
                    value: 30,
                    attributeId: 2,
                    timestamp: new DateTime(2012, 5, 1, 8, 30, 52)
                    )
                );

            result.Add(new XPEvent(
                    description: "Ut morbi tincidunt augue interdum velit euismod",
                    value: 40,
                    primarySkill: 6,
                    timestamp: new DateTime(2013, 5, 1, 8, 30, 52)
                    )
                );

            result.Add(new XPEvent(
                    description: "Purus viverra accumsan in nisl nisi",
                    value: 50,
                    primarySkill: 7,
                    timestamp: new DateTime(2014, 5, 1, 8, 30, 52)
                    )
                );

            result.Add(new XPEvent(
                    description: "Pellentesque elit ullamcorper dignissim cras tincidunt lobortis feugiat",
                    value: 60,
                    primarySkill: 8,
                    timestamp: new DateTime(2015, 5, 1, 8, 30, 52)
                    )
                );

            result.Add(new XPEvent(
                    description: "Morbi tristique senectus et netus et",
                    value: 70,
                    primarySkill: 9,
                    timestamp: new DateTime(2016, 5, 1, 8, 30, 52)
                    )
                );

            result.Add(new XPEvent(
                    description: "Amet volutpat consequat mauris nunc congue nisi",
                    value: 80,
                    primarySkill: 10,
                    timestamp: new DateTime(2017, 5, 1, 8, 30, 52)
                    )
                );

            result.Add(new XPEvent(
                    description: "Erat pellentesque adipiscing commodo elit at. Duis tristique sollicitudin nibh sit",
                    value: 90,
                    primarySkill: 11,
                    timestamp: new DateTime(2018, 5, 1, 8, 30, 52)
                    )
                );

            result.Add(new XPEvent(
                    description: "Vitae auctor eu augue ut lectus arcu bibendum",
                    value: 110,
                    primarySkill: 12,
                    timestamp: new DateTime(2019, 5, 1, 8, 30, 52)
                    )
                );

            return result;
        }

        private Dictionary<int, int> GenerateSkills()
        {
            Dictionary<int, int> result = new Dictionary<int, int> { };

            // Placeholder generated skill list
            var tupleList = new List<(int, int)>
            {
                (1, 10),
                (2, 20),
                (3, 30),
                (4, 40),
                (5, 50),
                (6, 60),
                (7, 70),
                (8, 80),
                (9, 90),
                (10, 100),
                (11, 110),
                (12, 120),
                (13, 130)
            };

            foreach (var t in tupleList)
            {
                result.Add(t.Item1, t.Item2);
            }

            return result;
        }

        public Dictionary<int, int> GenerateAttributes()
        {
            Dictionary<int, int> result = new Dictionary<int, int> { };

            // Placeholder generated attribute list
            var tupleList = new List<(int, int)>
            {
                (1, 10),
                (2, 20),
                (3, 30),
                (4, 40),
                (5, 50),
                (6, 60),
                (7, 70),
                (8, 80)
            };

            foreach (var t in tupleList)
            {
                result.Add(t.Item1, t.Item2);
            }

            return result;
        }

        public static Dictionary<int, string> attributeStringDict = new Dictionary<int, string>
        {
            {1, "Attribute 1" },
            {2, "Attribute 2" },
            {3, "Attribute 3" },
            {4, "Attribute 4" },
            {5, "Attribute 5" },
            {6, "Attribute 6" },
            {7, "Attribute 7" },
            {8, "Attribute 8" }
        };

        public static Dictionary<string, int> attributeIdDict = new Dictionary<string, int>
        {
            {"Attribute 1", 1},
            {"Attribute 2" , 2},
            {"Attribute 3", 3 },
            {"Attribute 4", 4 },
            {"Attribute 5", 5 },
            {"Attribute 6", 6 },
            {"Attribute 7", 7 },
            {"Attribute 8", 8 }
        };


        public static Dictionary<int, string> skillsStringDict = new Dictionary<int, string>
        {
            {1, "Skill 1" },
            {2, "Skill 2" },
            {3, "Skill 3" },
            {4, "Skill 4" },
            {5, "Skill 5" },
            {6, "Skill 6" },
            {7, "Skill 7" },
            {8, "Skill 8" },
            {9, "Skill 9" },
            {10, "Skill 10" },
            {11, "Skill 11" },
            {12, "Skill 12" },
            {13, "Skill 13" }
        };

        public static Dictionary<string, int> skillsIdDict = new Dictionary<string, int>
        {
            {"Skill 1", 1 },
            {"Skill 2", 2 },
            {"Skill 3", 3 },
            {"Skill 4", 4 },
            {"Skill 5", 5 },
            {"Skill 6", 6 },
            {"Skill 7", 7 },
            {"Skill 8", 8 },
            {"Skill 9", 9 },
            {"Skill 10", 10 },
            {"Skill 11", 11 },
            {"Skill 12", 12 },
            {"Skill 13", 13 }
        };

        public static string getSkillDesc(int n)
        {
            return skillsStringDict[n];
        }

        public static int getSkillId(string s)
        {
            return skillsIdDict[s];
        }

        public static string getAttributeDesc(int n)
        {
            return attributeStringDict[n];
        }

        public static int getAttributeId(string s)
        {
            return attributeIdDict[s];
        }
    }
}
