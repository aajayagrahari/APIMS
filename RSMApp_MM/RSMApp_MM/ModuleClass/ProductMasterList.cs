using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_MM;

namespace RSMApp_MM
{
    public class ProductMasterList :List<ProductMaster>
    {
        public ProductMasterList(string argSerialNo,string argMastMaterialCode, string argMatGroup1Code, string argClientCode)
       {
           LoadAllProductDetail(argSerialNo, argMastMaterialCode, argMatGroup1Code, argClientCode);
       }

        private void LoadAllProductDetail(string argSerialNo,string argMastMaterialCode, string argMatGroup1Code, string argClientCode)
       {
           if (this.Count > 0)
               this.Clear();

           ProductMasterManager objProductMasterManager = new ProductMasterManager();

           objProductMasterManager.colGetProductMaster(argSerialNo, argMastMaterialCode, argMatGroup1Code, argClientCode, this);
       }

        public ProductMaster GetProductDetailByID(string argSerialNo, string argMastMaterialCode, string argMaterialCode, string argMatGroup1Code, string argClientCode)
       {
           foreach (ProductMaster argProductMaster in this)
           {
               if (argProductMaster.SerialNo == argSerialNo &&  argProductMaster.MastMaterialCode == argMastMaterialCode && argProductMaster.MaterialCode == argMaterialCode && argProductMaster.MatGroup1Code == argMatGroup1Code && argProductMaster.ClientCode == argClientCode)
               {
                   return argProductMaster;
               }
           }
           return null;
       }

        public ProductMaster GetProductDetails(string argSerialNo, string argMastMaterialCode, string argMaterialCode, string argMatGroup1Code,string argClientCode)
       {
           ProductMaster objProductMaster = new ProductMaster();
           ProductMasterManager objProductMasterManager = new ProductMasterManager();

           objProductMaster = objProductMasterManager.objGetProductMaster(argSerialNo, argMastMaterialCode, argMatGroup1Code, argClientCode);

           return objProductMaster;
           
       }

    }
}
