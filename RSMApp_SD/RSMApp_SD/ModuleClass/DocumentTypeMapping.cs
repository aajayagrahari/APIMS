
//Created On :: 12, September, 2012
//Private const string ClassName = "DocumentTypeMapping"
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
    public class DocumentTypeMapping
    {
        private string _FromDocType;
        private string _FrmDocTypeCode;
        private string _FrmDocTypeDesc;
        private string _ToDocType;
        private string _ToDocTypeCode;
        private string _ToDocTypeDesc;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string FromDocType
        {
            get
            {
                return _FromDocType;
            }
            set
            {
                _FromDocType = value;
            }
        }
        public string FrmDocTypeCode
        {
            get
            {
                return _FrmDocTypeCode;
            }
            set
            {
                _FrmDocTypeCode = value;
            }
        }
        public string FrmDocTypeDesc
        {
            get
            {
                return _FrmDocTypeDesc;
            }
            set
            {
                _FrmDocTypeDesc = value;
            }
        }
        public string ToDocType
        {
            get
            {
                return _ToDocType;
            }
            set
            {
                _ToDocType = value;
            }
        }
        public string ToDocTypeCode
        {
            get
            {
                return _ToDocTypeCode;
            }
            set
            {
                _ToDocTypeCode = value;
            }
        }
        public string ToDocTypeDesc
        {
            get
            {
                return _ToDocTypeDesc;
            }
            set
            {
                _ToDocTypeDesc = value;
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
            this.FromDocType = Convert.ToString(dr["FromDocType"]);
            this.FrmDocTypeCode = Convert.ToString(dr["FrmDocTypeCode"]);
            this.FrmDocTypeDesc = Convert.ToString(dr["FrmDocTypeDesc"]);
            this.ToDocType = Convert.ToString(dr["ToDocType"]);
            this.ToDocTypeCode = Convert.ToString(dr["ToDocTypeCode"]);
            this.ToDocTypeDesc = Convert.ToString(dr["ToDocTypeDesc"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}