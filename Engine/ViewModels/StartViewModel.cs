using Engine.Utils.test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        public StartViewModel()
        {
           
        }

        public void NewCharacter_Click()
        {
            // NAVIGATE TO CHARACTER CREATION
        }

        public void LoadCharacter_Click()
        {
            this.UserCharacter = CharacterModel.LoadCharacter();
        }

        public void GeneratedCharacter_Click()
        {
            this.UserCharacter = new DummyCharacter();
        }
    }
}
