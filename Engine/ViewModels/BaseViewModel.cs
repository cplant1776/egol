using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.ViewModels
{
        public class BaseViewModel : INotifyPropertyChanged
        {
            private static CharacterModel _userCharacter;
            static BaseViewModel()
            {
                _userCharacter = new CharacterModel();
            }
            protected CharacterModel UserCharacter
            {
                get { return _userCharacter; }
                set {
                        _userCharacter = value;
                        OnPropertyChanged("UserCharacter");
                    }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public void OnPropertyChanged(string name)
            {
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
}
