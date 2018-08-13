
//Created On :: 23, May, 2012
//Private const string ClassName = "GLAccount"
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
    public class GLAccount
    {
        private string _GLCode;
        private string _GLDesc;
        private string _GLType;
        private string _ChartACCode;
        private string _CompanyCode;
        private int _PostLocalCurrency;
        private int _AutoPost;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string GLCode
        {
            get
            {
                return _GLCode;
            }
            set
            {
                _GLCode = value;
            }
        }
        public string GLDesc
        {
            get
            {
                return _GLDesc;
            }
            set
            {
                _GLDesc = value;
            }
        }
        public string GLType
        {
            get
            {
                return _GLType;
            }
            set
            {
                _GLType = value;
            }
        }
       
        public string ChartACCode
        {
            get
            {
                return _ChartACCode;
            }
            set
            {
                _ChartACCode = value;
            }
        }
        public string CompanyCode
        {
            get
            {
                return _CompanyCode;
            }
            set
            {
                _CompanyCode = value;
            }
        }
        public int PostLocalCurrency
        {
            get
            {
                return _PostLocalCurrency;
            }
            set
            {
                _PostLocalCurrency = value;
            }
        }
        public int AutoPost
        {
            get
            {
                return _AutoPost;
            }
            set
            {
                _AutoPost = value;
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
            this.GLCode = Convert.ToString(dr["GLCode"]);
            this.GLDesc = Convert.ToString(dr["GLDesc"]);
            this.GLType = Convert.ToString(dr["GLType"]);
            this.ChartACCode = Convert.ToString(dr["ChartACCode"]);
            this.CompanyCode = Convert.ToString(dr["CompanyCode"]);
            this.PostLocalCurrency = Convert.ToInt32(dr["PostLocalCurrency"]);
            this.AutoPost = Convert.ToInt32(dr["AutoPost"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}