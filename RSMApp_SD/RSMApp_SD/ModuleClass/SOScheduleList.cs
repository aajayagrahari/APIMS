using System;
using System.Collections.Generic;
using System.Text;

namespace RSMApp_SD
{
    public class SOScheduleList : List<SOSchedule>
    {
        public SOScheduleList(string argSODocCode, string argClientCode)
        {
            LoadAllSOSchedule(argSODocCode, argClientCode);
        }

        private void LoadAllSOSchedule(string argSODocCode, string argClientCode)
        {
            if (this.Count > 0)
                this.Clear();

            SOScheduleManager objSOScheduleManager = new SOScheduleManager();

            objSOScheduleManager.colGetSOSchedule(argClientCode, argSODocCode, this);
        }
             

        public SOSchedule GetSOScheduleByID(string argItemNo, string argSODocCode)
        {
            foreach (SOSchedule argSOSchedule in this)
            {
                if (argSOSchedule.ItemNo == argItemNo && argSOSchedule.SODocCode == argSODocCode)
                {
                    return argSOSchedule;
                }
            }
            return null;
        }

        public SOSchedule GetSOScheduleBySOSID(string argSOSItemNo, string argItemNo, string argSODocCode)
        {
            foreach (SOSchedule argSOSchedule in this)
            {
                if (argSOSchedule.SOSItemNo.Trim() == argSOSItemNo.Trim() && argSOSchedule.ItemNo.Trim() == argItemNo.Trim() && argSOSchedule.SODocCode.Trim() == argSODocCode.Trim())
                {
                    return argSOSchedule;
                }
            }
            return null;
        }

    }
}
