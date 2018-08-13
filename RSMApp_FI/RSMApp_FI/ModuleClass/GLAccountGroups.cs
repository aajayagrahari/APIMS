
//Created On :: 07, May, 2012
//Private const string ClassName = "GLAccountGroups"
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
    public class GLAccountGroups
    {
        private string _ChartACCode;
        private string _ActGroup;
        private string _GroupName;
        private string _FromSRNO;
        private string _ToSRNO;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



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
        public string ActGroup
        {
            get
            {
                return _ActGroup;
            }
            set
            {
                _ActGroup = value;
            }
        }
        public string GroupName
        {
            get
            {
                return _GroupName;
            }
            set
            {
                _GroupName = value;
            }
        }
        public string FromSRNO
        {
            get
            {
                return _FromSRNO;
            }
            set
            {
                _FromSRNO = value;
            }
        }
        public string ToSRNO
        {
            get
            {
                return _ToSRNO;
            }
            set
            {
                _ToSRNO = value;
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
            this.ChartACCode = Convert.ToString(dr["ChartACCode"]);
            this.ActGroup = Convert.ToString(dr["ActGroup"]);
            this.GroupName = Convert.ToString(dr["GroupName"]);
            this.FromSRNO = Convert.ToString(dr["FromSRNO"]);
            this.ToSRNO = Convert.ToString(dr["ToSRNO"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}