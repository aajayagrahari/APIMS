using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class UOMMasterManagerExt :UOMMasterManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBUOM, int iIsDeleted, string argClientCode)
        {
            RCBUOM.Items.Clear();
            foreach (DataRow dr in GetUOMMaster(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["UOMCode"].ToString() + " " + dr["UOMDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["UOMCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("UOMCode", dr["UOMCode"].ToString().Trim());
                itemCollection.Attributes.Add("UOMDesc", dr["UOMDesc"].ToString());

                RCBUOM.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }
    }
}
