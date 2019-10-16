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
    public class EventRecord
    {
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Value { get; set; }
        [DataMember]
        public DateTime Timestamp { get; set; }

        //Functions as both xp entries and milestones for the moment
        //TODO: Rewrite as two inherited classes instead
        public EventRecord(string description, int value=0, DateTime timestamp=new DateTime())
        {
            this.Description = description;
            this.Value = value;
            if (timestamp == DateTime.MinValue) // No timestamp passed
            {
                this.Timestamp = DateTime.UtcNow;
            }
            else
            {
                this.Timestamp = timestamp;
            }
        }

        public TextBlock GenerateEventRecordTextBlock()
        {
            TextBlock text = new TextBlock
            {
                Text = this.Description,
                FontSize = 16,
                VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left
            };

            return text;
        }


    }

    public class XPEvent : EventRecord
    {

        public int PrimarySkill;
        public XPEvent(string description, int value = 0, DateTime timestamp = new DateTime(), int primarySkill = -1) : base(description, value, timestamp)
        {
            this.PrimarySkill = primarySkill;
        }
    }

    public class Milestone : EventRecord
    {
        public int AttributeId;
        public Milestone(string description, int value = 0, DateTime timestamp = new DateTime(), int attributeId = -1) : base(description, value, timestamp)
        {
            this.AttributeId = attributeId;
        }
    }
}
