using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class DesignationMasterManagerExt : DesignationMasterManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBDesignation,string argClientCode, int iIsDeleted)
        {
            RCBDesignation.Items.Clear();
            foreach (DataRow dr in GetDesignationMaster(iIsDeleted,argClientCode).Tables[0].Rows)
            {
                String itemText = dr["DesignationCode"].ToString().Trim() + " " + dr["Designation"].ToString().Trim();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["DesignationCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("DesignationCode", dr["DesignationCode"].ToString().Trim());
                itemCollection.Attributes.Add("Designation", dr["Designation"].ToString().Trim());

                RCBDesignation.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }
    }
}
