using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_FI;
using System.Data;
using Telerik.Web.UI;
using RSMApp_SD;
using RSMApp_Organization;

namespace RSMApp_Extended
{
    public class SalesGroupManagerExt : SalesGroupManager
    {
        public RadComboBoxData FillRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtSalesGroupData = GetSalesGroup4Combo(context.Text.ToString(), strClientCode).Tables[0];

            if (dtSalesGroupData != null)
            {

                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dtSalesGroupData.Rows.Count)
                    {
                        endOffset = dtSalesGroupData.Rows.Count;
                    }
                    if (endOffset == dtSalesGroupData.Rows.Count)
                    {
                        comboData.EndOfItems = true;
                    }
                    else
                    {
                        comboData.EndOfItems = false;
                    }
                    result = new List<RadComboBoxItemData>();

                    for (int i = itemOffset; i < endOffset; i++)
                    {
                        RadComboBoxItemData itemData = new RadComboBoxItemData();

                        String itemText = dtSalesGroupData.Rows[i]["SalesGroupCode"].ToString().Trim() + " " + dtSalesGroupData.Rows[i]["SalesGroupName"].ToString();
                        itemData.Text = itemText;
                        itemData.Value = dtSalesGroupData.Rows[i]["SalesGroupCode"].ToString().Trim();
                        itemData.Attributes["SalesGroupCode"] = dtSalesGroupData.Rows[i]["SalesGroupCode"].ToString().Trim();
                        itemData.Attributes["SalesGroupName"] = dtSalesGroupData.Rows[i]["SalesGroupName"].ToString().Trim();

                        result.Add(itemData);
                    }

                    if (dtSalesGroupData.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), dtSalesGroupData.Rows.Count.ToString());
                    }
                    else
                    {
                        comboData.Message = "No matches";
                    }

                }
                catch (Exception e)
                {
                    comboData.Message = e.Message;
                }

                comboData.Items = result.ToArray();
            }
            return comboData;
        }

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBSalesGroup, string argClientCode, int iIsDeleted)
        {
            RCBSalesGroup.Items.Clear();
            foreach (DataRow dr in GetSalesGroup(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["SalesGroupCode"].ToString().Trim() + " " + dr["SalesGroupName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["SalesGroupCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("SalesGroupCode", dr["SalesGroupCode"].ToString().Trim());
                itemCollection.Attributes.Add("SalesGroupName", dr["SalesGroupName"].ToString().Trim());

                RCBSalesGroup.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
