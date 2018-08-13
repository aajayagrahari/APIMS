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
    public class SalesRegionManagerExt : SalesRegionManager
    {
        public RadComboBoxData FillRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtSalesRegionData = GetSalesRegion4Combo(context.Text.ToString(), strClientCode).Tables[0];

            if (dtSalesRegionData != null)
            {

                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dtSalesRegionData.Rows.Count)
                    {
                        endOffset = dtSalesRegionData.Rows.Count;
                    }
                    if (endOffset == dtSalesRegionData.Rows.Count)
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

                        String itemText = dtSalesRegionData.Rows[i]["SalesRegionCode"].ToString().Trim() + " " + dtSalesRegionData.Rows[i]["SalesRegionName"].ToString();
                        itemData.Text = itemText;
                        itemData.Value = dtSalesRegionData.Rows[i]["SalesRegionCode"].ToString().Trim();
                        itemData.Attributes["SalesRegionCode"] = dtSalesRegionData.Rows[i]["SalesRegionCode"].ToString().Trim();
                        itemData.Attributes["SalesRegionName"] = dtSalesRegionData.Rows[i]["SalesRegionName"].ToString().Trim();

                        result.Add(itemData);
                    }

                    if (dtSalesRegionData.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), dtSalesRegionData.Rows.Count.ToString());
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

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBSalesRegion, string argClientCode, int iIsDeleted)
        {
            RCBSalesRegion.Items.Clear();
            foreach (DataRow dr in GetSalesRegion(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["SalesRegionCode"].ToString().Trim() + " " + dr["SalesRegionName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["SalesRegionCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("SalesRegionCode", dr["SalesRegionCode"].ToString().Trim());
                itemCollection.Attributes.Add("SalesRegionName", dr["SalesRegionName"].ToString());

                RCBSalesRegion.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
