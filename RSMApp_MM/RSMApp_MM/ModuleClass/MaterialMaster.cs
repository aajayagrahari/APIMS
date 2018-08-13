
//Created On :: 17, May, 2012
//Private const string ClassName = "MaterialMaster"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_MM
{
    [Serializable]
    public class MaterialMaster
    {
        private string _MaterialCode;
        private string _MatDesc;
        private string _MaterialTypeCode;
        private string _ExtMatGroupCode;
        private string _MatGroup1Code;
        private string _MatGroup2Code;
        private string _MatGroup3Code;
        private string _SalesOrganizationCode;
        private string _DistChannelCode;
        private string _DivisionCode;
        private string _ValClassType;
        private string _ItemCatGroupCode;
        private string _PriceControl;
        private string _UOMCode;
        private int _PriceUnit;
        private int _PurchaseUnit;
        private int _IsSerialize;
        private int _IsBOM;
        private int _GrossWeight;
        private int _NetWeight;
        private int _MatSize;
        private string _WeightUOM;
        private int _MatVolume;
        private int _IsBatchWise;
        private string _VolumeUOM;
        private string _MaterialHierarchy;
        private string _OldMaterialCode;
        private string _SNProfileCode;
        private int _IsBOMExplodeApp;
        private string _BaseWarrantyOn;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;

       



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
        public string ExtMatGroupCode
        {
            get
            {
                return _ExtMatGroupCode;
            }
            set
            {
                _ExtMatGroupCode = value;
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
        public string MatGroup3Code
        {
            get
            {
                return _MatGroup3Code;
            }
            set
            {
                _MatGroup3Code = value;
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
        public string PriceControl
        {
            get
            {
                return _PriceControl;
            }
            set
            {
                _PriceControl = value;
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

        public int PurchaseUnit
        {
            get
            {
                return _PurchaseUnit;
            }
            set
            {
                _PurchaseUnit = value;
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
        public int IsBOM
        {
            get
            {
                return _IsBOM;
            }
            set
            {
                _IsBOM = value;
            }
        }

        public int IsBatchWise
        {
            get
            {
                return _IsBatchWise;
            }
            set
            {
                _IsBatchWise = value;
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
        public int MatSize
        {
            get
            {
                return _MatSize;
            }
            set
            {
                _MatSize = value;
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

        public string  VolumeUOM
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

        public string MaterialHierarchy
        {
            get
            {
                return _MaterialHierarchy;
            }
            set
            {
                _MaterialHierarchy = value;
            }
        }

        public string OldMaterialCode
        {
            get
            {
                return _OldMaterialCode;
            }
            set
            {
                _OldMaterialCode = value;
            }
        }

        public string SNProfileCode
        {
            get
            {
                return _SNProfileCode;
            }
            set
            {
                _SNProfileCode = value;
            }
        }

        public int IsBOMExplodeApp
        {
            get
            {
                return _IsBOMExplodeApp;
            }
            set
            {
                _IsBOMExplodeApp = value;
            }
        }

        public string BaseWarrantyOn
        {
            get
            {
                return _BaseWarrantyOn;
            }
            set
            {
                _BaseWarrantyOn = value;
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
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.MatDesc = Convert.ToString(dr["MatDesc"]);
            this.MaterialTypeCode = Convert.ToString(dr["MaterialTypeCode"]);
            this.ExtMatGroupCode = Convert.ToString(dr["ExtMatGroupCode"]);
            this.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]);
            this.MatGroup2Code = Convert.ToString(dr["MatGroup2Code"]);
            this.MatGroup3Code = Convert.ToString(dr["MatGroup3Code"]);
            this.DivisionCode = Convert.ToString(dr["DivisionCode"]);
            this.ValClassType = Convert.ToString(dr["ValClassType"]);
            this.ItemCatGroupCode = Convert.ToString(dr["ItemCatGroupCode"]);
            this.PriceControl = Convert.ToString(dr["PriceControl"]);
            this.UOMCode = Convert.ToString(dr["UOMCode"]);
            this.PriceUnit = Convert.ToInt32(dr["PriceUnit"]);
            this.PurchaseUnit=Convert.ToInt32(dr["PurchaseUnit"]);
            this.IsSerialize = Convert.ToInt32(dr["IsSerialize"]);
            this.IsBOM = Convert.ToInt32(dr["IsBOM"]);
            this.GrossWeight = Convert.ToInt32(dr["GrossWeight"]);
            this.NetWeight = Convert.ToInt32(dr["NetWeight"]);
            this.MatSize = Convert.ToInt32(dr["MatSize"]);
            this.WeightUOM = Convert.ToString(dr["WeightUOM"]);
            this.MatVolume = Convert.ToInt32(dr["MatVolume"]);
            this.IsBatchWise = Convert.ToInt32(dr["IsBatchWise"]);
            this.VolumeUOM = Convert.ToString(dr["VolumeUOM"]);
            this.MaterialHierarchy = Convert.ToString(dr["MaterialHierarchy"]);
            this.OldMaterialCode = Convert.ToString(dr["OldMaterialCode"]);
            this.SNProfileCode = Convert.ToString(dr["SNProfileCode"]);
            this.IsBOMExplodeApp = Convert.ToInt32(dr["IsBOMExplodeApp"]);
            this.BaseWarrantyOn = Convert.ToString(dr["BaseWarrantyOn"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}