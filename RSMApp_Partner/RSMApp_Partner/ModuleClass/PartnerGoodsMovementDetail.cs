
//Created On :: 30, October, 2012
//Private const string ClassName = "PartnerGoodsMovementDetail"
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
    public class PartnerGoodsMovementDetail
    {
        private string _PGoodsMovementCode;
        private int _ItemNo;
        private string _MaterialCode;
        private string _MatGroup1Code;
        private string _FromPlantCode;
        private string _FromPartnerCode;
        private string _FromStoreCode;
        private string _FromPartnerEmployeeCode;
        private string _ToPlantCode;
        private string _ToPartnerCode;
        private string _ToStoreCode;
        private string _ToPartnerEmployeeCode;
        private string _ToMaterialCode;
        private int _Quantity;
        private string _UOMCode;
        private double _UnitPrice;
        private string _StockIndicator;
        private string _TranRefDocCode;
        private int _TranRefDocItemNo;
        private string _MaterialDocTypeCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;
        private string _ToStockIndicator;
        private string _PartnerCode;

        private int _IsSerialize;
        private string _MatGroup1Desc;
        private string _MatDesc;


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
        public string FromPlantCode
        {
            get
            {
                return _FromPlantCode;
            }
            set
            {
                _FromPlantCode = value;
            }
        }
        public string FromPartnerCode
        {
            get
            {
                return _FromPartnerCode;
            }
            set
            {
                _FromPartnerCode = value;
            }
        }
        public string FromStoreCode
        {
            get
            {
                return _FromStoreCode;
            }
            set
            {
                _FromStoreCode = value;
            }
        }
        public string FromPartnerEmployeeCode
        {
            get
            {
                return _FromPartnerEmployeeCode;
            }
            set
            {
                _FromPartnerEmployeeCode = value;
            }
        }
        public string ToPlantCode
        {
            get
            {
                return _ToPlantCode;
            }
            set
            {
                _ToPlantCode = value;
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

        public string TranRefDocCode
        {
            get
            {
                return _TranRefDocCode;
            }
            set
            {
                _TranRefDocCode = value;
            }
        }

        public int TranRefDocItemNo
        {
            get
            {
                return _TranRefDocItemNo;
            }
            set
            {
                _TranRefDocItemNo = value;
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

        public int IsSerialize
        {
            get
            {
                return _IsSerialize;
            }
            set
            {
                _IsSerialize = value;
            }
        }

        public void SetObjectInfo(DataRow dr)
        {
            this.PGoodsMovementCode = Convert.ToString(dr["PGoodsMovementCode"]);
            this.ItemNo = Convert.ToInt32(dr["ItemNo"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]);
            this.FromPlantCode = Convert.ToString(dr["FromPlantCode"]);
            this.FromPartnerCode = Convert.ToString(dr["FromPartnerCode"]);
            this.FromStoreCode = Convert.ToString(dr["FromStoreCode"]);
            this.FromPartnerEmployeeCode = Convert.ToString(dr["FromPartnerEmployeeCode"]);
            this.ToPlantCode = Convert.ToString(dr["ToPlantCode"]);
            this.ToPartnerCode = Convert.ToString(dr["ToPartnerCode"]);
            this.ToStoreCode = Convert.ToString(dr["ToStoreCode"]);
            this.ToPartnerEmployeeCode = Convert.ToString(dr["ToPartnerEmployeeCode"]);
            this.ToMaterialCode = Convert.ToString(dr["ToMaterialCode"]);
            this.Quantity = Convert.ToInt32(dr["Quantity"]);
            this.UOMCode = Convert.ToString(dr["UOMCode"]);
            this.StockIndicator = Convert.ToString(dr["StockIndicator"]);
            this.ToStockIndicator = Convert.ToString(dr["ToStockIndicator"]);
            this.TranRefDocCode = Convert.ToString(dr["TranRefDocCode"]);
            this.TranRefDocItemNo = Convert.ToInt32(dr["TranRefDocItemNo"]);
            this.MaterialDocTypeCode = Convert.ToString(dr["MaterialDocTypeCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.UnitPrice = Convert.ToDouble(dr["UnitPrice"]);

            this.MatDesc = Convert.ToString(dr["MatDesc"]);
            this.MatGroup1Desc = Convert.ToString(dr["MatGroup1Desc"]);
            this.IsSerialize = Convert.ToInt32(dr["IsSerialize"]);

        }
    }
}