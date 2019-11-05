using Engine.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Engine.ViewModels
{
        [DataContract]
        public class EventRecordModel : INotifyPropertyChanged
        {
            [DataMember]
            public string Description { get; set; }

            [DataMember]
            public string TextTail { get; set; }
            [DataMember]
            public int Value { get; set; }
            [DataMember]
            public int AssociatedEventId { get; set; }
            [DataMember]
            public DateTime Timestamp { get; set; }


            public EventRecordModel(string description, int eventId, int value = 0, DateTime timestamp = new DateTime())
            {
                this.Description = description;
                this.Value = value;
                this.AssociatedEventId = eventId;
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
        [KnownType(typeof(EventRecordModel))]
        [KnownType(typeof(XPEventModel))]
    public class XPEventModel : EventRecordModel
        {
            public int PrimarySkill;

            public XPEventModel(string description, int primarySkill, int eventId, int value = 0, DateTime timestamp = new DateTime()) : base(description, eventId, value, timestamp)
            {
                this.PrimarySkill = primarySkill;
                this.TextTail = "    +" + this.Value + " xp!";
            }
        }

        [DataContract]
        [KnownType(typeof(EventRecordModel))]
        [KnownType(typeof(MilestoneModel))]
    public class MilestoneModel : EventRecordModel
        {
            public int AttributeId;
            public MilestoneModel(string description, int eventId, int attributeId, int value = 0, DateTime timestamp = new DateTime()) : base(description, eventId, value, timestamp)
            {
                this.AttributeId = attributeId;
                this.TextTail = "    +" + value + " " + DataHandler.getAttributeDesc(attributeId) + "!";
                // Generate Id
                Random rnd = new Random();
                this.AssociatedEventId = rnd.Next(1, 600000); // generate random id
            }
        }
    }