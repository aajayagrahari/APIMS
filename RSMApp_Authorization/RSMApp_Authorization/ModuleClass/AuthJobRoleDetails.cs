
//Created On :: 24, July, 2012
//Private const string ClassName = "AuthJobRoleDetails"
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
    public class AuthJobRoleDetails
    {
        private string _AuthJobRoleCode;
        private string _ModuleType;
        private string _Module;
        private int _CreateAllowed;
        private int _ModifiedAllowed;
        private int _DeleteAllowed;
        private int _PrintAllowed;
        private int _DisplayAllowed;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



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
            this.AuthJobRoleCode = Convert.ToString(dr["AuthJobRoleCode"]);
            this.ModuleType = Convert.ToString(dr["ModuleType"]);
            this.Module = Convert.ToString(dr["Module"]);
            this.CreateAllowed = Convert.ToInt32(dr["CreateAllowed"]);
            this.ModifiedAllowed = Convert.ToInt32(dr["ModifiedAllowed"]);
            this.DeleteAllowed = Convert.ToInt32(dr["DeleteAllowed"]);
            this.PrintAllowed = Convert.ToInt32(dr["PrintAllowed"]);
            this.DisplayAllowed = Convert.ToInt32(dr["DisplayAllowed"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}