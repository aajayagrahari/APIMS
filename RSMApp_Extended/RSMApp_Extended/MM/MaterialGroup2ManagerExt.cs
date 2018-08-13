using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_MM;

namespace RSMApp_Extended
{
    public class MaterialGroup2ManagerExt : MaterialGroup2Manager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBMaterialGroup2, string argClientCode, int iIsDeleted)
        {
            RCBMaterialGroup2.Items.Clear();
            foreach (DataRow dr in GetMaterialGroup2(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["MatGroup2Code"].ToString() + " " + dr["MatGroup2Desc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["MatGroup2Code"].ToString().Trim()
                };

                itemCollection.Attributes.Add("MatGroup2Code", dr["MatGroup2Code"].ToString().Trim());
                itemCollection.Attributes.Add("MatGroup2Desc", dr["MatGroup2Desc"].ToString());


                RCBMaterialGroup2.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public RadComboBoxData FillRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtMatGroup1Data = GetMaterialGroup24Combo(context.Text.ToString(), strClientCode).Tables[0];

            if (dtMatGroup1Data != null)
            {

                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dtMatGroup1Data.Rows.Count)
                    {
                        endOffset = dtMatGroup1Data.Rows.Count;
                    }
                    if (endOffset == dtMatGroup1Data.Rows.Count)
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

                        String itemText = dtMatGroup1Data.Rows[i]["MatGroup2Code"].ToString() + " " + dtMatGroup1Data.Rows[i]["MatGroup2Desc"].ToString();

                        itemData.Text = itemText;
                        itemData.Value = dtMatGroup1Data.Rows[i]["MatGroup2Code"].ToString().Trim();
                        itemData.Attributes["MatGroup2Code"] = dtMatGroup1Data.Rows[i]["MatGroup2Code"].ToString().Trim();
                        itemData.Attributes["MatGroup2Desc"] = dtMatGroup1Data.Rows[i]["MatGroup2Desc"].ToString();
                        result.Add(itemData);
                    }

                    if (dtMatGroup1Data.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), dtMatGroup1Data.Rows.Count.ToString());
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
