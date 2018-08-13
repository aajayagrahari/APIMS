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
    public class PlantMasterManagerExt : PlantMasterManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBPlant, string argClientCode, int iIsDeleted)
        {
            RCBPlant.Items.Clear();
            foreach (DataRow dr in GetPlantMaster(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["PlantCode"].ToString() + " " + dr["PlantName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["PlantCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("PlantCode", dr["PlantCode"].ToString().Trim());
                itemCollection.Attributes.Add("PlantName", dr["PlantName"].ToString().Trim());
                itemCollection.Attributes.Add("PlantDesc", dr["PlantDesc"].ToString().Trim());

                RCBPlant.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public void FillPlant4Shipping(Telerik.Web.UI.RadComboBox RCBPlant, string argShippingPointCode, string argClientCode)
        {
            RCBPlant.Items.Clear();
            foreach (DataRow dr in GetPlant4ShippingPoint(argShippingPointCode, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["PlantCode"].ToString() + " " + dr["PlantName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["PlantCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("PlantCode", dr["PlantCode"].ToString().Trim());
                itemCollection.Attributes.Add("PlantName", dr["PlantName"].ToString());

                RCBPlant.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBPlant, string argMaterialCode, string argClientCode)
        {
            RCBPlant.Items.Clear();
            foreach (DataRow dr in GetPlantMaster("", argMaterialCode, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["PlantCode"].ToString() + " " + dr["PlantName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["PlantCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("PlantCode", dr["PlantCode"].ToString().Trim());
                itemCollection.Attributes.Add("PlantName", dr["PlantName"].ToString());

                RCBPlant.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public void FillCombobox4State(Telerik.Web.UI.RadComboBox RCBPlantState, string argClientCode)
        {
            RCBPlantState.Items.Clear();
            foreach (DataRow dr in GetPlantState(argClientCode).Tables[0].Rows)
            {
                String itemText = dr["StateCode"].ToString() + " " + dr["StateName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["StateCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("StateCode", dr["StateCode"].ToString().Trim());
                itemCollection.Attributes.Add("StateName", dr["StateName"].ToString());

                RCBPlantState.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public RadComboBoxData FillRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtPlantData = GetPlantMaster(context.Text.ToString(), context["MaterialCode"].ToString(), strClientCode).Tables[0];

            if (dtPlantData != null)
            {
                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dtPlantData.Rows.Count)
                    {
                        endOffset = dtPlantData.Rows.Count;
                    }
                    if (endOffset == dtPlantData.Rows.Count)
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

                        String itemText = dtPlantData.Rows[i]["PlantCode"].ToString() + " " + dtPlantData.Rows[i]["PlantName"].ToString();

                        itemData.Text = itemText;
                        itemData.Value = dtPlantData.Rows[i]["PlantCode"].ToString().Trim();
                        itemData.Attributes["PlantName"] = dtPlantData.Rows[i]["PlantName"].ToString();
                        itemData.Attributes["PlantDesc"] = dtPlantData.Rows[i]["PlantDesc"].ToString();
                        result.Add(itemData);
                    }

                    if (dtPlantData.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), dtPlantData.Rows.Count.ToString());
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

        public RadComboBoxData FillPlantCombo(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtPlantData = GetPlant4Combo(context.Text.ToString(), strClientCode).Tables[0];

            if (dtPlantData != null)
            {
                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dtPlantData.Rows.Count)
                    {
                        endOffset = dtPlantData.Rows.Count;
                    }
                    if (endOffset == dtPlantData.Rows.Count)
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

                        String itemText = dtPlantData.Rows[i]["PlantCode"].ToString() + " " + dtPlantData.Rows[i]["PlantName"].ToString();

                        itemData.Text = itemText;
                        itemData.Value = dtPlantData.Rows[i]["PlantCode"].ToString().Trim();
                        itemData.Attributes["PlantName"] = dtPlantData.Rows[i]["PlantName"].ToString();
                        itemData.Attributes["PlantDesc"] = dtPlantData.Rows[i]["PlantDesc"].ToString();
                        result.Add(itemData);
                    }

                    if (dtPlantData.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), dtPlantData.Rows.Count.ToString());
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
