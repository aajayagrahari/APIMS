using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_Partner;
using Telerik.Web.UI;
using System.Data;
using System.Data.SqlClient;
using RSMApp_SAP;

namespace RSMApp_Extended
{
    public class DBTableStructureManagerExt : DBTableStructureManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBTableStructure)
        {
            RCBTableStructure.Items.Clear();
            foreach (DataRow dr in GetDBTableStructure().Tables[0].Rows)
            {
                String itemText = dr["TableName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["TableName"].ToString()
                };

                itemCollection.Attributes.Add("TableName", dr["TableName"].ToString());
                RCBTableStructure.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public void FillComboboxFields(Telerik.Web.UI.RadComboBox RCBTableField, string argTableName)
        {
            RCBTableField.Items.Clear();
            foreach (DataColumn dc in GetDBTableFields(argTableName).Tables[0].Columns)
            {
                string itemText = dc.ColumnName.ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dc.ColumnName.ToString()
                };

                RCBTableField.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
