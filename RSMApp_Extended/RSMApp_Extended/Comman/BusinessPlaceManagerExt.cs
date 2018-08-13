using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class BusinessPlaceManagerExt : BusinessPlaceManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBBusinessPlace,string argClientCode, int iIsDeleted)
        {
            RCBBusinessPlace.Items.Clear();
            foreach (DataRow dr in GetBusinessPlace(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["BusinessPlaceCode"].ToString() + " " + dr["BusinessPlaceDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["BusinessPlaceCode"].ToString()
                };

                itemCollection.Attributes.Add("BusinessPlaceCode", dr["BusinessPlaceCode"].ToString());
                itemCollection.Attributes.Add("BusinessPlaceDesc", dr["BusinessPlaceDesc"].ToString());

                RCBBusinessPlace.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }
    }
}
