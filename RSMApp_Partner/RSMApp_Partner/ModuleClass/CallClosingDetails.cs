
//Created On :: 08, November, 2012
//Private const string ClassName = "CallClosingDetails"
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
    public class CallClosingDetails
    {
        private string _CallClosingCode;
        private string _CallClosingDocTypeCode;
        private string _CallCode;
        private int _CallItemNo;
        private string _CallClosingDate;
        private string _SerialNo1;
        private string _SerialNo2;
        private string _MaterialCode;
        private string _MatGroup1Code;
        private string _PartnerCode;
        private string _StoreCode;
        private string _StockIndicator;
        private string _PartnerEmployeeCode;
        private int _Quantity;
        private string _UOMCode;
        private string _MaterialDocTypeCode;
        private string _PGoodsMovementCode;
        private int _GMItemNo;
        private double _PayableAmt;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string CallClosingCode
        {
            get
            {
                return _CallClosingCode;
            }
            set
            {
                _CallClosingCode = value;
            }
        }
        public string CallClosingDocTypeCode
        {
            get
            {
                return _CallClosingDocTypeCode;
            }
            set
            {
                _CallClosingDocTypeCode = value;
            }
        }
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

        public string CallClosingDate
        {
            get
            {
                return _CallClosingDate;
            }
            set
            {
                _CallClosingDate = value;
            }
        }

        public string SerialNo1
        {
            get
            {
                return _SerialNo1;
            }
            set
            {
                _SerialNo1 = value;
            }
        }
        public string SerialNo2
        {
            get
            {
                return _SerialNo2;
            }
            set
            {
                _SerialNo2 = value;
            }
        }
        public string MaterialCode
        {
            get
            {
                return _MaterialCode;
            }
            set
            {
                _MaterialCode = value;
            }
        }
        public string MatGroup1Code
        {
            get
            {
                return _MatGroup1Code;
            }
            set
            {
                _MatGroup1Code = value;
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
        public string StoreCode
        {
            get
            {
                return _StoreCode;
            }
            set
            {
                _StoreCode = value;
            }
        }
        public string StockIndicator
        {
            get
            {
                return _StockIndicator;
            }
            set
            {
                _StockIndicator = value;
            }
        }
        public string PartnerEmployeeCode
        {
            get
            {
                return _PartnerEmployeeCode;
            }
            set
            {
                _PartnerEmployeeCode = value;
            }
        }
        public int Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
            }
        }
        public string UOMCode
        {
            get
            {
                return _UOMCode;
            }
            set
            {
                _UOMCode = value;
            }
        }
        public string MaterialDocTypeCode
        {
            get
            {
                return _MaterialDocTypeCode;
            }
            set
            {
                _MaterialDocTypeCode = value;
            }
        }
        public string PGoodsMovementCode
        {
            get
            {
                return _PGoodsMovementCode;
            }
            set
            {
                _PGoodsMovementCode = value;
            }
        }
        public int GMItemNo
        {
            get
            {
                return _GMItemNo;
            }
            set
            {
                _GMItemNo = value;
            }
        }

        public double PayableAmt
        {
            get
            {
                return _PayableAmt;
            }
            set
            {
                _PayableAmt = value;
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
            this.CallClosingCode = Convert.ToString(dr["CallClosingCode"]);
            this.CallClosingDocTypeCode = Convert.ToString(dr["CallClosingDocTypeCode"]);
            this.CallCode = Convert.ToString(dr["CallCode"]);
            this.CallItemNo = Convert.ToInt32(dr["CallItemNo"]);
            this.CallClosingDate = Convert.ToDateTime(dr["CallClosingDate"]).ToString();
            this.SerialNo1 = Convert.ToString(dr["SerialNo1"]);
            this.SerialNo2 = Convert.ToString(dr["SerialNo2"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]);
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.StoreCode = Convert.ToString(dr["StoreCode"]);
            this.StockIndicator = Convert.ToString(dr["StockIndicator"]);
            this.PartnerEmployeeCode = Convert.ToString(dr["PartnerEmployeeCode"]);
            this.Quantity = Convert.ToInt32(dr["Quantity"]);
            this.UOMCode = Convert.ToString(dr["UOMCode"]);
            this.MaterialDocTypeCode = Convert.ToString(dr["MaterialDocTypeCode"]);
            this.PGoodsMovementCode = Convert.ToString(dr["PGoodsMovementCode"]);
            this.GMItemNo = Convert.ToInt32(dr["GMItemNo"]);
            this.PayableAmt = Convert.ToDouble(dr["PayableAmt"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}