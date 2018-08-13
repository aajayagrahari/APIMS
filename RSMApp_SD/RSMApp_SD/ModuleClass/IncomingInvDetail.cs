
//Created On :: 26, October, 2012
//Private const string ClassName = "IncomingInvDetail"
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
    public class IncomingInvDetail
    {
        private string _IncomingInvDocCode;
        private int _ItemNo;
        private string _RefDocumentCode;
        private string _RefItemNo;
        private string _MaterialCode;
        private string _PlantCode;
        private string _StoreCode;
        private string _PurchaseOrgCode;
        private string _CompanyCode;
        private string _ValTypeCode;
        private string _ProfitCenter;
        private int _MatVolume;
        private string _VolumeUOM;
        private string _WeightUOM;
        private int _GrossWeight;
        private int _NetWeight;
        private string _PriceDate;
        private int _Quantity;
        private int _PurchaseQty;
        private int _NetValue;
        private int _TaxAmt;
        private int _NetPricePerQty;
        private string _ItemCategoryCode;
        private string _UOMCode;
        private string _PurchaseUOMCode;
        private string _COACode;
        private string _OrginatingDoc;
        private string _OrginatingItem;
        private string _PODoc;
        private string _POItem;
        private string _MatGroup1Code;
        private string _ServiceRenderDate;
        private string _CostCenterCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string IncomingInvDocCode
        {
            get
            {
                return _IncomingInvDocCode;
            }
            set
            {
                _IncomingInvDocCode = value;
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
        public string RefDocumentCode
        {
            get
            {
                return _RefDocumentCode;
            }
            set
            {
                _RefDocumentCode = value;
            }
        }
        public string RefItemNo
        {
            get
            {
                return _RefItemNo;
            }
            set
            {
                _RefItemNo = value;
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
        public string PurchaseOrgCode
        {
            get
            {
                return _PurchaseOrgCode;
            }
            set
            {
                _PurchaseOrgCode = value;
            }
        }
        public string CompanyCode
        {
            get
            {
                return _CompanyCode;
            }
            set
            {
                _CompanyCode = value;
            }
        }
        public string ValTypeCode
        {
            get
            {
                return _ValTypeCode;
            }
            set
            {
                _ValTypeCode = value;
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
        public int MatVolume
        {
            get
            {
                return _MatVolume;
            }
            set
            {
                _MatVolume = value;
            }
        }
        public string VolumeUOM
        {
            get
            {
                return _VolumeUOM;
            }
            set
            {
                _VolumeUOM = value;
            }
        }
        public string WeightUOM
        {
            get
            {
                return _WeightUOM;
            }
            set
            {
                _WeightUOM = value;
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
        public int NetValue
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
        public int TaxAmt
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
        public string PurchaseUOMCode
        {
            get
            {
                return _PurchaseUOMCode;
            }
            set
            {
                _PurchaseUOMCode = value;
            }
        }
        public string COACode
        {
            get
            {
                return _COACode;
            }
            set
            {
                _COACode = value;
            }
        }
        public string OrginatingDoc
        {
            get
            {
                return _OrginatingDoc;
            }
            set
            {
                _OrginatingDoc = value;
            }
        }
        public string OrginatingItem
        {
            get
            {
                return _OrginatingItem;
            }
            set
            {
                _OrginatingItem = value;
            }
        }
        public string PODoc
        {
            get
            {
                return _PODoc;
            }
            set
            {
                _PODoc = value;
            }
        }
        public string POItem
        {
            get
            {
                return _POItem;
            }
            set
            {
                _POItem = value;
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
        public string ServiceRenderDate
        {
            get
            {
                return _ServiceRenderDate;
            }
            set
            {
                _ServiceRenderDate = value;
            }
        }
        public string CostCenterCode
        {
            get
            {
                return _CostCenterCode;
            }
            set
            {
                _CostCenterCode = value;
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
            this.IncomingInvDocCode = Convert.ToString(dr["IncomingInvDocCode"]);
            this.ItemNo = Convert.ToInt32(dr["ItemNo"]);
            this.RefDocumentCode = Convert.ToString(dr["RefDocumentCode"]);
            this.RefItemNo = Convert.ToString(dr["RefItemNo"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.PlantCode = Convert.ToString(dr["PlantCode"]);
            this.StoreCode = Convert.ToString(dr["StoreCode"]);
            this.PurchaseOrgCode = Convert.ToString(dr["PurchaseOrgCode"]);
            this.CompanyCode = Convert.ToString(dr["CompanyCode"]);
            this.ValTypeCode = Convert.ToString(dr["ValTypeCode"]);
            this.ProfitCenter = Convert.ToString(dr["ProfitCenter"]);
            this.MatVolume = Convert.ToInt32(dr["MatVolume"]);
            this.VolumeUOM = Convert.ToString(dr["VolumeUOM"]);
            this.WeightUOM = Convert.ToString(dr["WeightUOM"]);
            this.GrossWeight = Convert.ToInt32(dr["GrossWeight"]);
            this.NetWeight = Convert.ToInt32(dr["NetWeight"]);
            this.PriceDate = Convert.ToString(dr["PriceDate"]);
            this.Quantity = Convert.ToInt32(dr["Quantity"]);
            this.PurchaseQty = Convert.ToInt32(dr["PurchaseQty"]);
            this.NetValue = Convert.ToInt32(dr["NetValue"]);
            this.TaxAmt = Convert.ToInt32(dr["TaxAmt"]);
            this.NetPricePerQty = Convert.ToInt32(dr["NetPricePerQty"]);
            this.ItemCategoryCode = Convert.ToString(dr["ItemCategoryCode"]);
            this.UOMCode = Convert.ToString(dr["UOMCode"]);
            this.PurchaseUOMCode = Convert.ToString(dr["PurchaseUOMCode"]);
            this.COACode = Convert.ToString(dr["COACode"]);
            this.OrginatingDoc = Convert.ToString(dr["OrginatingDoc"]);
            this.OrginatingItem = Convert.ToString(dr["OrginatingItem"]);
            this.PODoc = Convert.ToString(dr["PODoc"]);
            this.POItem = Convert.ToString(dr["POItem"]);
            this.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]);
            this.ServiceRenderDate = Convert.ToString(dr["ServiceRenderDate"]);
            this.CostCenterCode = Convert.ToString(dr["CostCenterCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}