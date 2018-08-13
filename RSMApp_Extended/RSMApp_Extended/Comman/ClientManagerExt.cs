using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class ClientManagerExt : ClientManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBClient, int iIsDeleted)
        {
            RCBClient.Items.Clear();
            foreach (DataRow dr in GetClient(iIsDeleted).Tables[0].Rows)
            {
                String itemText = dr["ClientCode"].ToString() + " " + dr["ClientName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ClientCode"].ToString()
                };

                itemCollection.Attributes.Add("ClientCode", dr["ClientCode"].ToString());
                itemCollection.Attributes.Add("ClientName", dr["ClientName"].ToString());

                RCBClient.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }
    }
}
