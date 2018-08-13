using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_MM;

namespace RSMApp_Extended
{
    public class ItemCategory_MatMovementTypeManagerExt : ItemCategory_MatMovementTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBMatMovement, string argItemCategory, string argClientCode, int iIsDeleted)
        {
            RCBMatMovement.Items.Clear();
            foreach (DataRow dr in GetItemCategory_MatMovementType4Combo(argItemCategory, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["MatMovementCode"].ToString() + " " + dr["MatMovementDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["MatMovementCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("MatMovementCode", dr["MatMovementCode"].ToString().Trim());
                itemCollection.Attributes.Add("MatMovementDesc", dr["MatMovementDesc"].ToString());

                RCBMatMovement.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
