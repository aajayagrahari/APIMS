using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_FI;
using System.Data;
using Telerik.Web.UI;
using RSMApp_SD;

namespace RSMApp_Extended
{
    public class SalesOrderDetailManagerExt : SalesOrderDetailManager
    {
        public RadComboBoxData FillRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtMaterialData = GetSalesOrderDetail4Combo(context.Text.ToString(), context["SODocCode"].ToString().Trim(), strClientCode).Tables[0];

            if (dtMaterialData != null)
            {

                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dtMaterialData.Rows.Count)
                    {
                        endOffset = dtMaterialData.Rows.Count;
                    }
                    if (endOffset == dtMaterialData.Rows.Count)
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

                        String itemText = dtMaterialData.Rows[i]["MaterialCode"].ToString() + " " + dtMaterialData.Rows[i]["MatDesc"].ToString();

                        itemData.Text = itemText;
                        itemData.Value = dtMaterialData.Rows[i]["SOItemNo"].ToString().Trim();
                        itemData.Attributes["MatDesc"] = dtMaterialData.Rows[i]["MatDesc"].ToString();
                        itemData.Attributes["MaterialTypeCode"] = dtMaterialData.Rows[i]["MaterialTypeCode"].ToString().Trim();
                        itemData.Attributes["MatGroup1Code"] = dtMaterialData.Rows[i]["MatGroup1Code"].ToString().Trim();
                        itemData.Attributes["MatGroup2Code"] = dtMaterialData.Rows[i]["MatGroup2Code"].ToString().Trim();
                        itemData.Attributes["UOMCode"] = dtMaterialData.Rows[i]["UOMCode"].ToString();
                        itemData.Attributes["ValClassType"] = dtMaterialData.Rows[i]["ValClassType"].ToString();
                        itemData.Attributes["NetWeight"] = dtMaterialData.Rows[i]["NetWeight"].ToString();
                        itemData.Attributes["GrossWeight"] = dtMaterialData.Rows[i]["GrossWeight"].ToString();
                        itemData.Attributes["NetPriceperQty"] = dtMaterialData.Rows[i]["PriceUnit"].ToString();

                        result.Add(itemData);
                    }

                    if (dtMaterialData.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), dtMaterialData.Rows.Count.ToString());
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

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBSalesOrderDetailItemNo, string argClientCode, string argSODocCode)
        {
            RCBSalesOrderDetailItemNo.Items.Clear();
            foreach (DataRow dr in GetSalesOrderDetailsItemNo(argSODocCode, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["SOItemNo"].ToString().Trim();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["SOItemNo"].ToString().Trim()
                };

                itemCollection.Attributes.Add("SOItemNo", dr["SOItemNo"].ToString().Trim());
                RCBSalesOrderDetailItemNo.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
