using Engine.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.ViewModels
{
        public class BaseViewModel : MyObservableObject
        {
            private static CharacterModel _userCharacter;
            static BaseViewModel()
            {
                _userCharacter = new CharacterModel();
            }
            protected CharacterModel UserCharacter
            {
                get { return _userCharacter; }
                set
                {
                    if(value != _userCharacter)
                        {
                            _userCharacter = value;
                            OnPropertyChanged("UserCharacter");
                        }
                }
            }
        }
}
