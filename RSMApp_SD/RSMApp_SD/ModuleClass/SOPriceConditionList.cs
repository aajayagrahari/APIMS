using System;
using System.Collections.Generic;
using System.Text;

namespace RSMApp_SD
{
    public class SOPriceConditionList : List<SOPriceCondition>
    {
        
        public SOPriceConditionList(string argSODocCode, string argClientCode)
        {
            LoadAllSOPriceCondition(argSODocCode, argClientCode);
        }

        private void LoadAllSOPriceCondition(string argSODocCode, string argClientCode)
        {
            if (this.Count > 0)
                this.Clear();

            SOPriceConditionManager objSOPriceConditionManager = new SOPriceConditionManager();

            objSOPriceConditionManager.colGetSOPriceCondition(argSODocCode, argClientCode, this);
        }

        public SOPriceCondition GetSOPriceConditionByID(string argItemNo, string argSODocCode, string argConditionType)
        {
            foreach (SOPriceCondition argSOPriceCondition in this)
            {
                if (argSOPriceCondition.ItemNo == argItemNo && argSOPriceCondition.SODocCode == argSODocCode && argSOPriceCondition.ConditionType == argConditionType)
                {
                    return argSOPriceCondition;
                }
            }
            return null;
        }   
    }
}
