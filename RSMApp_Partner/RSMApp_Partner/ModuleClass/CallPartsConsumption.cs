
//Created On :: 27, November, 2012
//Private const string ClassName = "CallPartsConsumption"
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
    public class CallPartsConsumption
    {
        private string _CallCode;
        private int _CallItemNo;
        private string _RepairProcDocCode;
        private int _PCItemNo;
        private string _SerialNo1;
        private string _SerialNo2;
        private string _MaterialCode;
        private string _MatGroup1Code;
        private string _PartnerCode;
        private string _PartnerEmployeeCode;
        private string _StoreCode;
        private string _StockIndicator;
        private string _ToPartnerEmployeeCode;
        private string _ToStoreCode;
        private string _ToStockIndicator;
        private string _ToMaterialCode;
        private int _Quantity;
        private string _UOMCode;
        private double _UnitPrice;
        private string _MaterialDocTypeCode;
        private int _HigherLevel;
        private string _PGoodsMovementCode;
        private int _GMItemNo;
        private int _IsUnderWarranty;
        private double _SalesVatPer;
        private double _SalesVatAmt;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;
        
        private string _MatDesc;
        private string _MatGroup1Desc;


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
        public int PCItemNo
        {
            get
            {
                return _PCItemNo;
            }
            set
            {
                _PCItemNo = value;
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
        public string ToPartnerEmployeeCode
        {
            get
            {
                return _ToPartnerEmployeeCode;
            }
            set
            {
                _ToPartnerEmployeeCode = value;
            }
        }
        public string ToStoreCode
        {
            get
            {
                return _ToStoreCode;
            }
            set
            {
                _ToStoreCode = value;
            }
        }
        public string ToStockIndicator
        {
            get
            {
                return _ToStockIndicator;
            }
            set
            {
                _ToStockIndicator = value;
            }
        }
        public string ToMaterialCode
        {
            get
            {
                return _ToMaterialCode;
            }
            set
            {
                _ToMaterialCode = value;
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

        public double UnitPrice
        {
            get
            {
                return _UnitPrice;
            }
            set
            {
                _UnitPrice = value;
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

        public int HigherLevel
        {
            get
            {
                return _HigherLevel;
            }
            set
            {
                _HigherLevel = value;
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

        public int IsUnderWarranty
        {
            get
            {
                return _IsUnderWarranty;
            }
            set
            {
                _IsUnderWarranty = value;
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

        public string MatDesc
        {
            get
            {
                return _MatDesc;
            }
            set
            {
                _MatDesc = value;
            }
        }

        public string MatGroup1Desc
        {
            get
            {
                return _MatGroup1Desc;

            }
            set
            {
                _MatGroup1Desc = value;
            }
        }


        public void SetObjectInfo(DataRow dr)
        {
            this.CallCode = Convert.ToString(dr["CallCode"]);
            this.CallItemNo = Convert.ToInt32(dr["CallItemNo"]);
            this.RepairProcDocCode = Convert.ToString(dr["RepairProcDocCode"]);
            this.PCItemNo = Convert.ToInt32(dr["PCItemNo"]);
            this.SerialNo1 = Convert.ToString(dr["SerialNo1"]);
            this.SerialNo2 = Convert.ToString(dr["SerialNo2"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]);
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.PartnerEmployeeCode = Convert.ToString(dr["PartnerEmployeeCode"]);
            this.StoreCode = Convert.ToString(dr["StoreCode"]);
            this.StockIndicator = Convert.ToString(dr["StockIndicator"]);
            this.ToPartnerEmployeeCode = Convert.ToString(dr["ToPartnerEmployeeCode"]);
            this.ToStoreCode = Convert.ToString(dr["ToStoreCode"]);
            this.ToStockIndicator = Convert.ToString(dr["ToStockIndicator"]);
            this.ToMaterialCode = Convert.ToString(dr["ToMaterialCode"]);
            this.Quantity = Convert.ToInt32(dr["Quantity"]);
            this.UOMCode = Convert.ToString(dr["UOMCode"]);
            this.UnitPrice = Convert.ToDouble(dr["UnitPrice"]);
            this.MaterialDocTypeCode = Convert.ToString(dr["MaterialDocTypeCode"]);
            this.HigherLevel = Convert.ToInt32(dr["HigherLevel"]);
            this.PGoodsMovementCode = Convert.ToString(dr["PGoodsMovementCode"]);
            this.GMItemNo = Convert.ToInt32(dr["GMItemNo"]);
            this.IsUnderWarranty = Convert.ToInt32(dr["IsUnderWarranty"]);
            this.SalesVatPer = Convert.ToDouble(dr["SalesVatPer"]);
            this.SalesVatAmt = Convert.ToDouble(dr["SalesVatAmt"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

            this.MatDesc = Convert.ToString(dr["MatDesc"]);
            this.MatGroup1Desc = Convert.ToString(dr["MatGroup1Desc"]);


        }
    }
}