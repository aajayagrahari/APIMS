using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_MM;

namespace RSMApp_Extended
{
    public class ClassMasterManagerExt : ClassMasterManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBClass, string argClientCode, int iIsDeleted)
        {
            RCBClass.Items.Clear();
            foreach (DataRow dr in GetClassMaster(argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ClassType"].ToString() + " " + dr["ClassName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["idClass"].ToString().Trim()
                };

                itemCollection.Attributes.Add("idClass", dr["idClass"].ToString().Trim());
                itemCollection.Attributes.Add("ClassType", dr["ClassType"].ToString());
                itemCollection.Attributes.Add("ClassName", dr["ClassName"].ToString());

                RCBClass.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
