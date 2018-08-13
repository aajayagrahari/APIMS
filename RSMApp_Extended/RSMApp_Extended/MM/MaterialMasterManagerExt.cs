using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_MM;

namespace RSMApp_Extended
{
    public class MaterialMasterManagerExt : MaterialMasterManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBMaterial, string argClientCode, int iIsDeleted)
        {
            RCBMaterial.Items.Clear();
            foreach (DataRow dr in GetMaterialMaster(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["MaterialCode"].ToString() + " " + dr["MatDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["MaterialCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("MaterialCode", dr["MaterialCode"].ToString().Trim());
                itemCollection.Attributes.Add("MatDesc", dr["MatDesc"].ToString());
                itemCollection.Attributes.Add("MaterialTypeCode", dr["MaterialTypeCode"].ToString());

                RCBMaterial.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public RadComboBoxData FillRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtMaterialData = GetMaterialMaster(context.Text.ToString(), context["MaterialTypeCode"].ToString(), context["MatGroup1Code"].ToString(), context["MatGroup2Code"].ToString(), context["SalesOrganizationCode"].ToString(), context["DistChannelCode"].ToString(), context["DivisionCode"].ToString(), strClientCode).Tables[0];

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
                        itemData.Value = dtMaterialData.Rows[i]["MaterialCode"].ToString().Trim();
                        itemData.Attributes["MatDesc"] = dtMaterialData.Rows[i]["MatDesc"].ToString();
                        itemData.Attributes["MaterialTypeCode"] = dtMaterialData.Rows[i]["MaterialTypeCode"].ToString().Trim();
                        itemData.Attributes["MatGroup1Code"] = dtMaterialData.Rows[i]["MatGroup1Code"].ToString().Trim();
                        itemData.Attributes["MatGroup2Code"] = dtMaterialData.Rows[i]["MatGroup2Code"].ToString().Trim();
                        itemData.Attributes["UOMCode"] = dtMaterialData.Rows[i]["UOMCode"].ToString();
                        itemData.Attributes["ValClassType"] = dtMaterialData.Rows[i]["ValClassType"].ToString();
                        itemData.Attributes["NetWeight"] = dtMaterialData.Rows[i]["NetWeight"].ToString();
                        itemData.Attributes["GrossWeight"] = dtMaterialData.Rows[i]["GrossWeight"].ToString();

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

        public RadComboBoxData FillMaterialCombo4BOM(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtMaterialData = GetMaterialMaster4BOM(context.Text.ToString(), context["MaterialTypeCode"].ToString(), context["MatGroup1Code"].ToString(), context["MatGroup2Code"].ToString(), strClientCode).Tables[0];

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
                        itemData.Value = dtMaterialData.Rows[i]["MaterialCode"].ToString().Trim();
                        itemData.Attributes["MatDesc"] = dtMaterialData.Rows[i]["MatDesc"].ToString();
                        itemData.Attributes["MaterialTypeCode"] = dtMaterialData.Rows[i]["MaterialTypeCode"].ToString().Trim();
                        itemData.Attributes["UOMCode"] = dtMaterialData.Rows[i]["UOMCode"].ToString();
                        itemData.Attributes["ValClassType"] = dtMaterialData.Rows[i]["ValClassType"].ToString();
                        itemData.Attributes["NetWeight"] = dtMaterialData.Rows[i]["NetWeight"].ToString();
                        itemData.Attributes["GrossWeight"] = dtMaterialData.Rows[i]["GrossWeight"].ToString();

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

        public RadComboBoxData FillMaterialCombo(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtMaterialData = GetMaterialMaster(context.Text.ToString(), context["MaterialTypeCode"].ToString(), context["MatGroup1Code"].ToString(), context["MatGroup2Code"].ToString(), strClientCode).Tables[0];

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
                        itemData.Value = dtMaterialData.Rows[i]["MaterialCode"].ToString().Trim();
                        itemData.Attributes["MatDesc"] = dtMaterialData.Rows[i]["MatDesc"].ToString();
                        itemData.Attributes["MaterialTypeCode"] = dtMaterialData.Rows[i]["MaterialTypeCode"].ToString().Trim();
                        itemData.Attributes["UOMCode"] = dtMaterialData.Rows[i]["UOMCode"].ToString();
                        itemData.Attributes["ValClassType"] = dtMaterialData.Rows[i]["ValClassType"].ToString();
                        itemData.Attributes["NetWeight"] = dtMaterialData.Rows[i]["NetWeight"].ToString();
                        itemData.Attributes["GrossWeight"] = dtMaterialData.Rows[i]["GrossWeight"].ToString();

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

        public RadComboBoxData FillMaterialCombo4Type(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtMaterialData = GetMaterialMaster(context.Text.ToString(), context["MaterialTypeCode"].ToString(), context["MatGroup1Code"].ToString(), strClientCode).Tables[0];

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
                        itemData.Value = dtMaterialData.Rows[i]["MaterialCode"].ToString().Trim();
                        itemData.Attributes["MatDesc"] = dtMaterialData.Rows[i]["MatDesc"].ToString();
                        itemData.Attributes["MaterialTypeCode"] = dtMaterialData.Rows[i]["MaterialTypeCode"].ToString().Trim();
                        itemData.Attributes["UOMCode"] = dtMaterialData.Rows[i]["UOMCode"].ToString();
                        itemData.Attributes["ValClassType"] = dtMaterialData.Rows[i]["ValClassType"].ToString();
                        itemData.Attributes["NetWeight"] = dtMaterialData.Rows[i]["NetWeight"].ToString();
                        itemData.Attributes["GrossWeight"] = dtMaterialData.Rows[i]["GrossWeight"].ToString();

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
        
    }
}
