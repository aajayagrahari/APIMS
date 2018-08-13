using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_Partner;
using Telerik.Web.UI;
using System.Data;
using System.Data.SqlClient;

namespace RSMApp_Extended
{
    public class DefectTypeMasterManagerExt : DefectTypeMasterManager
    {

        public void FillCombobox(RadComboBox RCBDefectType, string argClientCode, int iIsDeleted)
        {
            RCBDefectType.Items.Clear();
            foreach (DataRow dr in GetDefectTypeMaster(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["DefectTypeCode"].ToString() + " " + dr["DefectTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["DefectTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("DefectTypeCode", dr["DefectTypeCode"].ToString());
                itemCollection.Attributes.Add("DefectTypeDesc", dr["DefectTypeDesc"].ToString());

                RCBDefectType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
     

    }
}
