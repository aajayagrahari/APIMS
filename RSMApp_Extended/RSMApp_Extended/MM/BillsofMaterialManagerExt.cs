using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_MM;

namespace RSMApp_Extended
{
    public class BillsofMaterialManagerExt : BillsofMaterialManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBRevision, string argMaterialCode, string argClientCode)
        {
            RCBRevision.Items.Clear();
            foreach (DataRow dr in GetRevisionCode(argMaterialCode, argClientCode).Tables[0].Rows)
                {
                    String itemText = dr["MaterialCode"].ToString();
                    var itemCollection = new RadComboBoxItem
                    {
                        Text = itemText,
                        Value = dr["MRevisionCode"].ToString().Trim()
                    };

                    itemCollection.Attributes.Add("MRevisionCode", dr["MRevisionCode"].ToString().Trim());
                    itemCollection.Attributes.Add("tBOMNode", dr["tBOMNode"].ToString());
                    itemCollection.Attributes.Add("ValidFrom", dr["ValidFrom"].ToString());
                    itemCollection.Attributes.Add("ValidTo", dr["ValidTo"].ToString());
                    itemCollection.Attributes.Add("IsSerialBatch", dr["IsSerialBatch"].ToString());

                    RCBRevision.Items.Add(itemCollection);
                    itemCollection.DataBind();
                }


            
        }

       public RadComboBoxData FillMaterialCombo4Product(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtMaterialData = GetBOM4Product(context.Text.ToString(), context["MatGroup1Code"].ToString(), strClientCode).Tables[0];

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
                        itemData.Attributes["IsBOMExplodeApp"] = dtMaterialData.Rows[i]["IsBOMExplodeApp"].ToString();
                       

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
