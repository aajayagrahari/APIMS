using System;
using System.Collections.Generic;
using System.Text;

namespace RSMApp_MM
{
    public class BOMList : List<BillsofMaterial>
    {
        public BOMList(string argMastMaterialCode, string argMRevisionCode, string argClientCode)
       {
           LoadAllBOMDetail(argMastMaterialCode, argMRevisionCode,  argClientCode);
       }

        private void LoadAllBOMDetail(string argMastMaterialCode, string argMRevisionCode, string argClientCode)
       {
           if (this.Count > 0)
               this.Clear();

           BillsofMaterialManager objBOMManager = new BillsofMaterialManager();

           objBOMManager.colGetBillsofMaterial(argMastMaterialCode, argMRevisionCode, argClientCode, this);
       }

       public BillsofMaterial GetBOMDetailByID(string argMastMaterialCode, string argMaterialCode, string argMRevisionCode, string argClientCode)
       {
           foreach (BillsofMaterial argBOM in this)
           {
               if (argBOM.MastMaterialCode == argMastMaterialCode &&  argBOM.MaterialCode == argMaterialCode && argBOM.MRevisionCode== argMRevisionCode && argBOM.ClientCode == argClientCode)
               {
                   return argBOM;
               }
           }
           return null;
       }

       public BillsofMaterial GetBOMDetails(string argMastMaterialCode, string argMRevisionCode, string argClientCode)
       {
           BillsofMaterial objBillofMaterial = new BillsofMaterial();
           BillsofMaterialManager objBOMManager = new BillsofMaterialManager();

           objBillofMaterial = objBOMManager.objGetBillsofMaterial(argMastMaterialCode, argMRevisionCode, argClientCode);
           return objBillofMaterial;
       }
    }
}
