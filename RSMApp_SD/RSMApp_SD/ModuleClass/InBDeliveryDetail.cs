
//Created On :: 19, October, 2012
//Private const string ClassName = "InBDeliveryDetail"
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
    public class InBDeliveryDetail
    {
        private string _InBDeliveryDocCode;
        private string _ItemNo;
        private string _PODocCode;
        private string _POItemNo;
        private string _DeliveryDocCode;
        private string _DLItemNo;
        private string _MaterialCode;
        private string _Batch;
        private string _PlantCode;
        private string _StoreCode;
        private string _ItemCategoryCode;
        private string _MatMovementCode;
        private int _Quantity;
        private int _PriceUnit;
        private string _UOMCode;
        private string _RCStatus;
        private int _ReceiptQty;
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

        public string InBDeliveryDocCode
        {
            get
            {
                return _InBDeliveryDocCode;
            }
            set
            {
                _InBDeliveryDocCode = value;
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
        public string DeliveryDocCode
        {
            get
            {
                return _DeliveryDocCode;
            }
            set
            {
                _DeliveryDocCode = value;
            }
        }
        public string DLItemNo
        {
            get
            {
                return _DLItemNo;
            }
            set
            {
                _DLItemNo = value;
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
        public string MatMovementCode
        {
            get
            {
                return _MatMovementCode;
            }
            set
            {
                _MatMovementCode = value;
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
        public int PriceUnit
        {
            get
            {
                return _PriceUnit;
            }
            set
            {
                _PriceUnit = value;
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
        public string RCStatus
        {
            get
            {
                return _RCStatus;
            }
            set
            {
                _RCStatus = value;
            }
        }
        public int ReceiptQty
        {
            get
            {
                return _ReceiptQty;
            }
            set
            {
                _ReceiptQty = value;
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
            this.InBDeliveryDocCode = Convert.ToString(dr["InBDeliveryDocCode"]);
            this.ItemNo = Convert.ToString(dr["ItemNo"]);
            this.PODocCode = Convert.ToString(dr["PODocCode"]);
            this.POItemNo = Convert.ToString(dr["POItemNo"]);
            this.DeliveryDocCode = Convert.ToString(dr["DeliveryDocCode"]);
            this.DLItemNo = Convert.ToString(dr["DLItemNo"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.Batch = Convert.ToString(dr["Batch"]);
            this.PlantCode = Convert.ToString(dr["PlantCode"]);
            this.StoreCode = Convert.ToString(dr["StoreCode"]);
            this.ItemCategoryCode = Convert.ToString(dr["ItemCategoryCode"]);
            this.MatMovementCode = Convert.ToString(dr["MatMovementCode"]);
            this.Quantity = Convert.ToInt32(dr["Quantity"]);
            this.PriceUnit = Convert.ToInt32(dr["PriceUnit"]);
            this.UOMCode = Convert.ToString(dr["UOMCode"]);
            this.RCStatus = Convert.ToString(dr["RCStatus"]);
            this.ReceiptQty = Convert.ToInt32(dr["ReceiptQty"]);
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