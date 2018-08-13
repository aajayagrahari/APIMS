using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class DepartmentMasterManagerExt : DepartmentMasterManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBDesignation,string argClientCode, int iIsDeleted)
        {
            RCBDesignation.Items.Clear();
            foreach (DataRow dr in GetDepartmentMaster(iIsDeleted,argClientCode).Tables[0].Rows)
            {
                String itemText = dr["DepartmentCode"].ToString().Trim() + " " + dr["Department"].ToString().Trim();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["DepartmentCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("DepartmentCode", dr["DepartmentCode"].ToString().Trim());
                itemCollection.Attributes.Add("Department", dr["Department"].ToString().Trim());

                RCBDesignation.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }
    }
}
