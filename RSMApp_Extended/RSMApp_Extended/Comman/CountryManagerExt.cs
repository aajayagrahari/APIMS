using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class CountryManagerExt : CountryManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBCountry, int iIsDeleted)
        {
            RCBCountry.Items.Clear();
            foreach (DataRow dr in GetCountry(iIsDeleted).Tables[0].Rows)
            {
                String itemText = dr["CountryCode"].ToString() + " " + dr["CountryName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["CountryCode"].ToString()
                };

                itemCollection.Attributes.Add("CountryCode", dr["CountryCode"].ToString());
                itemCollection.Attributes.Add("CountryName", dr["CountryName"].ToString());

                RCBCountry.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }
    }
}
