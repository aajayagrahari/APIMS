using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_MM;

namespace RSMApp_Extended
{
    public class ValuationTypeManagerExt : ValuationTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBValType, string argClientCode, int iIsDeleted)
        {
            RCBValType.Items.Clear();
            foreach (DataRow dr in GetValuationType(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ValTypeCode"].ToString().Trim() + " " + dr["ValTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ValTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ValTypeCode", dr["ValTypeCode"].ToString().Trim());
                itemCollection.Attributes.Add("ValTypeDesc", dr["ValTypeDesc"].ToString());


                RCBValType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }

        }
    }
}
