using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Utils
{
    public static class AppSettings
    {
        /* Set in json */
        public static string json;
        public static Dictionary<int, String> Attributes;
        public static Dictionary<string, int> reverseAttributes;
        public static Dictionary<int, String> Skills;
        public static Dictionary<String, int> reverseSkills;
        public static int AttributePointsPerLevel;
        public static int SkillPointsPerLevel;
        public static string SaveDestination;
        public static string ImageDirRoot;
        public static int NumOfRecentEvents;
        public static List<int> XPSelectableValues;

        /* Changed during runtime */
        public static string SaveLocation;
        public static int? DefaultSelectedQuestId;
        public static int NumOfLevelsOnLevelUp;
        public static int LoadDuration;
        public static string LoadDestination;

        /*Created at runtime*/
        public static Dictionary<char, PackIconKind> PackIconDict;

        public static void InitializeSettings()
        {
            json = File.ReadAllText("Utils/appSettings.json");
            var jObject = JObject.Parse(json);

            // Set points gained per level
            var jToken = jObject.GetValue("AttributePointsPerLevel");
            AttributePointsPerLevel = (int)jToken.ToObject(typeof(int));
            jToken = jObject.GetValue("SkillPointsPerLevel");
            SkillPointsPerLevel = (int)jToken.ToObject(typeof(int));

            // Save destination
            jToken = jObject.GetValue("SaveDestination");
            SaveDestination = (string)jToken.ToObject(typeof(string));

            // Attribute List
            jToken = jObject.GetValue("Attributes");
            Attributes = (Dictionary<int, String>)jToken.ToObject(typeof(Dictionary<int, String>));

            // Reverse attribute list
            reverseAttributes = (Dictionary<string, int>)Attributes.Reverse();

            // Skill List
            jToken = jObject.GetValue("Skills");
            Skills = (Dictionary<int, String>)jToken.ToObject(typeof(Dictionary<int, String>));

            // Reverse skill list
            reverseSkills = (Dictionary<string, int>)Skills.Reverse();

            // Number of displayed recent events on Dashboard
            jToken = jObject.GetValue("NumOfRecentEvents");
            NumOfRecentEvents = (int)jToken.ToObject(typeof(int));

            // Selectable XP values
            jToken = jObject.GetValue("XPSelectableValues");
            XPSelectableValues = (List<int>)jToken.ToObject(typeof(List<int>));

            GeneratePackIconDict();

            ImageDirRoot = new Uri("pack://application:,,,/media/").AbsolutePath;
        }

        public static void GeneratePackIconDict()
        {
            PackIconDict = new Dictionary<char, PackIconKind>
            {
                {'a', PackIconKind.AlphabetA },
                {'b', PackIconKind.AlphabetB },
                {'c', PackIconKind.AlphabetC },
                {'d', PackIconKind.AlphabetD },
                {'e', PackIconKind.AlphabetE },
                {'f', PackIconKind.AlphabetF },
                {'g', PackIconKind.AlphabetG },
                {'h', PackIconKind.AlphabetH },
                {'i', PackIconKind.AlphabetI },
                {'j', PackIconKind.AlphabetJ },
                {'k', PackIconKind.AlphabetK },
                {'l', PackIconKind.AlphabetL },
                {'m', PackIconKind.AlphabetM },
                {'n', PackIconKind.AlphabetN },
                {'o', PackIconKind.AlphabetO },
                {'p', PackIconKind.AlphabetP },
                {'q', PackIconKind.AlphabetQ },
                {'r', PackIconKind.AlphabetR },
                {'s', PackIconKind.AlphabetS },
                {'t', PackIconKind.AlphabetT },
                {'u', PackIconKind.AlphabetU },
                {'v', PackIconKind.AlphabetV },
                {'w', PackIconKind.AlphabetW },
                {'x', PackIconKind.AlphabetX },
                {'y', PackIconKind.AlphabetY },
                {'z', PackIconKind.AlphabetZ },
            };
        }

        public static void ResetSaveLocation()
        {
            SaveLocation = null;
        }

        public static void UpdateSaveLocation(string path)
        {
            SaveLocation = path;
        }

        public static void UpdateDefaultSelectedQuestId(int id)
        {
            DefaultSelectedQuestId = id;
        }

        public static void ResetDefaultSelectedQuest()
        {
            DefaultSelectedQuestId = null;
        }

        public static Dictionary<TValue, TKey> Reverse<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            var dictionary = new Dictionary<TValue, TKey>();
            foreach (var entry in source)
            {
                if (!dictionary.ContainsKey(entry.Value))
                    dictionary.Add(entry.Value, entry.Key);
            }
            return dictionary;
        }
    }
}
