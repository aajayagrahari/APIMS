
//Created On :: 15, October, 2012
//Private const string ClassName = "NumRangeMaster"
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
    public class NumRangeMaster
    {
        private string _NumRangeCode;
        private string _Prefix;
        private string _FromRange;
        private string _ToRange;
        private string _CurrentNo;
        private string _DocType;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string NumRangeCode
        {
            get
            {
                return _NumRangeCode;
            }
            set
            {
                _NumRangeCode = value;
            }
        }
        public string Prefix
        {
            get
            {
                return _Prefix;
            }
            set
            {
                _Prefix = value;
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
        public string CurrentNo
        {
            get
            {
                return _CurrentNo;
            }
            set
            {
                _CurrentNo = value;
            }
        }
        public string DocType
        {
            get
            {
                return _DocType;
            }
            set
            {
                _DocType = value;
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
            this.NumRangeCode = Convert.ToString(dr["NumRangeCode"]);
            this.Prefix = Convert.ToString(dr["Prefix"]);
            this.FromRange = Convert.ToString(dr["FromRange"]);
            this.ToRange = Convert.ToString(dr["ToRange"]);
            this.CurrentNo = Convert.ToString(dr["CurrentNo"]);
            this.DocType = Convert.ToString(dr["DocType"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}