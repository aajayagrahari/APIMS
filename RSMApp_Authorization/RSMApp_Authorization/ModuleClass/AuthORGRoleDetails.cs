
//Created On :: 24, July, 2012
//Private const string ClassName = "AuthORGRoleDetails"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Authorization
{
    [Serializable]
    public class AuthORGRoleDetails
    {
        private string _AuthOrgcode;
        private int _SerialNo;
        private string _FieldName;
        private string _FieldValueFrom;
        private string _FieldValueTo;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string AuthOrgcode
        {
            get
            {
                return _AuthOrgcode;
            }
            set
            {
                _AuthOrgcode = value;
            }
        }

        public int SerialNo
        {
            get
            {
                return _SerialNo;
            }
            set
            {
                _SerialNo = value;
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
        public string FieldValueFrom
        {
            get
            {
                return _FieldValueFrom;
            }
            set
            {
                _FieldValueFrom = value;
            }
        }
        public string FieldValueTo
        {
            get
            {
                return _FieldValueTo;
            }
            set
            {
                _FieldValueTo = value;
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
            this.AuthOrgcode = Convert.ToString(dr["AuthOrgcode"]);
            this.SerialNo = Convert.ToInt32(dr["SerialNo"]);
            this.FieldName = Convert.ToString(dr["FieldName"]);
            this.FieldValueFrom = Convert.ToString(dr["FieldValueFrom"]);
            this.FieldValueTo = Convert.ToString(dr["FieldValueTo"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}