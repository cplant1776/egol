using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharSheet.objects
{
    public struct AttributeRow
    {
        public String AttributeName { get; set; }
        public String PlusIconPath { get; set; }
        public String MinusIconPath { get; set; }
        public int AttributeValue { get; set; }
        public int GridRow { get; set; }

        public AttributeRow(String name, int val, int r)
        {
            AttributeName = name;
            AttributeValue = val;
            GridRow = r;

            PlusIconPath = "/media/icons/Add_grey_16xMD.png";
            MinusIconPath = "/media/icons/Subtract_16x.png";
        }
    }
}
