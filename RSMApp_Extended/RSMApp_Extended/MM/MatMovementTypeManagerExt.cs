using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_MM;

namespace RSMApp_Extended
{
    public class MatMovementTypeManagerExt : MatMovementTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBMatMovementType, string argClientCode, int iIsDeleted)
        {
            RCBMatMovementType.Items.Clear();
            foreach (DataRow dr in GetMatMovementType(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["MatMovementCode"].ToString() + " " + dr["MatMovementDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["MatMovementCode"].ToString()
                };

                itemCollection.Attributes.Add("MatMovementCode", dr["MatMovementCode"].ToString());
                itemCollection.Attributes.Add("MatMovementDesc", dr["MatMovementDesc"].ToString());

                RCBMatMovementType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

    }
}
