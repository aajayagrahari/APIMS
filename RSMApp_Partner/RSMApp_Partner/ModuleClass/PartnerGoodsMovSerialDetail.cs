
//Created On :: 30, October, 2012
//Private const string ClassName = "PartnerGoodsMovSerialDetail"
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
    public class PartnerGoodsMovSerialDetail
    {
        private string _PGoodsMovementCode;
        private int _ItemNo;
        private string _MaterialCode;
        private string _MatGroup1Code;
        private string _SerialNo1;
        private string _SerialNo2;
        private string _PartnerCode;
        private string _StoreCode;
        private string _PartnerEmployeeCode;
        private string _PlantCode;
        private string _StockIndicator;
        private string _ToPlantCode;
        private string _ToPartnerCode;
        private string _ToStoreCode;
        private string _ToStockIndicator;
        private string _ToPartnerEmployeeCode;
        private string _ToMaterialCode;
        private string _RefDocCode;
        private int _RefDocItemNo;
        private string _RefDocType;
        private string _TranRefDocCode;
        private int _TranRefDocItemNo;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;

        private string _MatDesc;
        private string _MatGroup1Desc;
        
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
        public string PlantCode
        {
            get
            {
                return _PlantCode;
            }
            set
            {
                _PlantCode = value;
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

        public string RefDocCode
        {
            get
            {
                return _RefDocCode;
            }
            set
            {
                _RefDocCode = value;
            }
        }

        public int RefDocItemNo
        {
            get
            {
                return _RefDocItemNo;
            }
            set
            {
                _RefDocItemNo = value;
            }
        }
        public string RefDocType
        {
            get
            {
                return _RefDocType;
            }
            set
            {
                _RefDocType = value;
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
            this.PGoodsMovementCode = Convert.ToString(dr["PGoodsMovementCode"]);
            this.ItemNo = Convert.ToInt32(dr["ItemNo"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]);
            this.SerialNo1 = Convert.ToString(dr["SerialNo1"]);
            this.SerialNo2 = Convert.ToString(dr["SerialNo2"]);
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.StoreCode = Convert.ToString(dr["StoreCode"]);
            this.PartnerEmployeeCode = Convert.ToString(dr["PartnerEmployeeCode"]);
            this.PlantCode = Convert.ToString(dr["PlantCode"]);
            this.StockIndicator = Convert.ToString(dr["StockIndicator"]);

            this.ToPlantCode = Convert.ToString(dr["ToPlantCode"]);
            this.ToPartnerCode = Convert.ToString(dr["ToPartnerCode"]);
            this.ToStoreCode = Convert.ToString(dr["ToStoreCode"]);
            this.ToStockIndicator = Convert.ToString(dr["ToStockIndicator"]);
            this.ToPartnerEmployeeCode = Convert.ToString(dr["ToPartnerEmployeeCode"]);
            this.ToMaterialCode = Convert.ToString(dr["ToMaterialCode"]);


            this.RefDocCode = Convert.ToString(dr["RefDocCode"]);
            this.RefDocItemNo = Convert.ToInt32(dr["RefDocItemNo"]);
            this.RefDocType = Convert.ToString(dr["RefDocType"]);
            this.TranRefDocCode = Convert.ToString(dr["TranRefDocCode"]);
            this.TranRefDocItemNo = Convert.ToInt32(dr["TranRefDocItemNo"]); 
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