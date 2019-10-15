using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharSheet.classes.data
{
    public class Quest
    {
        public String Title;
        public String Description;
        public int Status;
        public int XPValue;
        public int ReputationValue;
        public int ContactId;
        public DateTime Deadline;
        public DateTime Created;

        public enum QuestStatus
        {
            ACCEPTED = 0,
            CURRENT = 1,
            COMPLETED = 2
        }

        public Quest()
        {

        }

        public Quest(String title, String description, int xpValue, int contactId, int reputationValue, DateTime deadline)
        {
            this.Title = title;
            this.Description = description;
            this.XPValue = xpValue;
            this.ContactId = contactId;
            this.ReputationValue = reputationValue;
            this.Deadline = deadline;
            this.Created = DateTime.UtcNow;
            this.Status = (int)QuestStatus.CURRENT;
        }

    }
}
