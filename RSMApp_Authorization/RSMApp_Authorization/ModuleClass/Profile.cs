
//Created On :: 12, May, 2012
//Private const string ClassName = "Profile"
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
    public class Profile
    {
        private string _ProfileCode;
        private string _ProfileDesc;
        private string _TranType;
        private string _Modules;
        private string _Activity;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string ProfileCode
        {
            get
            {
                return _ProfileCode;
            }
            set
            {
                _ProfileCode = value;
            }
        }
        public string ProfileDesc
        {
            get
            {
                return _ProfileDesc;
            }
            set
            {
                _ProfileDesc = value;
            }
        }
        public string TranType
        {
            get
            {
                return _TranType;
            }
            set
            {
                _TranType = value;
            }
        }
        public string Modules
        {
            get
            {
                return _Modules;
            }
            set
            {
                _Modules = value;
            }
        }
        public string Activity
        {
            get
            {
                return _Activity;
            }
            set
            {
                _Activity = value;
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
            this.ProfileCode = Convert.ToString(dr["ProfileCode"]);
            this.ProfileDesc = Convert.ToString(dr["ProfileDesc"]);
            this.TranType = Convert.ToString(dr["TranType"]);
            this.Modules = Convert.ToString(dr["Modules"]);
            this.Activity = Convert.ToString(dr["Activity"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}