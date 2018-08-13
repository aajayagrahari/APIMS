
//Created On :: 06, June, 2012
//Private const string ClassName = "SAPField"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SAP
{
    [Serializable]
    public class SAPField
    {
        private string _ProcessCode;
        private string _ParamNo;
        private string _FieldNo;
        private string _FieldName;
        private string _InputMethod;
        private string _InputValue;
        private string _ParamType;
        private string _Size;
        private string _ZeroPrefix;
        private string _MapTableName;
        private string _MapFieldValue;
        private string _ParamDataType;
        private string _DataTypeFormat;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string ProcessCode
        {
            get
            {
                return _ProcessCode;
            }
            set
            {
                _ProcessCode = value;
            }
        }
        public string ParamNo
        {
            get
            {
                return _ParamNo;
            }
            set
            {
                _ParamNo = value;
            }
        }
        public string FieldNo
        {
            get
            {
                return _FieldNo;
            }
            set
            {
                _FieldNo = value;
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
        public string InputMethod
        {
            get
            {
                return _InputMethod;
            }
            set
            {
                _InputMethod = value;
            }
        }
        public string InputValue
        {
            get
            {
                return _InputValue;
            }
            set
            {
                _InputValue = value;
            }
        }
        public string ParamType
        {
            get
            {
                return _ParamType;
            }
            set
            {
                _ParamType = value;
            }
        }
        public string Size
        {
            get
            {
                return _Size;
            }
            set
            {
                _Size = value;
            }
        }
        public string ZeroPrefix
        {
            get
            {
                return _ZeroPrefix;
            }
            set
            {
                _ZeroPrefix = value;
            }
        }
        public string MapTableName
        {
            get
            {
                return _MapTableName;
            }
            set
            {
                _MapTableName = value;
            }
        }
        public string MapFieldValue
        {
            get
            {
                return _MapFieldValue;
            }
            set
            {
                _MapFieldValue = value;
            }
        }

        public string ParamDataType
        {
            get
            {
                return _ParamDataType;
            }
            set
            {
                _ParamDataType = value;
            }
        }

        public string DataTypeFormat
        {
            get
            {
                return _DataTypeFormat;
            }
            set
            {
                _DataTypeFormat = value;
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
            this.ProcessCode = Convert.ToString(dr["ProcessCode"]);
            this.ParamNo = Convert.ToString(dr["ParamNo"]);
            this.FieldNo = Convert.ToString(dr["FieldNo"]);
            this.FieldName = Convert.ToString(dr["FieldName"]);
            this.InputMethod = Convert.ToString(dr["InputMethod"]);
            this.InputValue = Convert.ToString(dr["InputValue"]);
            this.ParamType = Convert.ToString(dr["ParamType"]);
            this.Size = Convert.ToString(dr["Size"]);
            this.ZeroPrefix = Convert.ToString(dr["ZeroPrefix"]);
            this.MapTableName = Convert.ToString(dr["MapTableName"]);
            this.MapFieldValue = Convert.ToString(dr["MapFieldValue"]);
            this.ParamDataType = Convert.ToString(dr["ParamDataType"]);
            this.DataTypeFormat = Convert.ToString(dr["DataTypeFormat"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}