
//Created On :: 28, November, 2012
//Private const string ClassName = "CallInvoiceDetail"
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
    public class CallInvoiceDetail
    {
        private string _CallCode;
        private int _CallItemNo;
        private string _RepairProcDocCode;
        private double _PartsCost;
        private double _LabourCost;
        private double _ShippingCost;
        private double _HandlingCost;
        private double _TravelCost;
        private double _SubTotal;
        private double _SalesVatPer;
        private double _SalesVatAmt;
        private double _ServiceTaxPer;
        private double _ServiceTaxAmt;
        private double _NetAmt;
        private int _IsPaid;
        private int _IsExempted;
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
        public int CallItemNo
        {
            get
            {
                return _CallItemNo;
            }
            set
            {
                _CallItemNo = value;
            }
        }
        public string RepairProcDocCode
        {
            get
            {
                return _RepairProcDocCode;
            }
            set
            {
                _RepairProcDocCode = value;
            }
        }
        public double PartsCost
        {
            get
            {
                return _PartsCost;
            }
            set
            {
                _PartsCost = value;
            }
        }
        public double LabourCost
        {
            get
            {
                return _LabourCost;
            }
            set
            {
                _LabourCost = value;
            }
        }
        public double ShippingCost
        {
            get
            {
                return _ShippingCost;
            }
            set
            {
                _ShippingCost = value;
            }
        }
        public double HandlingCost
        {
            get
            {
                return _HandlingCost;
            }
            set
            {
                _HandlingCost = value;
            }
        }
        public double TravelCost
        {
            get
            {
                return _TravelCost;
            }
            set
            {
                _TravelCost = value;
            }
        }
        public double SubTotal
        {
            get
            {
                return _SubTotal;
            }
            set
            {
                _SubTotal = value;
            }
        }
        public double SalesVatPer
        {
            get
            {
                return _SalesVatPer;
            }
            set
            {
                _SalesVatPer = value;
            }
        }
        public double SalesVatAmt
        {
            get
            {
                return _SalesVatAmt;
            }
            set
            {
                _SalesVatAmt = value;
            }
        }
        public double ServiceTaxPer
        {
            get
            {
                return _ServiceTaxPer;
            }
            set
            {
                _ServiceTaxPer = value;
            }
        }
        public double ServiceTaxAmt
        {
            get
            {
                return _ServiceTaxAmt;
            }
            set
            {
                _ServiceTaxAmt = value;
            }
        }
        public double NetAmt
        {
            get
            {
                return _NetAmt;
            }
            set
            {
                _NetAmt = value;
            }
        }
        public int IsPaid
        {
            get
            {
                return _IsPaid;
            }
            set
            {
                _IsPaid = value;
            }
        }
        public int IsExempted
        {
            get
            {
                return _IsExempted;
            }
            set
            {
                _IsExempted = value;
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
            this.CallItemNo = Convert.ToInt32(dr["CallItemNo"]);
            this.RepairProcDocCode = Convert.ToString(dr["RepairProcDocCode"]);
            this.PartsCost = Convert.ToDouble(dr["PartsCost"]);
            this.LabourCost = Convert.ToDouble(dr["LabourCost"]);
            this.ShippingCost = Convert.ToDouble(dr["ShippingCost"]);
            this.HandlingCost = Convert.ToDouble(dr["HandlingCost"]);
            this.TravelCost = Convert.ToDouble(dr["TravelCost"]);
            this.SubTotal = Convert.ToDouble(dr["SubTotal"]);
            this.SalesVatPer = Convert.ToDouble(dr["SalesVatPer"]);
            this.SalesVatAmt = Convert.ToDouble(dr["SalesVatAmt"]);
            this.ServiceTaxPer = Convert.ToDouble(dr["ServiceTaxPer"]);
            this.ServiceTaxAmt = Convert.ToDouble(dr["ServiceTaxAmt"]);
            this.NetAmt = Convert.ToDouble(dr["NetAmt"]);
            this.IsPaid = Convert.ToInt32(dr["IsPaid"]);
            this.IsExempted = Convert.ToInt32(dr["IsExempted"]);
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