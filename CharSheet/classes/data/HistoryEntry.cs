using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CharSheet.classes
{
    [DataContract]
    public class HistoryEntry
    {
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public bool isMilestone { get; set; }
        [DataMember]
        public int value { get; set; }
        [DataMember]
        public int primarySkill { get; set; }
        [DataMember]
        public string timestamp { get; set; }

        //Functions as both xp entries and milestones for the moment
        //TODO: Rewrite as two inherited classes instead
        public HistoryEntry(string description, bool isMilestone, int value=0, int primarySkill=-1)
        {
            this.description = description;
            this.isMilestone = isMilestone;
            this.value = value;
            this.primarySkill = primarySkill;
            this.timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
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
