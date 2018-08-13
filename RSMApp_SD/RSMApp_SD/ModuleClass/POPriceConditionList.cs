using System;
using System.Collections.Generic;
using System.Text;

namespace RSMApp_SD
{
    public class POPriceConditionList : List<POPriceCondition>
    {
        
        public POPriceConditionList(string argPODocCode, string argClientCode)
        {
            LoadAllPOPriceCondition(argPODocCode, argClientCode);
        }

        private void LoadAllPOPriceCondition(string argPODocCode, string argClientCode)
        {
            if (this.Count > 0)
                this.Clear();

            POPriceConditionManager objPOPriceConditionManager = new POPriceConditionManager();

            objPOPriceConditionManager.colGetPOPriceCondition(argPODocCode, argClientCode, this);
        }

        public POPriceCondition GetPOPriceConditionByID(string argItemNo, string argPODocCode, string argConditionType)
        {
            foreach (POPriceCondition argPOPriceCondition in this)
            {
                if (argPOPriceCondition.ItemNo == argItemNo && argPOPriceCondition.PODocCode == argPODocCode && argPOPriceCondition.ConditionType == argConditionType)
                {
                    return argPOPriceCondition;
                }
            }
            return null;
        }   
    }
}
