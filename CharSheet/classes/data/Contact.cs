using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CharSheet.classes.data
{
    public class Contact : INotifyPropertyChanged
    {

        private String _name;
        private String _description;
        private int _reputation;
        private string _imgPath;

        public int Id { get; set; }
        public String Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(() => Name);
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
        public int Reputation
        {
            get { return _reputation; }
            set
            {
                _reputation = value;
                OnPropertyChanged(() => Reputation);
            }
        }
        public String ImgPath
        {
            get { return _imgPath; }
            set
            {
                _imgPath = value;
                OnPropertyChanged(() => ImgPath);
            }
        }

        public Contact()
        {
            this.Name = "";
            this.Description = "";
            this.Reputation = 0;
            this.ImgPath = "";
        }

        public Contact(String name, String description, int reputation=0, String imgPath="")
        {
            this.Name = name;
            this.Description = description;
            this.Reputation = reputation;
            this.ImgPath = imgPath;
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
