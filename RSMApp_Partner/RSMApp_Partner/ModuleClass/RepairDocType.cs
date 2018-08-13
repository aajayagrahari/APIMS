
//Created On :: 12, October, 2012
//Private const string ClassName = "RepairDocType"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    [Serializable]
    public class RepairDocType
    {
        private string _RepairDocTypeCode;
        private string _RepairTypeDesc;
        private int _ItemNoIncr;
        private string _NumRange;
        private int _SaveMode;
        private string _RepairType;
        private string _RepairProcessCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;

        public string RepairDocTypeCode
        {
            get
            {
                return _RepairDocTypeCode;
            }
            set
            {
                _RepairDocTypeCode = value;
            }
        }
        public string RepairTypeDesc
        {
            get
            {
                return _RepairTypeDesc;
            }
            set
            {
                _RepairTypeDesc = value;
            }
        }
        public int ItemNoIncr
        {
            get
            {
                return _ItemNoIncr;
            }
            set
            {
                _ItemNoIncr = value;
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

        public string RepairType
        {
            get
            {
                return _RepairType;
            }
            set
            {
                _RepairType = value;
            }
        }

        public string RepairProcessCode
        {
            get
            {
                return _RepairProcessCode;
            }
            set
            {
                _RepairProcessCode = value;
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
            this.RepairDocTypeCode = Convert.ToString(dr["RepairDocTypeCode"]);
            this.RepairTypeDesc = Convert.ToString(dr["RepairTypeDesc"]);
            this.ItemNoIncr = Convert.ToInt32(dr["ItemNoIncr"]);
            this.NumRange = Convert.ToString(dr["NumRange"]);
            this.SaveMode = Convert.ToInt32(dr["SaveMode"]);
            this.RepairType = Convert.ToString(dr["RepairType"]);
            this.RepairProcessCode = Convert.ToString(dr["RepairProcessCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}