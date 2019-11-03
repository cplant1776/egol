using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Engine.ViewModels
{
    public class QuestModel : INotifyPropertyChanged
    {
        private String _title;
        private String _description;
        private int _xpValue;
        private int _reputatinValue;
        private DateTime _deadline;
        private DateTime _created;

        public int Id;

        public String Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(() => Title);
            }
        }
        public String Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(() => Description);
            }
        }
        public int Status;
        public int XPValue
        {
            get { return _xpValue; }
            set
            {
                _xpValue = value;
                OnPropertyChanged(() => XPValue);
            }
        }
        public int ReputationValue
        {
            get { return _reputatinValue; }
            set
            {
                _reputatinValue = value;
                OnPropertyChanged(() => ReputationValue);
            }
        }
        public int ContactId { get; set; }
        public DateTime Deadline
        {
            get { return _deadline; }
            set
            {
                _deadline = value;
                OnPropertyChanged(() => Deadline);
            }
        }
        public DateTime Created
        {
            get { return _created; }
            set
            {
                _created = value;
                OnPropertyChanged(() => Created);
            }
        }

        public enum QuestStatus
        {
            ACCEPTED = 0,
            ACTIVE = 1,
            COMPLETED = 2
        }

        public QuestModel()
        {
            this.Id = -1;
        }

        public QuestModel(String title, String description, int xpValue, int contactId,
            int reputationValue, DateTime deadline = new DateTime(), int status = -1, int id = -1)
        {
            this.Title = title;
            this.Description = description;
            this.XPValue = xpValue;
            this.ContactId = contactId;
            this.ReputationValue = reputationValue;
            this.Deadline = deadline;
            this.Created = DateTime.UtcNow;
            if (status == -1)
            {
                this.Status = (int)QuestStatus.ACCEPTED;
            }
            else
            {
                this.Status = status;
            }

            //Generate Id
            if (id == -1)
            {
                Random rnd = new Random();
                this.Id = rnd.Next(1, 600000); // generate random id
            }
            else
            {
                this.Id = id;
            }

        }

        public override string ToString()
        {
            return this.Title;
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
}