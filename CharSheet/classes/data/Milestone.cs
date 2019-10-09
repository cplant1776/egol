using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CharSheet.objects
{
    [DataContract]
    public class Milestone
    {
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        private int AttributeId { get; set; }
        [DataMember]
        private int AttributeModifierValue { get; set; }
        [DataMember]
        public string timestamp { get; set; }

        public Milestone(string desc, int attribute, int attributeValue)
        {
            this.Description = desc;
            this.AttributeId = attribute;
            this.AttributeModifierValue = attributeValue;
            this.timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        }

    }
}
