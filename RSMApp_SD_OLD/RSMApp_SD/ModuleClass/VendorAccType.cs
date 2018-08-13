
//Created On :: 15, May, 2012
//Private const string ClassName = "VendorAccType"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    [Serializable]
    public class VendorAccType
    {
        private string _VendorAccTypeCode;
        private string _VendorAccTypeDesc;
        private string _NumRange;
        private string _RangeFrom;
        private string _RangeTo;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string VendorAccTypeCode
        {
            get
            {
                return _VendorAccTypeCode;
            }
            set
            {
                _VendorAccTypeCode = value;
            }
        }
        public string VendorAccTypeDesc
        {
            get
            {
                return _VendorAccTypeDesc;
            }
            set
            {
                _VendorAccTypeDesc = value;
            }
        }
        public string NumRange
        {
            get
            {
                return _NumRange;
            }
            set
            {
                _NumRange = value;
            }
        }
        public string RangeFrom
        {
            get
            {
                return _RangeFrom;
            }
            set
            {
                _RangeFrom = value;
            }
        }
        public string RangeTo
        {
            get
            {
                return _RangeTo;
            }
            set
            {
                _RangeTo = value;
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
            this.VendorAccTypeCode = Convert.ToString(dr["VendorAccTypeCode"]);
            this.VendorAccTypeDesc = Convert.ToString(dr["VendorAccTypeDesc"]);
            this.NumRange = Convert.ToString(dr["NumRange"]);
            this.RangeFrom = Convert.ToString(dr["RangeFrom"]);
            this.RangeTo = Convert.ToString(dr["RangeTo"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}