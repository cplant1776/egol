using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CharSheet.classes;

namespace CharSheet.classes
{
    public static class DataHandler
    {

        public static Dictionary<int, string> attributeStringDict = AppSettings.Attributes;

        public static Dictionary<string, int> attributeIdDict = (Dictionary<string, int>)AppSettings.Attributes.Reverse();


        public static Dictionary<int, string> skillsStringDict = AppSettings.Skills;

        public static Dictionary<string, int> skillsIdDict = (Dictionary<string, int>)AppSettings.Skills.Reverse();

        public static void SaveToXml(Object obj, String destination)
        {
            var serializer = new DataContractSerializer(typeof(Object));
            string xmlString;
            using (var sw = new StringWriter())
            {
                using (var writer = new XmlTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented; // indent the Xml so it's human readable
                    serializer.WriteObject(writer, obj);
                    writer.Flush();
                    xmlString = sw.ToString();
                }
            }
            File.WriteAllText(destination, xmlString);
        }

        public static object ReadFromXml(string xml, Type toType)
        {
            using (Stream stream = new MemoryStream())
            {
                byte[] data = Encoding.UTF8.GetBytes(xml);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                DataContractSerializer deserializer = new DataContractSerializer(toType);
                return deserializer.ReadObject(stream);
            };
        }


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
