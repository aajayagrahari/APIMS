using System;
using System.Collections.Generic;
using System.Text;

namespace RSMApp_SD
{
   public class SODetailList : List<SalesOrderDetail>
   {

       public SODetailList(string argSODocCode, string argClientCode)
       {
           LoadAllSalesOrderDetail(argSODocCode, argClientCode);
       }

       private void LoadAllSalesOrderDetail(string argSODocCode, string argClientCode)
       {
           if (this.Count > 0)
               this.Clear();

           SalesOrderDetailManager objSODetailManager = new SalesOrderDetailManager();

           objSODetailManager.colGetSalesOrderDetail(argSODocCode, argClientCode, this);
       }

       public SalesOrderDetail GetSalesOrderDetailByID(string argItemNo, string argSODocCode)
       {
           foreach (SalesOrderDetail argSalesOrderDetail in this)
           {
               if (argSalesOrderDetail.ItemNo == argItemNo && argSalesOrderDetail.SODocCode == argSODocCode)
               {
                   return argSalesOrderDetail;
               }
           }
           return null;
       }

       public SalesOrderDetail GetSalesOrderDetailByItemNo(string argItemNo)
       {
           foreach (SalesOrderDetail argSalesOrderDetail in this)
           {
               if (argSalesOrderDetail.ItemNo == argItemNo)
               {
                   return argSalesOrderDetail;
               }
           }
           return null;
       }

   }


}
