
//Created On :: 12, May, 2012
//Private const string ClassName = "ProfileDetail"
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
    public class ProfileDetail
    {
        private string _ProfileCode;
        private int _ItemNo;
        private string _ClientCode;
        private string _FieldName;
        private string _FromRange;
        private string _ToRange;
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
        public int ItemNo
        {
            get
            {
                return _ItemNo;
            }
            set
            {
                _ItemNo = value;
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
        public string FromRange
        {
            get
            {
                return _FromRange;
            }
            set
            {
                _FromRange = value;
            }
        }
        public string ToRange
        {
            get
            {
                return _ToRange;
            }
            set
            {
                _ToRange = value;
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
            this.ItemNo = Convert.ToInt32(dr["ItemNo"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.FieldName = Convert.ToString(dr["FieldName"]);
            this.FromRange = Convert.ToString(dr["FromRange"]);
            this.ToRange = Convert.ToString(dr["ToRange"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}