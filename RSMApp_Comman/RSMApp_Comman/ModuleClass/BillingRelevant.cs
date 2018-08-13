
//Created On :: 06, September, 2012
//Private const string ClassName = "BillingRelevant"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Comman
{
    [Serializable]
    public class BillingRelevant
    {
        private string _BillingRelevantCode;
        private string _BRelevantDesc;
        private int _IsDeleted;



        public string BillingRelevantCode
        {
            get
            {
                return _BillingRelevantCode;
            }
            set
            {
                _BillingRelevantCode = value;
            }
        }
        public string BRelevantDesc
        {
            get
            {
                return _BRelevantDesc;
            }
            set
            {
                _BRelevantDesc = value;
            }
        }
        public int IsDeleted
        {
            get
            {
                return _IsDeleted;
            }
            set
            {
                _IsDeleted = value;
            }
        }


        public void SetObjectInfo(DataRow dr)
        {
            this.BillingRelevantCode = Convert.ToString(dr["BillingRelevantCode"]);
            this.BRelevantDesc = Convert.ToString(dr["BRelevantDesc"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}