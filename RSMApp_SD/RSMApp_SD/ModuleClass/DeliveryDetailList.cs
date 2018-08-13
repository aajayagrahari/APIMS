using System;
using System.Collections.Generic;
using System.Text;

namespace RSMApp_SD
{
   public class DeliveryDetailList : List<DeliveryDetail>  
   {
       public DeliveryDetailList(string argDeliveryDocCode, string argClientCode)
       {
           LoadAllDeliveryDetail(argDeliveryDocCode, argClientCode);
       }

       private void LoadAllDeliveryDetail(string argDeliveryDocCode, string argClientCode)
       {
           if (this.Count > 0)
               this.Clear();

           DeliveryDetailManager objDeliveryDetailManager = new DeliveryDetailManager();

           objDeliveryDetailManager.colGetDeliveryDetail(argDeliveryDocCode, argClientCode, this);
       }

       public void LoadAllSODetail4DC(string argDeliveryDocTypeCode, string argSODocCode, string argClientCode, int argItemNoFrom, int argItemNoTo)
       {
           //if (this.Count > 0)
           //    this.Clear();

           DeliveryDetailManager objDeliveryDetailManager = new DeliveryDetailManager();

           objDeliveryDetailManager.colGetSODetail4DC(argDeliveryDocTypeCode,argSODocCode, argClientCode, argItemNoFrom, argItemNoTo, this);
       }


       public DeliveryDetail GetDeliveryDetailByID(string argItemNo, string argDeliveryDocCode)
       {
           foreach (DeliveryDetail argDeliveryDetail in this)
           {
               if (argDeliveryDetail.ItemNo.Trim() == argItemNo.Trim() && argDeliveryDetail.DeliveryDocCode.Trim() == argDeliveryDocCode.Trim())
               {
                   return argDeliveryDetail;
               }
           }
           return null;
       }

   }
}
