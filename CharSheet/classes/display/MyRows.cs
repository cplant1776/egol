using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CharSheet.classes.display
{
    public class AttributeRow : INotifyPropertyChanged
    {
        private string _attributeName;
        private int _attributeValue;

        public int StartingValue;

        public string AttributeName
        {
            get { return _attributeName; }
            set
            {
                _attributeName = value;
                OnPropertyChanged(() => AttributeName);
            }
        }

        public int AttributeValue
        {
            get { return _attributeValue; }
            set
            {
                _attributeValue = value;
                OnPropertyChanged(() => AttributeValue);
            }
        }
        public AttributeRow()
        {
            
        }
        
        public AttributeRow(string name, int value)
        {
            this.AttributeName = name;
            this.AttributeValue = value;
            this.StartingValue = value;
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

    public class SkillRow : INotifyPropertyChanged
    {
        private string _skillName;
        private int _skillValue;

        public int StartingValue;

        public string SkillName
        {
            get { return _skillName; }
            set
            {
                _skillName = value;
                OnPropertyChanged(() => SkillName);
            }
        }

        public int SkillValue
        {
            get { return _skillValue; }
            set
            {
                _skillValue = value;
                OnPropertyChanged(() => SkillValue);
            }
        }
        public SkillRow()
        {

        }

        public SkillRow(string name, int value)
        {
            this.SkillName = name;
            this.SkillValue = value;
            this.StartingValue = value;
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
