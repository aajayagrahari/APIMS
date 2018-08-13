
//Created On :: 12, May, 2012
//Private const string ClassName = "RoleMaster"
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
    public class RoleMaster
    {
        private string _RoleCode;
        private string _Role;
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
        public string Role
        {
            get
            {
                return _Role;
            }
            set
            {
                _Role = value;
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
            this.Role = Convert.ToString(dr["Role"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}