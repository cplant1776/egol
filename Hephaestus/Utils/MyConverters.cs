using Engine.Utils;
using Engine.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Hephaestus.Utils
{
    public class MyIValueConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public static CharacterModel GetCurrentCharacter()
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            ApplicationViewModel appView = (ApplicationViewModel)mainWindow.DataContext;
            BaseViewModel currentView = (BaseViewModel)appView.CurrentPageViewModel;
            return currentView.UserCharacter;
        }
    }
    public class XPToLevelConverter : MyIValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)value / 100;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class XPToProgressConverter : MyIValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)value % 100;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class XPToProgressConverterString : MyIValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((int)value % 100).ToString() + " / 100";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class DeadlineDateTimeToStringConverter : MyIValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime deadline = (DateTime)value;
            if (deadline == DateTime.MinValue) // no deadline given
            {
                return "N/A";
            }
            else
            {
                return deadline.ToString("MM-dd-yyyy");
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class AttributeIdToAttributeString : MyIValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string attributeName = DataHandler.getAttributeDesc((int)value);
            return attributeName; 
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class SkillIdToSkillString : MyIValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string skillName = DataHandler.getSkillDesc((int)value);
            return skillName;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class PlusButtonName : MyIValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string elementName = DataHandler.getAttributeDesc((int)value);
            return "plus" + elementName;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class MinusButtonName : MyIValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string elementName = DataHandler.getAttributeDesc((int)value);
            return "minus" + elementName;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class EventRecordToString : MyIValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string desc = (value as EventRecordModel).Description;
            string tail = (value as EventRecordModel).TextTail;
            return desc + tail;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
    public class CharacterContactToNameString : MyIValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string contactName = (value as ContactModel).Name;
            return contactName;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class ContactIdToStringConverter : MyIValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string contactName = GetCurrentCharacter().GetContact(targetId: (int)value).Name;
            return contactName;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class QuestCreatedDateTimeToStringConverter : MyIValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime dateCreated = (DateTime)value;
            return dateCreated.ToString("MM-dd-yyyy");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class EventIdToContactNameConverter : MyIValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            CharacterModel currentcharacter = GetCurrentCharacter();
            QuestModel targetQuest = currentcharacter.GetQuest(targetId: (int)value);
            string contactName = currentcharacter.GetContact(targetId: targetQuest.ContactId).Name;
            return contactName;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class EventIdToContactImageConverter : MyIValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            CharacterModel currentcharacter = GetCurrentCharacter();
            QuestModel targetQuest = currentcharacter.GetQuest(targetId: (int)value);
            string contactImg = currentcharacter.GetContact(targetId: targetQuest.ContactId).ImgName;
            if (contactImg == AppSettings.ContactImageFullPath + "default.png")
            {
                return value;
            }
            else
            {
                return contactImg;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class EventIdToContactIconConverter : MyIValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            CharacterModel currentcharacter = GetCurrentCharacter();
            QuestModel targetQuest = currentcharacter.GetQuest(targetId: (int)value);
            string contactName = currentcharacter.GetContact(targetId: targetQuest.ContactId).Name;
            // return icon of 1st letter of contact's name
            if(String.IsNullOrEmpty(contactName))
            {
                return value;
            }
            else
            {
                return AppSettings.PackIconDict[contactName.ToLower()[0]];
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class RemoveCompletedPrefixConverter : MyIValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string fullDescription = (string)value;
            if (fullDescription.StartsWith("Completed"))
            {
                return fullDescription.Remove(startIndex: 0, count: 9);
            }
            else
                return value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class ContactIdToContactImageConverter : MyIValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string contactImg = GetCurrentCharacter().GetContact(targetId: (int)value).ImgName;
            if (contactImg == AppSettings.ContactImageFullPath + "default.png")
            {
                return value;
            }
            else
            {
                return contactImg;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class ContactIdToContactIconConverter : MyIValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string contactName = GetCurrentCharacter().GetContact(targetId:(int)value).Name;
            // return icon of 1st letter of contact's name
            if (String.IsNullOrEmpty(contactName))
            {
                return value;
            }
            else
            {
                return AppSettings.PackIconDict[contactName.ToLower()[0]]; // Return icon matching first char in contact's name
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

}
