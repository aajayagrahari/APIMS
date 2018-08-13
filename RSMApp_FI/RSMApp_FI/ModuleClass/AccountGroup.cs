
//Created On :: 05, September, 2012
//Private const string ClassName = "AccountGroup"
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
    public class AccountGroup
    {
        private string _AccGroupCode;
        private string _GroupDesc;
        private string _AccCategoryCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string AccGroupCode
        {
            get
            {
                return _AccGroupCode;
            }
            set
            {
                _AccGroupCode = value;
            }
        }
        public string GroupDesc
        {
            get
            {
                return _GroupDesc;
            }
            set
            {
                _GroupDesc = value;
            }
        }
        public string AccCategoryCode
        {
            get
            {
                return _AccCategoryCode;
            }
            set
            {
                _AccCategoryCode = value;
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
            this.AccGroupCode = Convert.ToString(dr["AccGroupCode"]);
            this.GroupDesc = Convert.ToString(dr["GroupDesc"]);
            this.AccCategoryCode = Convert.ToString(dr["AccCategoryCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}