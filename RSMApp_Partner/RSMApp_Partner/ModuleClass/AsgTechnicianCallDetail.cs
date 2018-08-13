
//Created On :: 01, November, 2012
//Private const string ClassName = "AsgTechnicianCallDetail"
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
    public class AsgTechnicianCallDetail
    {
        private string _AsgTechCallCode;
        private int _ItemNo;
        private string _SerialNo1;
        private string _SerialNo2;
        private string _MaterialCode;
        private string _MatGroup1Code;
        private string _StoreCode;
        private string _StockIndicator;
        private string _RefDocCode;
        private int _RefDocItemNo;
        private string _RefDocTypeCode;
        private int _Quantity;
        private string _UOMCode;
        private string _PartnerCode;
        private string _FromPartnerEmployeeCode;
        private string _ToPartnerCode;
        private string _ToPartnerEmployeeCode;
        private string _ToStoreCode;
        private string _ToMaterialCode;
        private string _ToStockIndicator;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;

        private string _MaterialDocTypeCode;

        private string _PGoodsMovementCode;
        private int _GMItemNo;

        private string _MatDesc;
        private string _MatGroup1Desc;


        public string AsgTechCallCode
        {
            get
            {
                return _AsgTechCallCode;
            }
            set
            {
                _AsgTechCallCode = value;
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
        public string RefDocTypeCode
        {
            get
            {
                return _RefDocTypeCode;
            }
            set
            {
                _RefDocTypeCode = value;
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
            this.AsgTechCallCode = Convert.ToString(dr["AsgTechCallCode"]);
            this.ItemNo = Convert.ToInt32(dr["ItemNo"]);
            this.SerialNo1 = Convert.ToString(dr["SerialNo1"]);
            this.SerialNo2 = Convert.ToString(dr["SerialNo2"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]);
            this.StoreCode = Convert.ToString(dr["StoreCode"]);
            this.StockIndicator = Convert.ToString(dr["StockIndicator"]);
            this.RefDocCode = Convert.ToString(dr["RefDocCode"]);
            this.RefDocItemNo = Convert.ToInt32(dr["RefDocItemNo"]);
            this.RefDocTypeCode = Convert.ToString(dr["RefDocTypeCode"]);
            this.Quantity = Convert.ToInt32(dr["Quantity"]);
            this.UOMCode = Convert.ToString(dr["UOMCode"]);
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.FromPartnerEmployeeCode = Convert.ToString(dr["FromPartnerEmployeeCode"]);
            this.ToPartnerCode = Convert.ToString(dr["ToPartnerCode"]);
            this.ToPartnerEmployeeCode = Convert.ToString(dr["ToPartnerEmployeeCode"]);
            this.ToStoreCode = Convert.ToString(dr["ToStoreCode"]);
            this.ToMaterialCode = Convert.ToString(dr["ToMaterialCode"]);
            this.ToStockIndicator = Convert.ToString(dr["ToStockIndicator"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

            this.MaterialDocTypeCode = Convert.ToString(dr["MaterialDocTypeCode"]);
            this.PGoodsMovementCode = Convert.ToString(dr["PGoodsMovementCode"]);
            this.GMItemNo = Convert.ToInt32(dr["GMItemNo"]);
            this.MatDesc = Convert.ToString(dr["MatDesc"]);
            this.MatGroup1Desc = Convert.ToString(dr["MatGroup1Desc"]);



        }
    }
}