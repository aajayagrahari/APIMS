using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_Partner;

namespace RSMApp_Partner
{
   public class CallDefectTypeDetailList:List<CallDefectTypeDetail>
    {
        public CallDefectTypeDetailList(string argCallCode, int argItemNo, string argClientCode)
        {
            LoadAllCallDefectDetail(argCallCode, argItemNo, argClientCode);
        }

        private void LoadAllCallDefectDetail(string argCallCode, int argItemNo, string argClientCode)
        {
            if (this.Count > 0)
                this.Clear();

            CallDefectTypeDetailManager objCallDefectTypeDetailManager = new CallDefectTypeDetailManager();

            objCallDefectTypeDetailManager.colGetCallDefectTypeDetail(argClientCode, argItemNo, argCallCode, this);
        }


        public CallDefectTypeDetail GetCallDefeactDetailByID(int argItemNo, string argCallCode)
        {
            foreach (CallDefectTypeDetail argCallDefectTypeDetail in this)
            {
                if (argCallDefectTypeDetail.ItemNo == argItemNo && argCallDefectTypeDetail.CallCode == argCallCode)
                {
                    return argCallDefectTypeDetail;
                }
            }
            return null;
        }

        public CallDefectTypeDetail GetCallDefeactDetailByDefectID(int argDfItemNo, int argItemNo, string argCallCode)
        {
            foreach (CallDefectTypeDetail argCallDefectTypeDetail in this)
            {
                if (argCallDefectTypeDetail.DfItemNo == argDfItemNo && argCallDefectTypeDetail.ItemNo == argItemNo && argCallDefectTypeDetail.CallCode.Trim() == argCallCode.Trim())
                {
                    return argCallDefectTypeDetail;
                }
            }
            return null;
        }

    }
}
