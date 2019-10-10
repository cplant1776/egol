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
    public class DataHandler
    {

        public static Dictionary<int, string> attributeDict = new Dictionary<int, string>
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

        public static Dictionary<int, string> skillsDict = new Dictionary<int, string>
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

        public DataHandler()
        {

        }

        public void SaveToXml(Object obj, String destination)
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

        public object ReadFromXml(string xml, Type toType)
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
            return skillsDict[n];
        }

        public static string getAttributeDesc(int n)
        {
            return attributeDict[n];
        }

        public List<Milestone> GenerateNewMilestones()
        {
            return new List<Milestone>
            {
                new Milestone("Placeholder Milestone Description", -1, -1)
            };
        }

        public List<HistoryEntry> GenerateNewEventHistory()
        {
            return new List<HistoryEntry>
            {
                new HistoryEntry("History entry description right here...default values used", false, 99, -1)
            };
        }

        public Dictionary<int, int> GenerateNewSkillValue()
        {
            Dictionary<int, int> result = new Dictionary<int, int> { };

            // Placeholder generated skill list
            var tupleList = new List<(int, int)>
            {
                (1, 0),
                (2, 0),
                (3, 0),
                (4, 0),
                (5, 0),
                (6, 0),
                (7, 0),
                (8, 0),
                (9, 0),
                (10, 0),
                (11, 0),
                (12, 0),
                (13, 0)
            };

            foreach(var t in tupleList)
            {
                result.Add(t.Item1, t.Item2);
            }

            return result;
        }

        public Dictionary<int, int> GenerateNewAttributeValue()
        {
            Dictionary<int, int> result = new Dictionary<int, int> { };

            // Placeholder generated attribute list
            var tupleList = new List<(int, int)>
            {
                (1, 0),
                (2, 0),
                (3, 0),
                (4, 0),
                (5, 0),
                (6, 0),
                (7, 0),
                (8, 0)
            };

            foreach (var t in tupleList)
            {
                result.Add(t.Item1, t.Item2);
            }

            return result;
        }
    }
}
