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
        public static string tester;

        public static void InitializeSettings()
        {
            json = File.ReadAllText("../../appSettings.json");
            var jObject = JObject.Parse(json);

            var jToken = jObject.GetValue("PagePaths");
            pagePaths = (Dictionary<String, String>)jToken.ToObject(typeof(Dictionary<String, String>));
        }
    }
}
