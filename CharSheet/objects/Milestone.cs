using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharSheet.objects
{
    class Milestone
    {
        private readonly int id;
        public string Description { get;}
        private readonly int attributeId=0;
        private readonly int attributeModifierValue=0;

        public Milestone(int milestoneId, string desc)
        {
            id = milestoneId;
            Description = desc;
        }

    }
}
