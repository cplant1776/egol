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
        public static string json;
        public static Dictionary<String, String> pagePaths;
        public static int AttributePointsPerLevel;
        public static int SkillPointsPerLevel;
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

        }
    }
}
