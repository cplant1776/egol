using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CharSheet.classes
{
    [DataContract]
    [KnownType(typeof(XPEvent)), KnownType(typeof(Milestone))]
    public class EventRecord : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        // Just so we can call it via lambda which is nicer
        public void OnPropertyChanged<T>(Expression<Func<T>> propertyNameExpression)
        {
            OnPropertyChanged(((MemberExpression)propertyNameExpression.Body).Member.Name);
        }
    }

    [DataContract]
    public class XPEvent : EventRecord
    {
        [DataMember]
        public int PrimarySkill;

        public XPEvent(string description, int primarySkill, int value = 0, DateTime timestamp = new DateTime()) : base(description, value, timestamp)
        {
            this.PrimarySkill = primarySkill;
            this.TextTail = "    +" + this.Value + " xp!";
        }
    }

    [DataContract]
    public class Milestone : EventRecord
    {
        [DataMember]
        public int AttributeId;
        public Milestone(string description, int attributeId, int value = 0, DateTime timestamp = new DateTime()) : base(description, value, timestamp)
        {
            this.AttributeId = attributeId;
            this.TextTail = "    +" + value + " " + DataHandler.getAttributeDesc(attributeId) + "!";
        }
    }
}
