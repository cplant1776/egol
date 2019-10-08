using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CharSheet.objects
{
    class HistoryEntry
    {
        public int id;
        public string description { get; set; }
        private int skillModified_1 = -1;
        private int skillValue_1 = 0;
        private int skillModified_2 = -1;
        private int skillValue_2 = 0;
        private int attributeModified = -1;
        private int attributeValue = 0;

        public HistoryEntry(string description)
        {
            this.description = description;
        }

        public TextBlock GenerateHistoryEntryTextBlock()
        {
            TextBlock text = new TextBlock
            {
                Text = this.description,
                FontSize = 16,
                VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left
            };

            return text;
        }
    }
}
