using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class ModulePageManagerExt : ModulePageManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBModule, string argTransType, string argClientCode)
        {
            RCBModule.Items.Clear();
            foreach (DataRow dr in GetModule(argTransType, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["Module"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["Module"].ToString().Trim()
                };

                itemCollection.Attributes.Add("Module", dr["Module"].ToString());
                RCBModule.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public void FillModuleType(Telerik.Web.UI.RadComboBox RCBModuleType, string argClientCode)
        {
            RCBModuleType.Items.Clear();
            foreach (DataRow dr in GetModuleType(argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ModuleType"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ModuleType"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ModuleType", dr["ModuleType"].ToString());
                RCBModuleType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public void FillParentCombobox(Telerik.Web.UI.RadComboBox RCBParentModule, string argClientCode)
        {
            RCBParentModule.Items.Clear();
            foreach (DataRow dr in GetModulePage(argClientCode).Tables[0].Rows)
            {
                String itemText = dr["Module"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["idModulePage"].ToString().Trim()
                };

                itemCollection.Attributes.Add("Module", dr["Module"].ToString());
                itemCollection.Attributes.Add("ModuleNL", dr["ModuleNL"].ToString());
                itemCollection.Attributes.Add("idModulePage", dr["idModulePage"].ToString());
                itemCollection.Attributes.Add("ParentModule", dr["ParentModule"].ToString());
                RCBParentModule.Items.Add(itemCollection);
                itemCollection.DataBind();
            }

        }
    }
}
