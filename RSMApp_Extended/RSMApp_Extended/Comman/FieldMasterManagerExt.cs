using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class FieldMasterManagerExt : FieldMasterManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBFiledName, int iIsDeleted)
        {
            RCBFiledName.Items.Clear();
            foreach (DataRow dr in GetFieldMaster(iIsDeleted).Tables[0].Rows)
            {
                String itemText = dr["FieldName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["FieldName"].ToString()
                };

                itemCollection.Attributes.Add("FieldName", dr["FieldName"].ToString());

                RCBFiledName.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }
    }
}
