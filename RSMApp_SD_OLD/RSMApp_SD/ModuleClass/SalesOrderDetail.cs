
//Created On :: 24, May, 2012
//Private const string ClassName = "SalesOrderDetail"
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
    public class SalesOrderDetail
    {
        private string _SODocCode;
        private string _ItemNo;
        private string _POItemNo;
        private string _PlantCode;
        private string _StoreCode;
        private string _MaterialCode;
        private string _Batch;
        private int _OrderQuantity;
        private int _DeliveryQuantity;
        private string _PriceDate;
        private string _ValClassType;
        private string _CurrencyCode;
        private string _UOMCode;
        private int _NetWeight;
        private int _GrossWeight;
        private string _ItemCategoryCode;
        private string _HighLevelItem;
        private string _ProfitCenter;
        private int _DeliveryBlock;
        private int _BillingBlock;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;

        private string _MaterialTypeCode;        
        private string _MatGroup1Code;
        private string _MatGroup2Code;
        

        public string SODocCode
        {
            get
            {
                return _SODocCode;
            }
            set
            {
                _SODocCode = value;
            }
        }
        public string ItemNo
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
        public string POItemNo
        {
            get
            {
                return _POItemNo;
            }
            set
            {
                _POItemNo = value;
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
        public string Batch
        {
            get
            {
                return _Batch;
            }
            set
            {
                _Batch = value;
            }
        }
        public int OrderQuantity
        {
            get
            {
                return _OrderQuantity;
            }
            set
            {
                _OrderQuantity = value;
            }
        }
        public int DeliveryQuantity
        {
            get
            {
                return _DeliveryQuantity;
            }
            set
            {
                _DeliveryQuantity = value;
            }
        }
        public string PriceDate
        {
            get
            {
                return _PriceDate;
            }
            set
            {
                _PriceDate = value;
            }
        }
        public string ValClassType
        {
            get
            {
                return _ValClassType;
            }
            set
            {
                _ValClassType = value;
            }
        }
        public string CurrencyCode
        {
            get
            {
                return _CurrencyCode;
            }
            set
            {
                _CurrencyCode = value;
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
        public int NetWeight
        {
            get
            {
                return _NetWeight;
            }
            set
            {
                _NetWeight = value;
            }
        }
        public int GrossWeight
        {
            get
            {
                return _GrossWeight;
            }
            set
            {
                _GrossWeight = value;
            }
        }
        public string ItemCategoryCode
        {
            get
            {
                return _ItemCategoryCode;
            }
            set
            {
                _ItemCategoryCode = value;
            }
        }
        public string HighLevelItem
        {
            get
            {
                return _HighLevelItem;
            }
            set
            {
                _HighLevelItem = value;
            }
        }
        public string ProfitCenter
        {
            get
            {
                return _ProfitCenter;
            }
            set
            {
                _ProfitCenter = value;
            }
        }
        public int DeliveryBlock
        {
            get
            {
                return _DeliveryBlock;
            }
            set
            {
                _DeliveryBlock = value;
            }
        }
        public int BillingBlock
        {
            get
            {
                return _BillingBlock;
            }
            set
            {
                _BillingBlock = value;
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

        public string MaterialTypeCode
        {
            get
            {
                return _MaterialTypeCode;
            }
            set
            {
                _MaterialTypeCode = value;
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

        public string MatGroup2Code
        {
            get
            {
                return _MatGroup2Code;
            }
            set
            {
                _MatGroup2Code = value;
            }
        }


        public void SetObjectInfo(DataRow dr)
        {
            this.SODocCode = Convert.ToString(dr["SODocCode"]);
            this.ItemNo = Convert.ToString(dr["ItemNo"]);
            this.POItemNo = Convert.ToString(dr["POItemNo"]);
            this.PlantCode = Convert.ToString(dr["PlantCode"]);
            this.StoreCode = Convert.ToString(dr["StoreCode"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.Batch = Convert.ToString(dr["Batch"]);
            this.OrderQuantity = Convert.ToInt32(dr["OrderQuantity"]);
            this.DeliveryQuantity = Convert.ToInt32(dr["DeliveryQuantity"]);
            this.PriceDate = Convert.ToString(dr["PriceDate"]);
            this.ValClassType = Convert.ToString(dr["ValClassType"]);
            this.CurrencyCode = Convert.ToString(dr["CurrencyCode"]);
            this.UOMCode = Convert.ToString(dr["UOMCode"]);
            this.NetWeight = Convert.ToInt32(dr["NetWeight"]);
            this.GrossWeight = Convert.ToInt32(dr["GrossWeight"]);
            this.ItemCategoryCode = Convert.ToString(dr["ItemCategoryCode"]);
            this.HighLevelItem = Convert.ToString(dr["HighLevelItem"]);
            this.ProfitCenter = Convert.ToString(dr["ProfitCenter"]);
            this.DeliveryBlock = Convert.ToInt32(dr["DeliveryBlock"]);
            this.BillingBlock = Convert.ToInt32(dr["BillingBlock"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

            this.MaterialTypeCode = Convert.ToString(dr["MaterialTypeCode"]);
            this.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]);
            this.MatGroup2Code = Convert.ToString(dr["MatGroup2Code"]);

        }
    }
}