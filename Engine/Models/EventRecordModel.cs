using Engine.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Engine.ViewModels
{
        public class EventRecordModel : INotifyPropertyChanged
        {
            public string Description { get; set; }

            public string TextTail { get; set; }
            public int Value { get; set; }
            public int AssociatedEventId { get; set; }
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

        public class XPEventModel : EventRecordModel
        {
            public int PrimarySkill;

            public XPEventModel(string description, int primarySkill, int eventId, int value = 0, DateTime timestamp = new DateTime()) : base(description, eventId, value, timestamp)
            {
                this.PrimarySkill = primarySkill;
                this.TextTail = "    +" + this.Value + " xp!";
            }
        }

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