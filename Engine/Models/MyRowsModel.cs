using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class StatRow : MyObservableObject
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
                OnPropertyChanged("StatName");
            }
        }

        public int StatValue
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged("StatValue");
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
    }
}