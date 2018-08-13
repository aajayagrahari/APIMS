using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Organization;

namespace RSMApp_Extended
{
    public class CRContUpdateTypeManagerExt : CRContUpdateTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBUpdateType, string argClientCode, int iIsDeleted)
        {
            RCBUpdateType.Items.Clear();
            foreach (DataRow dr in GetCRContUpdateType(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["CRUpdateCode"].ToString() + " " + dr["UpdateType"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["CRUpdateCode"].ToString()
                };

                itemCollection.Attributes.Add("CRUpdateCode", dr["CRUpdateCode"].ToString());
                itemCollection.Attributes.Add("UpdateType", dr["UpdateType"].ToString());

                RCBUpdateType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
