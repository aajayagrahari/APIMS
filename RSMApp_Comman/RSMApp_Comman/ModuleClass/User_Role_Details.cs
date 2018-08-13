
//Created On :: 28, July, 2012
//Private const string ClassName = "User_Role_Details"
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
    public class User_Role_Details
    {
        private string _UserName;
        private string _RoleType;
        private string _AuthRoleCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;


        private string _AuthJobRoleCode;
        private string _AuthJobRoleDesc;
        private string _ModuleType;
        private string _Module;
        private int _CreateAllowed;
        private int _ModifiedAllowed;
        private int _DeleteAllowed;
        private int _PrintAllowed;
        private int _DisplayAllowed;

        private string _AuthOrgcode;
        private string _AuthORGDesc;
        private int _SerialNo;
        private string _FieldName;
        private string _FieldValueFrom;
        private string _FieldValueTo;


        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }
        public string RoleType
        {
            get
            {
                return _RoleType;
            }
            set
            {
                _RoleType = value;
            }
        }
        public string AuthRoleCode
        {
            get
            {
                return _AuthRoleCode;
            }
            set
            {
                _AuthRoleCode = value;
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

        public string AuthJobRoleCode
        {
            get
            {
                return _AuthJobRoleCode;
            }
            set
            {
                _AuthJobRoleCode = value;
            }
        }
        public string AuthJobRoleDesc
        {
            get
            {
                return _AuthJobRoleDesc;
            }
            set
            {
                _AuthJobRoleDesc = value;
            }
        }

        public string ModuleType
        {
            get
            {
                return _ModuleType;
            }
            set
            {
                _ModuleType = value;
            }
        }
        public string Module
        {
            get
            {
                return _Module;
            }
            set
            {
                _Module = value;
            }
        }
        public int CreateAllowed
        {
            get
            {
                return _CreateAllowed;
            }
            set
            {
                _CreateAllowed = value;
            }
        }
        public int ModifiedAllowed
        {
            get
            {
                return _ModifiedAllowed;
            }
            set
            {
                _ModifiedAllowed = value;
            }
        }
        public int DeleteAllowed
        {
            get
            {
                return _DeleteAllowed;
            }
            set
            {
                _DeleteAllowed = value;
            }
        }
        public int PrintAllowed
        {
            get
            {
                return _PrintAllowed;
            }
            set
            {
                _PrintAllowed = value;
            }
        }
        public int DisplayAllowed
        {
            get
            {
                return _DisplayAllowed;
            }
            set
            {
                _DisplayAllowed = value;
            }
        }


        /*Auth Org*/
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

        public string AuthORGDesc
        {
            get
            {
                return _AuthORGDesc;
            }
            set
            {
                _AuthORGDesc = value;
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



        public void SetObjectInfo(DataRow dr)
        {
            this.UserName = Convert.ToString(dr["UserName"]);
            this.RoleType = Convert.ToString(dr["RoleType"]);
            this.AuthRoleCode = Convert.ToString(dr["AuthRoleCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }


        public void SetObjectInfo2(DataRow dr)
        {
            this.UserName = Convert.ToString(dr["UserName"]);
            this.RoleType = Convert.ToString(dr["RoleType"]);
            this.AuthRoleCode = Convert.ToString(dr["AuthRoleCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

            this.AuthJobRoleCode = Convert.ToString(dr["AuthJobRoleCode"]);
            this.AuthJobRoleDesc = Convert.ToString(dr["AuthJobRoleDesc"]);

            this.ModuleType = Convert.ToString(dr["ModuleType"]);
            this.Module = Convert.ToString(dr["Module"]);
            this.CreateAllowed = Convert.ToInt32(dr["CreateAllowed"]);
            this.ModifiedAllowed = Convert.ToInt32(dr["ModifiedAllowed"]);
            this.DeleteAllowed = Convert.ToInt32(dr["DeleteAllowed"]);
            this.PrintAllowed = Convert.ToInt32(dr["PrintAllowed"]);
            this.DisplayAllowed = Convert.ToInt32(dr["DisplayAllowed"]);

        }


        public void SetObjectInfo3(DataRow dr)
        {
            this.UserName = Convert.ToString(dr["UserName"]);
            this.RoleType = Convert.ToString(dr["RoleType"]);
            this.AuthRoleCode = Convert.ToString(dr["AuthRoleCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

            this.AuthOrgcode = Convert.ToString(dr["AuthOrgcode"]);
            this.AuthORGDesc = Convert.ToString(dr["AuthORGDesc"]);

            this.SerialNo = Convert.ToInt32(dr["SerialNo"]);
            this.FieldName = Convert.ToString(dr["FieldName"]);
            this.FieldValueFrom = Convert.ToString(dr["FieldValueFrom"]);
            this.FieldValueTo = Convert.ToString(dr["FieldValueTo"]);

        }

    }
}