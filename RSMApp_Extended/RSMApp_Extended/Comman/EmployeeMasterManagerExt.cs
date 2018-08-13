using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class EmployeeMasterManagerExt : EmployeeMasterManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBEmployee,string argClientCode, int iIsDeleted)
        {
            RCBEmployee.Items.Clear();
            foreach (DataRow dr in GetEmployeeMaster(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["EmployeeCode"].ToString() + " " + dr["EmployeeName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["EmployeeCode"].ToString()
                };

                itemCollection.Attributes.Add("EmployeeCode", dr["EmployeeCode"].ToString());
                itemCollection.Attributes.Add("EmployeeName", dr["EmployeeName"].ToString());

                RCBEmployee.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }
    }
}
