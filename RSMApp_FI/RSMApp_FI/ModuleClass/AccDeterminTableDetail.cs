
//Created On :: 05, October, 2012
//Private const string ClassName = "AccDeterminTableDetail"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_FI
{
    [Serializable]
    public class AccDeterminTableDetail
    {
        private string _AccDMTableCode;
        private string _FieldName;
        private string _MasterTableName;
        private string _MasterTableField;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string AccDMTableCode
        {
            get
            {
                return _AccDMTableCode;
            }
            set
            {
                _AccDMTableCode = value;
            }
        }
        public string FieldName
        {
            get
            {
                return _FieldName;
            }
            set
            {
                _FieldName = value;
            }
        }
        public string MasterTableName
        {
            get
            {
                return _MasterTableName;
            }
            set
            {
                _MasterTableName = value;
            }
        }
        public string MasterTableField
        {
            get
            {
                return _MasterTableField;
            }
            set
            {
                _MasterTableField = value;
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
            this.AccDMTableCode = Convert.ToString(dr["AccDMTableCode"]);
            this.FieldName = Convert.ToString(dr["FieldName"]);
            this.MasterTableName = Convert.ToString(dr["MasterTableName"]);
            this.MasterTableField = Convert.ToString(dr["MasterTableField"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}