using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_FI;
using System.Data;
using Telerik.Web.UI;

namespace RSMApp_Extended
{
   public class AccAssignGrpMaterialManagerExt : AccAssignGrpMaterialManager
    {

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBAccAssGrpMaterial, string argClientCode, int iIsDeleted)
        {
            RCBAccAssGrpMaterial.Items.Clear();
            foreach (DataRow dr in GetAccAssignGrpMaterial(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["AccAssignGroupCode"].ToString() + " " + dr["AccAssignGroupDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["AccAssignGroupCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("AccAssignGroupCode", dr["AccAssignGroupCode"].ToString());
                itemCollection.Attributes.Add("AccAssignGroupDesc", dr["AccAssignGroupDesc"].ToString());

                RCBAccAssGrpMaterial.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

    }
}
