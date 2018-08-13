
//Created On :: 04, July, 2012
//Private const string ClassName = "PartnerFunction"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    [Serializable]
    public class PartnerFunction
    {
        private string _PFunctionCode;
        private string _PartnerType;
        private string _PartnerTable;
        private string _PFunctionDesc;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string PFunctionCode
        {
            get
            {
                return _PFunctionCode;
            }
            set
            {
                _PFunctionCode = value;
            }
        }
        public string PartnerType
        {
            get
            {
                return _PartnerType;
            }
            set
            {
                _PartnerType = value;
            }
        }

        public string PartnerTable
        {
            get
            {
                return _PartnerTable;
            }
            set
            {
                _PartnerTable = value;
            }
        }
        public string PFunctionDesc
        {
            get
            {
                return _PFunctionDesc;
            }
            set
            {
                _PFunctionDesc = value;
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
            this.PFunctionCode = Convert.ToString(dr["PFunctionCode"]);
            this.PartnerType = Convert.ToString(dr["PartnerType"]);
            this.PartnerTable = Convert.ToString(dr["PartnerTable"]);
            this.PFunctionDesc = Convert.ToString(dr["PFunctionDesc"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}