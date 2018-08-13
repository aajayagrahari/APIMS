
//Created On :: 18, October, 2012
//Private const string ClassName = "CallEstimation"
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
    public class CallEstimation
    {
        private string _CallCode;
        private int _ItemNo;
        private int _EstimateNo;
        private double _EstPartsCost;
        private double _EstLabourCost;
        private double _EstShippingCost;
        private double _EstHandlingCost;
        private double _EstTravelCost;
        private double _EstTotal;
        private string _EstAppStatus;
        private string _AppDate;
        private string _PartnerCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string CallCode
        {
            get
            {
                return _CallCode;
            }
            set
            {
                _CallCode = value;
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

        public int EstimateNo
        {
            get
            {
                return _EstimateNo;
            }
            set
            {
                _EstimateNo = value;
            }
        }
        public double EstPartsCost
        {
            get
            {
                return _EstPartsCost;
            }
            set
            {
                _EstPartsCost = value;
            }
        }
        public double EstLabourCost
        {
            get
            {
                return _EstLabourCost;
            }
            set
            {
                _EstLabourCost = value;
            }
        }
        public double EstShippingCost
        {
            get
            {
                return _EstShippingCost;
            }
            set
            {
                _EstShippingCost = value;
            }
        }
        public double EstHandlingCost
        {
            get
            {
                return _EstHandlingCost;
            }
            set
            {
                _EstHandlingCost = value;
            }
        }
        public double EstTravelCost
        {
            get
            {
                return _EstTravelCost;
            }
            set
            {
                _EstTravelCost = value;
            }
        }
        public double EstTotal
        {
            get
            {
                return _EstTotal;
            }
            set
            {
                _EstTotal = value;
            }
        }
        public string EstAppStatus
        {
            get
            {
                return _EstAppStatus;
            }
            set
            {
                _EstAppStatus = value;
            }
        }
        public string AppDate
        {
            get
            {
                return _AppDate;
            }
            set
            {
                _AppDate = value;
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
            this.CallCode = Convert.ToString(dr["CallCode"]);
            this.EstimateNo = Convert.ToInt32(dr["EstimateNo"]);
            this.EstPartsCost = Convert.ToDouble(dr["EstPartsCost"]);
            this.EstLabourCost = Convert.ToDouble(dr["EstLabourCost"]);
            this.EstShippingCost = Convert.ToDouble(dr["EstShippingCost"]);
            this.EstHandlingCost = Convert.ToDouble(dr["EstHandlingCost"]);
            this.EstTravelCost = Convert.ToDouble(dr["EstTravelCost"]);
            this.EstTotal = Convert.ToDouble(dr["EstTotal"]);
            this.EstAppStatus = Convert.ToString(dr["EstAppStatus"]);
            this.AppDate = Convert.ToDateTime(dr["AppDate"]).ToString("yyyy-MM-dd");
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}