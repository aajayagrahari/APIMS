
//Created On :: 29, September, 2012
//Private const string ClassName = "ExchangeRateType"
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
    public class ExchangeRateType
    {
        private string _ExChngRateTypeCode;
        private string _ExChngRateTypeDesc;
        private string _CreatedBy;
        private string _ModifiedBY;
        private int _IsDeleted;



        public string ExChngRateTypeCode
        {
            get
            {
                return _ExChngRateTypeCode;
            }
            set
            {
                _ExChngRateTypeCode = value;
            }
        }
        public string ExChngRateTypeDesc
        {
            get
            {
                return _ExChngRateTypeDesc;
            }
            set
            {
                _ExChngRateTypeDesc = value;
            }
        }
        public string CreatedBy
        {
            get
            {
                return _CreatedBy;
            }
            set
            {
                _CreatedBy = value;
            }
        }
        public string ModifiedBY
        {
            get
            {
                return _ModifiedBY;
            }
            set
            {
                _ModifiedBY = value;
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
            this.ExChngRateTypeCode = Convert.ToString(dr["ExChngRateTypeCode"]);
            this.ExChngRateTypeDesc = Convert.ToString(dr["ExChngRateTypeDesc"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBY = Convert.ToString(dr["ModifiedBY"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}