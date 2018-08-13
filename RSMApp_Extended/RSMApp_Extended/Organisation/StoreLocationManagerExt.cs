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
    public class StoreLocationManagerExt : StoreLocationManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBStoreLocation, string argClientCode, int iIsDeleted)
        {
            RCBStoreLocation.Items.Clear();
            foreach (DataRow dr in GetStoreLocation(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["StoreCode"].ToString() + " " + dr["StoreName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["StoreCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("StoreCode", dr["StoreCode"].ToString().Trim());
                itemCollection.Attributes.Add("StoreName", dr["StoreName"].ToString());

                RCBStoreLocation.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBStoreLocation, string argClientCode, string argPlantCode, int iIsDeleted)
        {
            RCBStoreLocation.Items.Clear();
            foreach (DataRow dr in GetStoreLocatio4Plant(argClientCode, argPlantCode).Tables[0].Rows)
            {
                String itemText = dr["StoreCode"].ToString() + " " + dr["StoreName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["StoreCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("StoreCode", dr["StoreCode"].ToString().Trim());
                itemCollection.Attributes.Add("StoreName", dr["StoreName"].ToString());

                RCBStoreLocation.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public RadComboBoxData FillRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtStoreData = GetStoreLocation(context.Text.ToString(), context["MaterialCode"].ToString(), context["PlantCode"].ToString(), strClientCode).Tables[0];

            if (dtStoreData != null)
            {
                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dtStoreData.Rows.Count)
                    {
                        endOffset = dtStoreData.Rows.Count;
                    }
                    if (endOffset == dtStoreData.Rows.Count)
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

                        String itemText = dtStoreData.Rows[i]["StoreCode"].ToString() + " " + dtStoreData.Rows[i]["StoreName"].ToString();

                        itemData.Text = itemText;
                        itemData.Value = dtStoreData.Rows[i]["StoreCode"].ToString().Trim();
                        itemData.Attributes["StoreName"] = dtStoreData.Rows[i]["StoreName"].ToString();
                        result.Add(itemData);
                    }

                    if (dtStoreData.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), dtStoreData.Rows.Count.ToString());
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

        public RadComboBoxData FillStoreCombo(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtStoreData = GetStore4Combo(context.Text.ToString(), strClientCode).Tables[0];

            if (dtStoreData != null)
            {
                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dtStoreData.Rows.Count)
                    {
                        endOffset = dtStoreData.Rows.Count;
                    }
                    if (endOffset == dtStoreData.Rows.Count)
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

                        String itemText = dtStoreData.Rows[i]["StoreCode"].ToString() + " " + dtStoreData.Rows[i]["StoreName"].ToString();

                        itemData.Text = itemText;
                        itemData.Value = dtStoreData.Rows[i]["StoreCode"].ToString();
                        itemData.Attributes["StoreName"] = dtStoreData.Rows[i]["StoreName"].ToString();
                        result.Add(itemData);
                    }

                    if (dtStoreData.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), dtStoreData.Rows.Count.ToString());
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
    }
}
