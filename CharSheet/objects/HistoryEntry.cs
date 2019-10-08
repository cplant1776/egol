using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CharSheet.objects
{
    [DataContract]
    public class HistoryEntry
    {
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public int xpValue { get; set; }
        [DataMember]
        public int primarySkill { get; set; }
        [DataMember]
        public string timestamp { get; set; }

        public HistoryEntry(string description, int xp=0, int primarySkill=-1)
        {
            this.description = description;
            this.xpValue = xp;
            this.primarySkill = primarySkill;
            this.timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        }

        public TextBlock GenerateHistoryEntryTextBlock()
        {
            TextBlock text = new TextBlock
            {
                Text = this.description,
                FontSize = 16,
                VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left
            };

            return text;
        }
    }
}
