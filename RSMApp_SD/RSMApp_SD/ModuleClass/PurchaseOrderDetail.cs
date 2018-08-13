
//Created On :: 15, October, 2012
//Private const string ClassName = "PurchaseOrderDetail"
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
    public class PurchaseOrderDetail
    {
        private string _PODocCode;
        private string _ItemNo;
        private string _PlantCode;
        private string _StoreCode;
        private string _MaterialCode;
        private string _Batch;
        private int _OrderQuantity;
        private int _ReceivedQuantity;
        private string _PriceDate;
        private string _ValClassType;
        private string _CurrencyCode;
        private string _UOMCode;
        private int _NetWeight;
        private int _GrossWeight;
        private string _ItemCategoryCode;
        private int _HighLevelItem;
        private string _ProfitCenter;
        private int _ReceiptBlock;
        private int _PurchaseBlock;
        private double _NetValue;
        private double _TaxAmt;
        private int _NetPricePerQty;
        private string _POStatus;
        private int _PurchaseQty;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;
        private string _PlantName;
        private string _MatDesc;
        private string _StoreName;
        private string _ItemCategoryDesc;



        public string PODocCode
        {
            get
            {
                return _PODocCode;
            }
            set
            {
                _PODocCode = value;
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
        public int ReceivedQuantity
        {
            get
            {
                return _ReceivedQuantity;
            }
            set
            {
                _ReceivedQuantity = value;
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
        public int HighLevelItem
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
        public int ReceiptBlock
        {
            get
            {
                return _ReceiptBlock;
            }
            set
            {
                _ReceiptBlock = value;
            }
        }
        public int PurchaseBlock
        {
            get
            {
                return _PurchaseBlock;
            }
            set
            {
                _PurchaseBlock = value;
            }
        }
        public double NetValue
        {
            get
            {
                return _NetValue;
            }
            set
            {
                _NetValue = value;
            }
        }
        public double TaxAmt
        {
            get
            {
                return _TaxAmt;
            }
            set
            {
                _TaxAmt = value;
            }
        }
        public int NetPricePerQty
        {
            get
            {
                return _NetPricePerQty;
            }
            set
            {
                _NetPricePerQty = value;
            }
        }
        public string POStatus
        {
            get
            {
                return _POStatus;
            }
            set
            {
                _POStatus = value;
            }
        }
        public int PurchaseQty
        {
            get
            {
                return _PurchaseQty;
            }
            set
            {
                _PurchaseQty = value;
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

        public string PlantName
        {
            get
            {
                return _PlantName;
            }
            set
            {
                _PlantName = value;
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
        public string StoreName
        {
            get
            {
                return _StoreName;
            }
            set
            {
                _StoreName = value;
            }
        }
        public string ItemCategoryDesc
        {
            get
            {
                return _ItemCategoryDesc;
            }
            set
            {
                _ItemCategoryDesc = value;
            }
        }


        public void SetObjectInfo(DataRow dr)
        {
            this.PODocCode = Convert.ToString(dr["PODocCode"]);
            this.ItemNo = Convert.ToString(dr["ItemNo"]);
            this.PlantCode = Convert.ToString(dr["PlantCode"]);
            this.StoreCode = Convert.ToString(dr["StoreCode"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.Batch = Convert.ToString(dr["Batch"]);
            this.OrderQuantity = Convert.ToInt32(dr["OrderQuantity"]);
            this.ReceivedQuantity = Convert.ToInt32(dr["ReceivedQuantity"]);
            this.PriceDate = Convert.ToString(dr["PriceDate"]);
            this.ValClassType = Convert.ToString(dr["ValClassType"]);
            this.CurrencyCode = Convert.ToString(dr["CurrencyCode"]);
            this.UOMCode = Convert.ToString(dr["UOMCode"]);
            this.NetWeight = Convert.ToInt32(dr["NetWeight"]);
            this.GrossWeight = Convert.ToInt32(dr["GrossWeight"]);
            this.ItemCategoryCode = Convert.ToString(dr["ItemCategoryCode"]);
            this.HighLevelItem = Convert.ToInt32(dr["HighLevelItem"]);
            this.ProfitCenter = Convert.ToString(dr["ProfitCenter"]);
            this.ReceiptBlock = Convert.ToInt32(dr["ReceiptBlock"]);
            this.PurchaseBlock = Convert.ToInt32(dr["PurchaseBlock"]);
            this.NetValue = Convert.ToDouble(dr["NetValue"]);
            this.TaxAmt = Convert.ToDouble(dr["TaxAmt"]);
            this.NetPricePerQty = Convert.ToInt32(dr["NetPricePerQty"]);
            this.POStatus = Convert.ToString(dr["POStatus"]);
            this.PurchaseQty = Convert.ToInt32(dr["PurchaseQty"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

            this.MatDesc = Convert.ToString(dr["MatDesc"]);
            this.PlantName = Convert.ToString(dr["PlantName"]);
            this.StoreName = Convert.ToString(dr["StoreName"]);
            this.ItemCategoryDesc = Convert.ToString(dr["ItemCategoryDesc"]);

        }
    }
}