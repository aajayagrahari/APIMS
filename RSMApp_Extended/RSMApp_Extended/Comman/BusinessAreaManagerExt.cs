using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class BusinessAreaManagerExt : BusinessAreaManager
    {

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBBusinessArea, int iIsDeleted, string argClientCode)
        {
            RCBBusinessArea.Items.Clear();
            foreach (DataRow dr in GetBusinessArea(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["BusinessAreaCode"].ToString() + " " + dr["BusinessAreaName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["BusinessAreaCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("BusinessAreaCode", dr["BusinessAreaCode"].ToString());
                itemCollection.Attributes.Add("BusinessAreaName", dr["BusinessAreaName"].ToString());

                RCBBusinessArea.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
