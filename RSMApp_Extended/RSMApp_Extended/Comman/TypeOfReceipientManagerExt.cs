using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class TypeOfReceipientManagerExt : TypeOfReceipientManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBTypeOfRecpt, string argClientCode, int iIsDeleted)
        {
            RCBTypeOfRecpt.Items.Clear();
            foreach (DataRow dr in GetTypeOfReceipient(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["TypeOfRecCode"].ToString() + " " + dr["TypeOfRecDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["TypeOfRecCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("TypeOfRecCode", dr["TypeOfRecCode"].ToString());
                itemCollection.Attributes.Add("TypeOfRecDesc", dr["TypeOfRecDesc"].ToString());

                RCBTypeOfRecpt.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
