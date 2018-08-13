
//Created On :: 29, November, 2012
//Private const string ClassName = "ReplacementOrder"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    [Serializable]
    public class ReplacementOrder
    {
        private string _RepOrderCode;
        private string _RepOrderDocTypeCode;
        private string _RepOrderDate;
        private int _TotalQuantity;
        private string _RepOrderStatus;
        private string _IssueDocCode;
        private string _PartnerCode;
        private string _ToPartnerCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;

        private string _OrderType;


        public string RepOrderCode
        {
            get
            {
                return _RepOrderCode;
            }
            set
            {
                _RepOrderCode = value;
            }
        }
        public string RepOrderDocTypeCode
        {
            get
            {
                return _RepOrderDocTypeCode;
            }
            set
            {
                _RepOrderDocTypeCode = value;
            }
        }
        public string RepOrderDate
        {
            get
            {
                return _RepOrderDate;
            }
            set
            {
                _RepOrderDate = value;
            }
        }
        public int TotalQuantity
        {
            get
            {
                return _TotalQuantity;
            }
            set
            {
                _TotalQuantity = value;
            }
        }
        public string RepOrderStatus
        {
            get
            {
                return _RepOrderStatus;
            }
            set
            {
                _RepOrderStatus = value;
            }
        }
        public string IssueDocCode
        {
            get
            {
                return _IssueDocCode;
            }
            set
            {
                _IssueDocCode = value;
            }
        }
        public string PartnerCode
        {
            get
            {
                return _PartnerCode;
            }
            set
            {
                _PartnerCode = value;
            }
        }

        public string ToPartnerCode
        {
            get
            {
                return _ToPartnerCode;
            }
            set
            {
                _ToPartnerCode = value;
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

        public string OrderType
        {
            get
            {
                return _OrderType;
            }
            set
            {
                _OrderType = value;
            }
        }


        public void SetObjectInfo(DataRow dr)
        {
            this.RepOrderCode = Convert.ToString(dr["RepOrderCode"]);
            this.RepOrderDocTypeCode = Convert.ToString(dr["RepOrderDocTypeCode"]);
            this.RepOrderDate = Convert.ToString(dr["RepOrderDate"]);
            this.TotalQuantity = Convert.ToInt32(dr["TotalQuantity"]);
            this.RepOrderStatus = Convert.ToString(dr["RepOrderStatus"]);
            this.IssueDocCode = Convert.ToString(dr["IssueDocCode"]);
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.ToPartnerCode = Convert.ToString(dr["ToPartnerCode"]);
            this.OrderType = Convert.ToString(dr["OrderType"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}