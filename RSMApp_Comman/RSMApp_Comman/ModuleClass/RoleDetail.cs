
//Created On :: 12, May, 2012
//Private const string ClassName = "RoleDetail"
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
    public class RoleDetail
    {
        private string _RoleCode;
        private string _AuthJobRoleCode;
        private string _AuthOrgCode;
        private string _ClientCode;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string RoleCode
        {
            get
            {
                return _RoleCode;
            }
            set
            {
                _RoleCode = value;
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

        public string AuthOrgCode
        {
            get
            {
                return _AuthOrgCode;
            }
            set
            {
                _AuthOrgCode = value;
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
            this.RoleCode = Convert.ToString(dr["RoleCode"]);
            this.AuthJobRoleCode = Convert.ToString(dr["AuthJobRoleCode"]);
            this.AuthOrgCode = Convert.ToString(dr["AuthOrgCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}