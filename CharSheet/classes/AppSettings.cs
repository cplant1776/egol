using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharSheet.classes
{
    public static class AppSettings
    {
        /* Set in json */
        public static string json;
        public static Dictionary<String, String> pagePaths;
        public static Dictionary<int, String> Attributes;
        public static Dictionary<int, String> Skills;
        public static int AttributePointsPerLevel;
        public static int SkillPointsPerLevel;
        public static string SaveDestination;
        public static string ContactImagePath;
        public static string ContactImageFullPath;
        public static int NumOfRecentEvents;
        public static List<int> XPSelectableValues;

        /* Changed during runtime */
        public static string SaveLocation;
        public static string DefaultSelectedQuest;

        public static void InitializeSettings()
        {
            json = File.ReadAllText("../../appSettings.json");
            var jObject = JObject.Parse(json);

            // Set page paths
            var jToken = jObject.GetValue("PagePaths");
            pagePaths = (Dictionary<String, String>)jToken.ToObject(typeof(Dictionary<String, String>));

            // Set points gained per level
            jToken = jObject.GetValue("AttributePointsPerLevel");
            AttributePointsPerLevel = (int)jToken.ToObject(typeof(int));
            jToken = jObject.GetValue("SkillPointsPerLevel");
            SkillPointsPerLevel = (int)jToken.ToObject(typeof(int));

            // Save destination
            jToken = jObject.GetValue("SaveDestination");
            SaveDestination = (string)jToken.ToObject(typeof(string));

            // Attribute List
            jToken = jObject.GetValue("Attributes");
            Attributes = (Dictionary<int, String>)jToken.ToObject(typeof(Dictionary<int, String>));

            // Skill List
            jToken = jObject.GetValue("Skills");
            Skills = (Dictionary<int, String>)jToken.ToObject(typeof(Dictionary<int, String>));

            // Relative path for contact images
            jToken = jObject.GetValue("ContactImagePath");
            ContactImagePath = (string)jToken.ToObject(typeof(string));

            // Absolute path for contact images ContactImageFullPath
            jToken = jObject.GetValue("ContactImageFullPath");
            ContactImageFullPath = (string)jToken.ToObject(typeof(string));

            // Number of displayed recent events on Dashboard
            jToken = jObject.GetValue("NumOfRecentEvents");
            NumOfRecentEvents = (int)jToken.ToObject(typeof(int));

            // Selectable XP values
            jToken = jObject.GetValue("XPSelectableValues");
            XPSelectableValues = (List<int>)jToken.ToObject(typeof(List<int>));
        }

        public static void ResetSaveLocation()
        {
            SaveLocation = null;
        }

        public static void UpdateSaveLocation(string path)
        {
            SaveLocation = path;
        }

        public static void UpdateDefaultSelectedQuest(string title)
        {
            DefaultSelectedQuest = title;
        }

        public static void ResetDefaultSelectedQuest()
        {
            DefaultSelectedQuest = null;
        }
    }
}
