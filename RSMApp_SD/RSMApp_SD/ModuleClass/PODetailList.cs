using System;
using System.Collections.Generic;
using System.Text;

namespace RSMApp_SD
{
   public class PODetailList : List<PurchaseOrderDetail>
   {

       public PODetailList(string argPODocCode, string argClientCode)
       {
           LoadAllPurchaseOrderDetail(argPODocCode, argClientCode);
       }

       private void LoadAllPurchaseOrderDetail(string argPODocCode, string argClientCode)
       {
           if (this.Count > 0)
               this.Clear();

           PurchaseOrderDetailManager objPODetailManager = new PurchaseOrderDetailManager();

           objPODetailManager.colGetPurchaseOrderDetail(argPODocCode, argClientCode, this);
       }

       public PurchaseOrderDetail GetPurchaseOrderDetailByID(string argItemNo, string argPODocCode)
       {
           foreach (PurchaseOrderDetail argPurchaseOrderDetail in this)
           {
               if (argPurchaseOrderDetail.ItemNo.Trim() == argItemNo && argPurchaseOrderDetail.PODocCode.Trim() == argPODocCode)
               {
                   return argPurchaseOrderDetail;
               }
           }
           return null;
       }

       public PurchaseOrderDetail GetPurchaseOrderDetailByItemNo(string argItemNo)
       {
           foreach (PurchaseOrderDetail argPurchaseOrderDetail in this)
           {
               if (argPurchaseOrderDetail.ItemNo == argItemNo)
               {
                   return argPurchaseOrderDetail;
               }
           }
           return null;
       }

   }


}
