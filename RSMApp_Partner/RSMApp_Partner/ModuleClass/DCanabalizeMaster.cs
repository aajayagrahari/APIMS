
//Created On :: 12, January, 2013
//Private const string ClassName = "DCanabalizeMaster"
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
    public class DCanabalizeMaster
    {
        private string _DCanabalizeDocNo;
        private string _DCanablizeDocTypeCode;
        private string _DCanablizeDate;
        private string _SerialNo;
        private string _MaterialCode;
        private string _MatGroup1Code;
        private string _PartnerCode;
        private string _StoreCode;
        private string _StockIndicator;
        private string _PartnerEmployeeCode;
        private string _ToPartnerCode;
        private string _ToStoreCode;
        private string _ToStockIndicator;
        private string _ToPartnerEmployeeCode;
        private string _MaterialDocTypeCode;
        private string _PGoodsMovementCode;
        private int _GMItemNo;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;

        private string _MatDesc;
        private string _MatGroup1Desc;



        public string DCanabalizeDocNo
        {
            get
            {
                return _DCanabalizeDocNo;
            }
            set
            {
                _DCanabalizeDocNo = value;
            }
        }
        public string DCanablizeDocTypeCode
        {
            get
            {
                return _DCanablizeDocTypeCode;
            }
            set
            {
                _DCanablizeDocTypeCode = value;
            }
        }
        public string DCanablizeDate
        {
            get
            {
                return _DCanablizeDate;
            }
            set
            {
                _DCanablizeDate = value;
            }
        }
        public string SerialNo
        {
            get
            {
                return _SerialNo;
            }
            set
            {
                _SerialNo = value;
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
            this.DCanabalizeDocNo = Convert.ToString(dr["DCanabalizeDocNo"]);
            this.DCanablizeDocTypeCode = Convert.ToString(dr["DCanablizeDocTypeCode"]);
            this.DCanablizeDate = Convert.ToString(dr["DCanablizeDate"]);
            this.SerialNo = Convert.ToString(dr["SerialNo"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]);
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.StoreCode = Convert.ToString(dr["StoreCode"]);
            this.StockIndicator = Convert.ToString(dr["StockIndicator"]);
            this.PartnerEmployeeCode = Convert.ToString(dr["PartnerEmployeeCode"]);
            this.ToPartnerCode = Convert.ToString(dr["ToPartnerCode"]);
            this.ToStoreCode = Convert.ToString(dr["ToStoreCode"]);
            this.ToStockIndicator = Convert.ToString(dr["ToStockIndicator"]);
            this.ToPartnerEmployeeCode = Convert.ToString(dr["ToPartnerEmployeeCode"]);
            this.MaterialDocTypeCode = Convert.ToString(dr["MaterialDocTypeCode"]);
            this.PGoodsMovementCode = Convert.ToString(dr["PGoodsMovementCode"]);
            this.GMItemNo = Convert.ToInt32(dr["GMItemNo"]);
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