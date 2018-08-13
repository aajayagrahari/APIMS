
//Created On :: 26, May, 2012
//Private const string ClassName = "ProfitCenter"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_MM
{
    [Serializable]
    public class ProfitCenter
    {
        private string _ProfitCenterCode;
        private string _ProfitCenterDesc;
        private string _ClientCode;
        private string _CreatedDate;
        private string _ModifiedDate;
        private string _CreatedBy;
        private string _ModifiedBy;
        private int _IsDeleted;



        public string ProfitCenterCode
        {
            get
            {
                return _ProfitCenterCode;
            }
            set
            {
                _ProfitCenterCode = value;
            }
        }
        public string ProfitCenterDesc
        {
            get
            {
                return _ProfitCenterDesc;
            }
            set
            {
                _ProfitCenterDesc = value;
            }
        }
        public string ClientCode
        {
            get
            {
                return _ClientCode;
            }
            set
            {
                _ClientCode = value;
            }
        }
        public string CreatedDate
        {
            get
            {
                return _CreatedDate;
            }
            set
            {
                _CreatedDate = value;
            }
        }
        public string ModifiedDate
        {
            get
            {
                return _ModifiedDate;
            }
            set
            {
                _ModifiedDate = value;
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
        public string ModifiedBy
        {
            get
            {
                return _ModifiedBy;
            }
            set
            {
                _ModifiedBy = value;
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
            this.ProfitCenterCode = Convert.ToString(dr["ProfitCenterCode"]);
            this.ProfitCenterDesc = Convert.ToString(dr["ProfitCenterDesc"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}