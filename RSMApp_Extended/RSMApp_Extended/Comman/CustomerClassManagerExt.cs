using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;


namespace RSMApp_Extended
{
    public class CustomerClassManagerExt : CustomerClassManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBCustomerClass, string argClientcode, int iIsDeleted)
        {
            RCBCustomerClass.Items.Clear();
            foreach (DataRow dr in GetCustomerClass(iIsDeleted, argClientcode).Tables[0].Rows)
            {
                String itemText = dr["CustomerClassCode"].ToString() + " " + dr["CustomerClassDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["CustomerClassCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("CustomerClassCode", dr["CustomerClassCode"].ToString());
                itemCollection.Attributes.Add("CustomerClassDesc", dr["CustomerClassDesc"].ToString());

                RCBCustomerClass.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
