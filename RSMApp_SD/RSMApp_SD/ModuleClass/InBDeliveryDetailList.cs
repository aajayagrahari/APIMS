using System;
using System.Collections.Generic;
using System.Text;

namespace RSMApp_SD
{
   public class InBDeliveryDetailList : List<InBDeliveryDetail>  
   {
       public InBDeliveryDetailList(string argInBDeliveryDocCode, string argClientCode)
       {
           LoadAllInBDeliveryDetail(argInBDeliveryDocCode, argClientCode);
       }

       private void LoadAllInBDeliveryDetail(string argInBDeliveryDocCode, string argClientCode)
       {
           if (this.Count > 0)
               this.Clear();

           InBDeliveryDetailManager objInBDeliveryDetailManager = new InBDeliveryDetailManager();

           objInBDeliveryDetailManager.colGetInBDeliveryDetail(argInBDeliveryDocCode, argClientCode, this);
       }

       public void LoadAllPODetail4InBDC(string argInBDeliveryDocTypeCode, string argPODocCode, string argClientCode, int argItemNoFrom, int argItemNoTo)
       {
           //if (this.Count > 0)
           //    this.Clear();

           InBDeliveryDetailManager objInBDeliveryDetailManager = new InBDeliveryDetailManager();

           objInBDeliveryDetailManager.colGetPODetail4InBDC(argInBDeliveryDocTypeCode,argPODocCode, argClientCode, argItemNoFrom, argItemNoTo, this);
       }


       public InBDeliveryDetail GetInBDeliveryDetailByID(string argItemNo, string argInBDeliveryDocCode)
       {
           foreach (InBDeliveryDetail argInBDeliveryDetail in this)
           {
               if (argInBDeliveryDetail.ItemNo.Trim() == argItemNo.Trim() && argInBDeliveryDetail.InBDeliveryDocCode.Trim() == argInBDeliveryDocCode.Trim())
               {
                   return argInBDeliveryDetail;
               }
           }
           return null;
       }

   }
}
