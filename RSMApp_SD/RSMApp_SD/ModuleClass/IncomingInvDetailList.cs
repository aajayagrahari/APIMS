using System;
using System.Collections.Generic;
using System.Text;

namespace RSMApp_SD
{
   public class IncomingInvDetailList : List<IncomingInvDetail>  
   {
       public IncomingInvDetailList(string argDeliveryDocCode, string argClientCode)
       {
           LoadAllIncomingInvDetail(argDeliveryDocCode, argClientCode);
       }

       private void LoadAllIncomingInvDetail(string argIncomingInvDocCode, string argClientCode)
       {
           if (this.Count > 0)
               this.Clear();

           IncomingInvDetailManager objIncomingInvDetailManager = new IncomingInvDetailManager();
           objIncomingInvDetailManager.colGetIncomingInvDetail(argIncomingInvDocCode, argClientCode, this);
       }

       public void LoadAllIncomingInvDetails4PO(string argPODocCode, string argClientCode)
       {
           IncomingInvDetailManager objIncomingInvDetailManager = new IncomingInvDetailManager();

           objIncomingInvDetailManager.colGetIncomingInvDetail4PO(argPODocCode, argClientCode, this);
       }

       public void LoadAllBillingDetails4InBDC(string argInBDeliveryDocCode, string argClientCode)
       {
           IncomingInvDetailManager objIncomingInvDetailManager = new IncomingInvDetailManager();

           objIncomingInvDetailManager.colGetIncomingInvDetail4InBDC(argInBDeliveryDocCode, argClientCode, this);
       }

       public IncomingInvDetail GetIncomingInvDetailByID(int argItemNo, string argIncomingInvDocCode)
       {
           foreach (IncomingInvDetail argIncomingInvDetail in this)
           {
               if (argIncomingInvDetail.ItemNo == argItemNo && argIncomingInvDetail.IncomingInvDocCode.Trim() == argIncomingInvDocCode.Trim())
               {
                   return argIncomingInvDetail;
               }
           }
           return null;
       }

   }
}
