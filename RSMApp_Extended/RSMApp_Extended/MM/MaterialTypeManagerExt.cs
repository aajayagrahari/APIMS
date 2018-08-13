using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_MM;

namespace RSMApp_Extended
{
    public class MaterialTypeManagerExt : MaterialTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBMaterialType, string argClientCode, int iIsDeleted)
        {
            RCBMaterialType.Items.Clear();
            foreach (DataRow dr in GetMaterialType(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["MaterialTypeCode"].ToString() + " " + dr["MaterialTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["MaterialTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("MaterialTypeCode", dr["MaterialTypeCode"].ToString().Trim());
                itemCollection.Attributes.Add("MaterialTypeDesc", dr["MaterialTypeDesc"].ToString());
                itemCollection.Attributes.Add("NumRange", dr["NumRange"].ToString());

                RCBMaterialType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public RadComboBoxData FillRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtMatTypeData = GetMaterialType4Combo(context.Text.ToString().Trim(), strClientCode).Tables[0];

            if (dtMatTypeData != null)
            {

                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dtMatTypeData.Rows.Count)
                    {
                        endOffset = dtMatTypeData.Rows.Count;
                    }
                    if (endOffset == dtMatTypeData.Rows.Count)
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

                        String itemText = dtMatTypeData.Rows[i]["MaterialTypeCode"].ToString().Trim() + " " + dtMatTypeData.Rows[i]["MaterialTypeDesc"].ToString().Trim();

                        itemData.Text = itemText;
                        itemData.Value = dtMatTypeData.Rows[i]["MaterialTypeCode"].ToString().Trim();
                        itemData.Attributes["MaterialTypeCode"] = dtMatTypeData.Rows[i]["MaterialTypeCode"].ToString().Trim();
                        itemData.Attributes["MaterialTypeDesc"] = dtMatTypeData.Rows[i]["MaterialTypeDesc"].ToString();
                        itemData.Attributes["NumRange"] = dtMatTypeData.Rows[i]["NumRange"].ToString();
                        result.Add(itemData);
                    }

                    if (dtMatTypeData.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), dtMatTypeData.Rows.Count.ToString());
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
