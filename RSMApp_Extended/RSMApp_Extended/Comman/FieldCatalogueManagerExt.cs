using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class FieldCatalogueManagerExt : FieldCatalogueManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBFieldName, string argClientCode, int iIsDeleted)
        {
            RCBFieldName.Items.Clear();
            foreach (DataRow dr in GetFieldCatalogue(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["FieldName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["FieldName"].ToString().Trim()
                };

                itemCollection.Attributes.Add("FieldName", dr["FieldName"].ToString());


                RCBFieldName.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
