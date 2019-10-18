using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CharSheet.classes.display
{
    public class AttributeModifierButton : Button
    {
        public int StartingValue
        {
            get { return (int)GetValue(StartingValueProperty); }
            set { SetValue(StartingValueProperty, value); }
        }

        public static readonly DependencyProperty StartingValueProperty = DependencyProperty.RegisterAttached(
            "StartingValue", typeof(int), typeof(AttributeModifierButton));

    }

    public class SkillModifierButton : Button
    {
        public int StartingValue
        {
            get { return (int)GetValue(StartingValueProperty); }
            set { SetValue(StartingValueProperty, value); }
        }

        public static readonly DependencyProperty StartingValueProperty = DependencyProperty.RegisterAttached(
            "StartingValue", typeof(int), typeof(SkillModifierButton));
    }
}