
//Created On :: 15, October, 2012
//Private const string ClassName = "PODocType"
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
    public class PODocType
    {
        private string _PODocTypeCode;
        private string _PODocTypeDesc;
        private string _NumRange;
        private string _POType;
        private int _SaveMode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string PODocTypeCode
        {
            get
            {
                return _PODocTypeCode;
            }
            set
            {
                _PODocTypeCode = value;
            }
        }
        public string PODocTypeDesc
        {
            get
            {
                return _PODocTypeDesc;
            }
            set
            {
                _PODocTypeDesc = value;
            }
        }
        public string NumRange
        {
            get
            {
                return _NumRange;
            }
            set
            {
                _NumRange = value;
            }
        }
        public string POType
        {
            get
            {
                return _POType;
            }
            set
            {
                _POType = value;
            }
        }
        public int SaveMode
        {
            get
            {
                return _SaveMode;
            }
            set
            {
                _SaveMode = value;
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
            this.PODocTypeCode = Convert.ToString(dr["PODocTypeCode"]);
            this.PODocTypeDesc = Convert.ToString(dr["PODocTypeDesc"]);
            this.NumRange = Convert.ToString(dr["NumRange"]);
            this.POType = Convert.ToString(dr["POType"]);
            this.SaveMode = Convert.ToInt32(dr["SaveMode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}