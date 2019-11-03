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
    public class ContactModel : INotifyPropertyChanged
    {
        private String _name;
        private String _description;
        private int _reputation;
        private string _imgName;

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
        public String ImgName
        {
            get { return _imgName; }
            set
            {
                _imgName = value;
                OnPropertyChanged(() => ImgName);
            }
        }

        public ContactModel()
        {
            this.Name = "";
            this.Description = "";
            this.Reputation = 0;
            this.ImgName = AppSettings.ContactImageFullPath + "default.png";
        }

        public ContactModel(String name, String description, int reputation = 0, String imgName = "", int id = -1)
        {
            this.Name = name;
            this.Description = description;
            this.Reputation = reputation;

            // Image path
            if (imgName != "")
            {
                this.ImgName = imgName;
            }
            else
            {
                this.ImgName = AppSettings.ContactImageFullPath + "default.png";
            }

            // Id
            Random rnd = new Random();
            if (id < 0)
            {
                this.Id = rnd.Next(1, 60000); // generate random id
            }
            else
            {
                this.Id = id;
            }

        }

        public string SetImage()
        {
            this.ImgName = DataHandler.UploadImageAndGetPath();
            return this.ImgName;
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
