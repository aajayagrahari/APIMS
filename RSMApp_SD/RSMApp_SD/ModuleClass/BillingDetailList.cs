using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSMApp_SD
{

  public class BillingDetailList : List<BillingDetail>  
   {
      public BillingDetailList(string argBillingDocCode, string argClientCode)
       {
           LoadAllBillingDetail(argBillingDocCode, argClientCode);
       }

      public void LoadAllBillingDetail(string argBillingDocCode, string argClientCode)
       {
           if (this.Count > 0)
               this.Clear();

           BillingDetailManager objBillingDetailManager = new BillingDetailManager();
           objBillingDetailManager.colGetBillingDetail(argBillingDocCode, argClientCode, this);

       }

      public void LoadAllBillingDetails4SO(string argSODocCode, string argClientCode)
      {
          //if (this.Count > 0)
          //    this.Clear();

          BillingDetailManager objBillingDetailManager = new BillingDetailManager();

          objBillingDetailManager.colGetBillingDetail4SO(argSODocCode, argClientCode, this);
      }

    

      public void LoadAllBillingDetails4DC(string argDeliveryDocCode, string argClientCode)
      {
          //if (this.Count > 0)
          //    this.Clear();

          BillingDetailManager objBillingDetailManager = new BillingDetailManager();

          objBillingDetailManager.colGetBillingDetail4DC(argDeliveryDocCode, argClientCode, this);
      }

       public BillingDetail GetBillingDetailByID(int argItemNo, string argBillingDocCode)
       {
           foreach (BillingDetail argBillingDetail in this)
           {
               if (argBillingDetail.ItemNo == argItemNo && argBillingDetail.BillingDocCode.Trim() == argBillingDocCode.Trim())
               {
                   return argBillingDetail;
               }
           }
           return null;
       }

   }
}
