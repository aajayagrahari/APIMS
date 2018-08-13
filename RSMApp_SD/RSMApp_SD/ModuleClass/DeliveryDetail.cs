
//Created On :: 19, July, 2012
//Private const string ClassName = "DeliveryDetail"
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
    public class DeliveryDetail
    {
        private string _DeliveryDocCode;
        private string _ItemNo;
        private string _SODocCode;
        private string _SOItemNo;
        private string _MaterialCode;
        private string _Batch;
        private string _PlantCode;
        private string _StoreCode;
        private string _ItemCategoryCode;
        private string _MatMovementCode;
        private double _Quantity;
        private double _OrderQty;
        private double _PriceUnit;
        private string _UOMCode;
        private string _OrderUOMCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;
        private string _MatDesc;
        private string _PlantName;
        private string _StoreName;
        private string _ItemCategoryDesc;
        private string _MatMovementDesc;
        private double _GrossWeight;
        private double _NetWeight;

        private int _IsSerialize;

        private string _ItemCatGroupCode;
        private string _SalesOrganizationCode;
        private string _DistChannelCode;
        private string _DivisionCode;

        private string _InetrnalUOM;

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
        public string SOItemNo
        {
            get
            {
                return _SOItemNo;
            }
            set
            {
                _SOItemNo = value;
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
        public double PriceUnit
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

        public string InternalUOM
        {
            get
            {
                return _InetrnalUOM;
            }
            set
            {
                _InetrnalUOM = value;
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

        public string MatMovementDesc
        {
            get
            {
                return _MatMovementDesc;
            }
            set
            {
                _MatMovementDesc = value;
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

        public string ItemCatGroupCode
        {
            get
            {
                return _ItemCatGroupCode;
            }
            set
            {
                _ItemCatGroupCode = value;
            }
        }


        public double OrderQty
        {
            get
            {
                return _OrderQty;
            }
            set
            {
                _OrderQty = value;
            }
        }

        public string OrderUOMCode
        {
            get
            {
                return _OrderUOMCode;
            }
            set
            {
                _OrderUOMCode = value;
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

        public void SetObjectInfo(DataRow dr)
        {
            this.DeliveryDocCode = Convert.ToString(dr["DeliveryDocCode"]);
            this.ItemNo = Convert.ToString(dr["ItemNo"]);
            this.SODocCode = Convert.ToString(dr["SODocCode"]);
            this.SOItemNo = Convert.ToString(dr["SOItemNo"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.Batch = Convert.ToString(dr["Batch"]);
            this.PlantCode = Convert.ToString(dr["PlantCode"]);
            this.StoreCode = Convert.ToString(dr["StoreCode"]);
            this.ItemCategoryCode = Convert.ToString(dr["ItemCategoryCode"]);
            this.MatMovementCode = Convert.ToString(dr["MatMovementCode"]);
            this.Quantity = Convert.ToDouble(dr["Quantity"]);
            this.OrderQty = Convert.ToDouble(dr["OrderQty"]);
            this.PriceUnit = Convert.ToDouble(dr["PriceUnit"]);
            this.UOMCode = Convert.ToString(dr["UOMCode"]);
            this.OrderUOMCode = Convert.ToString(dr["OrderUOMCode"]);
            this.NetWeight = Convert.ToDouble(dr["NetWeight"]);
            this.GrossWeight = Convert.ToDouble(dr["GrossWeight"]);  
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

            this.MatDesc = Convert.ToString(dr["MatDesc"]).Trim();
            this.PlantName = Convert.ToString(dr["PlantName"]).Trim();
            this.StoreName = Convert.ToString(dr["StoreName"]).Trim();
            this.ItemCategoryDesc = Convert.ToString(dr["ItemCategoryDesc"]).Trim();
            this.MatMovementDesc = Convert.ToString(dr["MatMovementDesc"]).Trim();
            this.IsSerialize = Convert.ToInt32(dr["IsSerialize"]);
            this.ItemCatGroupCode = Convert.ToString(dr["ItemCatGroupCode"]);
            this.SalesOrganizationCode = Convert.ToString(dr["SalesOrganizationCode"]);
            this.DistChannelCode = Convert.ToString(dr["DistChannelCode"]);
            this.DivisionCode = Convert.ToString(dr["DivisionCode"]);
            this.InternalUOM = Convert.ToString(dr["InternalUOM"]);
        }
    }
}