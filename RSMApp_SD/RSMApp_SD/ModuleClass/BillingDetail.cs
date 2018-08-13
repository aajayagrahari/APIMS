
//Created On :: 17, September, 2012
//Private const string ClassName = "BillingDetail"
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
    public class BillingDetail
    {
        private string _BillingDocCode;
        private int _ItemNo;
        private string _RefDocumentCode;
        private string _RefItemNo;
        private string _MaterialCode;
        private string _Batch;
        private string _PlantCode;
        private string _StoreCode;
        private string _SalesOrganizationCode;
        private string _DistChannelCode;
        private string _DivisionCode;
        private string _ShipingPointCode;
        private string _SalesofficeCode;
        private string _SalesGroupCode;
        private string _ValTypeCode;
        private string _ProfitCenter;
        private int _MatVolume;
        private string _VolumeUOM;
        private string _WeightUOM;
        private double _GrossWeight;
        private double _NetWeight;
        private string _PriceDate;
        private string _ItemCategoryCode;
        private double _Quantity;
        private double _BillingQty;
        private double _NetValue;
        private double _TaxAmt;
        private double _NetPricePerQty;
        private string _UOMCode;
        private string _SalesUOMCode;
        private string _COACode;
        private string _BusinessAreaCode;
        private string _ProfitSegment;
        private string _OrginatingDoc;
        private string _OrginatingItem;
        private string _SalesDoc;
        private string _SalesItem;
        private string _MatGroup1Code;
        private string _ServiceRenderDate;
        private string _CostCenterCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;


        private string _MatDesc;
        private string _PlantName;
        private string _StoreName;
        private string _SalesOrgName;
        private string _DistChannel;
        private string _DivisionName;

        public string BillingDocCode
        {
            get
            {
                return _BillingDocCode;
            }
            set
            {
                _BillingDocCode = value;
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

        public string SalesOrganizationCode
        {
            get
            {
                return _SalesOrganizationCode;
            }
            set
            {
                _SalesOrganizationCode = value;
            }
        }
        public string DistChannelCode
        {
            get
            {
                return _DistChannelCode;
            }
            set
            {
                _DistChannelCode = value;
            }
        }
        public string DivisionCode
        {
            get
            {
                return _DivisionCode;
            }
            set
            {
                _DivisionCode = value;
            }
        }

        public string ShipingPointCode
        {
            get
            {
                return _ShipingPointCode;
            }
            set
            {
                _ShipingPointCode = value;
            }
        }

        public string SalesofficeCode
        {
            get
            {
                return _SalesofficeCode;
            }
            set
            {
                _SalesofficeCode = value;
            }
        }

        public string SalesGroupCode
        {
            get
            {
                return _SalesGroupCode;
            }
            set
            {
                _SalesGroupCode = value;
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

        public double GrossWeight
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
        public double NetWeight
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
        public double Quantity
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
        public double BillingQty
        {
            get
            {
                return _BillingQty;
            }
            set
            {
                _BillingQty = value;
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

        public double NetPricePerQty
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

        public string SalesUOMCode
        {
            get
            {
                return _SalesUOMCode;
            }
            set
            {
                _SalesUOMCode = value;
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

        public string BusinessAreaCode
        {
            get
            {
                return _BusinessAreaCode;
            }
            set
            {
                _BusinessAreaCode = value;
            }
        }

        public string ProfitSegment
        {
            get
            {
                return _ProfitSegment;
            }
            set
            {
                _ProfitSegment = value;
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

        public string SalesDoc
        {
            get
            {
                return _SalesDoc;
            }
            set
            {
                _SalesDoc = value;
            }
        }

        public string SalesItem
        {
            get
            {
                return _SalesItem;
            }
            set
            {
                _SalesItem = value;
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



        public string SalesOrgName
        {
            get
            {
                return _SalesOrgName;
            }
            set
            {
                _SalesOrgName = value;
            }
        }

        public string DistChannel
        {
            get
            {
                return _DistChannel;
            }
            set
            {
                _DistChannel = value;
            }
        }

        public string DivisionName
        {
            get
            {
                return _DivisionName;
            }
            set
            {
                _DivisionName = value;
            }
        }


        public void SetObjectInfo(DataRow dr)
        {
            this.BillingDocCode = Convert.ToString(dr["BillingDocCode"]);
            this.ItemNo = Convert.ToInt32(dr["ItemNo"]);
            this.RefDocumentCode = Convert.ToString(dr["RefDocumentCode"]);
            this.RefItemNo = Convert.ToString(dr["RefItemNo"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.Batch = Convert.ToString(dr["Batch"]);
            this.PlantCode = Convert.ToString(dr["PlantCode"]);
            this.StoreCode = Convert.ToString(dr["StoreCode"]);
            this.SalesOrganizationCode = Convert.ToString(dr["SalesOrganizationCode"]);
            this.DistChannelCode = Convert.ToString(dr["DistChannelCode"]);
            this.DivisionCode = Convert.ToString(dr["DivisionCode"]);
            this.ShipingPointCode = Convert.ToString(dr["ShipingPointCode"]);
            this.SalesofficeCode = Convert.ToString(dr["SalesofficeCode"]);
            this.SalesGroupCode = Convert.ToString(dr["SalesGroupCode"]);
            this.ValTypeCode = Convert.ToString(dr["ValTypeCode"]);
            this.ProfitCenter = Convert.ToString(dr["ProfitCenter"]);
            this.MatVolume = Convert.ToInt32(dr["MatVolume"]);
            this.VolumeUOM = Convert.ToString(dr["VolumeUOM"]);
            this.WeightUOM = Convert.ToString(dr["WeightUOM"]);
            this.GrossWeight = Convert.ToDouble(dr["GrossWeight"]);
            this.NetWeight = Convert.ToDouble(dr["NetWeight"]);
            this.PriceDate = Convert.ToString(dr["PriceDate"]);
            this.ItemCategoryCode = Convert.ToString(dr["ItemCategoryCode"]);
            this.Quantity = Convert.ToDouble(dr["Quantity"]);
            this.BillingQty = Convert.ToDouble(dr["BillingQty"]);
            this.NetValue = Convert.ToDouble(dr["NetValue"]);
            this.TaxAmt = Convert.ToDouble(dr["TaxAmt"]);
            this.NetPricePerQty = Convert.ToDouble(dr["NetPricePerQty"]);
            this.UOMCode = Convert.ToString(dr["UOMCode"]);
            this.SalesUOMCode = Convert.ToString(dr["SalesUOMCode"]);
            this.COACode = Convert.ToString(dr["COACode"]);
            this.BusinessAreaCode = Convert.ToString(dr["BusinessAreaCode"]);
            this.ProfitSegment = Convert.ToString(dr["ProfitSegment"]);
            this.OrginatingDoc = Convert.ToString(dr["OrginatingDoc"]);
            this.OrginatingItem = Convert.ToString(dr["OrginatingItem"]);
            this.SalesDoc = Convert.ToString(dr["SalesDoc"]);
            this.SalesItem = Convert.ToString(dr["SalesItem"]);
            this.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]);
            this.ServiceRenderDate = Convert.ToString(dr["ServiceRenderDate"]);
            this.CostCenterCode = Convert.ToString(dr["CostCenterCode"]);
            this.UOMCode = Convert.ToString(dr["UOMCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

            this.MatDesc = Convert.ToString(dr["MatDesc"]).Trim();
            this.PlantName = Convert.ToString(dr["PlantName"]).Trim();
            this.StoreName = Convert.ToString(dr["StoreName"]).Trim();
            this.SalesOrgName = Convert.ToString(dr["SalesOrgName"]);
            this.DistChannel = Convert.ToString(dr["DistChannel"]);
            this.DivisionName = Convert.ToString(dr["DivisionName"]);

        }
    }
}