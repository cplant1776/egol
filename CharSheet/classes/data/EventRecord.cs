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
        public string TextTail { get; set; }
        [DataMember]
        public int Value { get; set; }
        [DataMember]
        public DateTime Timestamp { get; set; }


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
    }

    public class XPEvent : EventRecord
    {

        public int PrimarySkill;

        public XPEvent(string description, int primarySkill, int value = 0, DateTime timestamp = new DateTime()) : base(description, value, timestamp)
        {
            this.PrimarySkill = primarySkill;
            this.TextTail = "    +" + this.Value + " xp!";
        }
    }

    public class Milestone : EventRecord
    {
        public int AttributeId;
        public Milestone(string description, int attributeId, int value = 0, DateTime timestamp = new DateTime()) : base(description, value, timestamp)
        {
            this.AttributeId = attributeId;
            this.TextTail = "    +" + value + " " + DataHandler.getAttributeDesc(attributeId) + "!";
        }
    }
}
