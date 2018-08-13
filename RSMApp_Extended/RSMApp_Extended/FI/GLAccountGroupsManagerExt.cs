using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_FI;

namespace RSMApp_Extended
{
   public class GLAccountGroupsManagerExt :GLAccountGroupsManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBGLAccountGroup, string argChartACCode, string argClientCode, int iIsDeleted)
        {
            RCBGLAccountGroup.Items.Clear();
            foreach (DataRow dr in GetGLAccountGroups(argChartACCode, "", iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ActGroup"].ToString() + " " + dr["GroupName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ActGroup"].ToString()
                };

                itemCollection.Attributes.Add("ActGroup", dr["ActGroup"].ToString());
                itemCollection.Attributes.Add("GroupName", dr["GroupName"].ToString());

                RCBGLAccountGroup.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

    }
}
