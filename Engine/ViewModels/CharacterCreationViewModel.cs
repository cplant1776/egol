using Engine.Models;
using Engine.Utils;
using Engine.Utils.test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Engine.ViewModels
{
    public class CharacterCreationViewModel : BaseViewModel
    {

        private List<StatRow> _attributeRows = new List<StatRow> { };
        private List<StatRow> _skillRows = new List<StatRow> { };
        private string _imgName;

        public string ImgName { get; set; }

        public List<StatRow> AttributeRows { get { return _attributeRows; } set { _attributeRows = value; } }

        public List<StatRow> SkillRows { get { return _skillRows; } set { _skillRows = value; } }
        public CharacterCreationViewModel()
        {
            this.UserCharacter = new DummyCharacter();
            GenerateStatRows();
            this.ImgName = this.UserCharacter.ImgName;
        }

        private void GenerateStatRows()
        {
            // Generate attribute rows
            foreach (KeyValuePair<int, int> entry in this.UserCharacter.AttributeValue)
            {
                this.AttributeRows.Add(new StatRow(name: DataHandler.getAttributeDesc(entry.Key), value: entry.Value, isSkill: false));
            }

            // Generate skill rows
            foreach (KeyValuePair<int, int> entry in this.UserCharacter.SkillValue)
            {
                this.SkillRows.Add(new StatRow(name: DataHandler.getSkillDesc(entry.Key), value: entry.Value, isSkill: true));
            }
        }

        public void Done_Click(string name, string description)
        {
            this.UserCharacter = new CharacterModel(
                name: name,
                description: description,
                attributes: this.AttributeRows,
                skills: this.SkillRows
                );
            // NAVIGATE TO DASHBOARD
        }

        public void Cancel_Click()
        {
            // NAVIGATE TO START PAGE
        }

        public void PlusAttribute_Click(object sender)
        {
            // Find Sending Row
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow row)
                {
                    // Get sent attribute
                    StatRow tar = (StatRow)row.Item;
                    //Find target attribute in current character
                    foreach (StatRow r in this.AttributeRows)
                    {
                        if (r.StatName == tar.StatName)
                        {
                            r.StatValue++; //Update attribute value
                            break;
                        }
                    }
                    break;
                }
        }

        public void MinusAttribute_Click(object sender)
        {
            // Find Sending Row
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow row)
                {
                    // Get sent attribute
                    StatRow tar = (StatRow)row.Item;
                    // Value cant go below zero
                    if (tar.StatValue > 0)
                    {
                        //Find target attribute in current character
                        foreach (StatRow r in this.AttributeRows)
                        {
                            if (r.StatName == tar.StatName)
                            {
                                r.StatValue--; //Update attribute value
                                break;
                            }
                        }
                    }
                    break;
                }
        }

        public void PlusSkill_Click(object sender)
        {
            // Find Sending Row
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow row)
                {
                    // Get sent attribute
                    StatRow tar = (StatRow)row.Item;
                    //Find target attribute in current character
                    foreach (StatRow r in this.SkillRows)
                    {
                        if (r.StatName == tar.StatName)
                        {
                            r.StatValue++; //Update attribute value
                            break;
                        }
                    }
                    break;
                }
        }

        public void MinusSkill_Click(object sender)
        {
            // Find Sending Row
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow row)
                {
                    // Get sent attribute
                    StatRow tar = (StatRow)row.Item;
                    // Value cant go below zero
                    if (tar.StatValue > 0)
                    {
                        //Find target attribute in current character
                        foreach (StatRow r in this.SkillRows)
                        {
                            if (r.StatName == tar.StatName)
                            {
                                r.StatValue--; //Update attribute value
                                break;
                            }
                        }
                    }
                    break;
                }
        }

        public void AddImage_Click()
        {
            this.UserCharacter.SetImage();
        }

    }
}
