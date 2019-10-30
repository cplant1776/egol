using Engine.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Engine.Utils
{
    public static class DataHandler
    {
        public static Dictionary<int, string> attributeStringDict = AppSettings.Attributes;

        public static Dictionary<string, int> attributeIdDict = AppSettings.reverseAttributes;

        public static Dictionary<int, string> skillsStringDict = AppSettings.Skills;

        public static Dictionary<string, int> skillsIdDict = AppSettings.reverseSkills;

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

        public static CharacterModel LoadCharacterFromXml(string origin)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(origin);
            return (CharacterModel)ReadFromXml(doc.OuterXml, typeof(CharacterModel));
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

        public static string UploadImageAndGetPath()
        {
            string filePath = "";
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                // Set filter for file extension and default file extension 
                DefaultExt = ".png",
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            };

            // Get the selected file name and load a character with it
            if (dlg.ShowDialog() == true)
            {
                string source = dlg.FileName;
                string destination = AppSettings.ContactImageFullPath + System.IO.Path.GetFileName(source);
                // Copy file to resources
                System.IO.File.Copy(source, destination, true);
                filePath = destination;
            }
            return filePath;
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
            try
            {
                return attributeStringDict[n];
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Failed on " + n.ToString());
                return "-1";
            }
        }

        public static int getAttributeId(string s)
        {
            return attributeIdDict[s];
        }
    }

}
