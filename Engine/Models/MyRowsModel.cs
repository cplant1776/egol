using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class StatRow : INotifyPropertyChanged
    {
        private string _name;
        private int _value;

        public int StartingValue;
        public bool IsSkill;

        public string StatName
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(() => StatName);
            }
        }

        public int StatValue
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged(() => StatValue);
            }
        }
        public StatRow()
        {

        }

        public StatRow(string name, int value, bool isSkill = false)
        {
            this.StatName = name;
            this.StatValue = value;
            this.StartingValue = value;
            this.IsSkill = isSkill;
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