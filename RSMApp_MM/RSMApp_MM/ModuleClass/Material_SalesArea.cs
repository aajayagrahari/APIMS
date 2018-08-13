
//Created On :: 20, May, 2012
//Private const string ClassName = "Material_SalesArea"
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
    public class Material_SalesArea
    {
        private string _MaterialCode;
        private string _SalesOrganizationCode;
        private string _DistChannelCode;
        private int _MinOrderQty;
        private int _MinDeliveryQty;
        private string _SalesUOMCode;
        private string _DeliveringPlantCode;
        private string _MaterialHierarchy;
        private string _PFMaterialCode;
        private int _IsVariableSalesUOM;
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

        public int MinOrderQty
        {
            get
            {
                return _MinOrderQty;
            }
            set
            {
                _MinOrderQty = value;
            }
        }

        public int MinDeliveryQty
        {
            get
            {
                return _MinDeliveryQty;
            }
            set
            {
                _MinDeliveryQty = value;
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
      
        public string DeliveringPlantCode
        {
            get
            {
                return _DeliveringPlantCode;
            }
            set
            {
                _DeliveringPlantCode = value;
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

        public string PFMaterialCode
        {
            get
            {
                return _PFMaterialCode;
            }
            set
            {
                _PFMaterialCode = value;
            }
        }

    
        public int IsVariableSalesUOM
        {
            get
            {
                return _IsVariableSalesUOM;
            }
            set
            {
                _IsVariableSalesUOM = value;
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
            this.SalesOrganizationCode = Convert.ToString(dr["SalesOrganizationCode"]);
            this.DistChannelCode = Convert.ToString(dr["DistChannelCode"]);
            this.MinOrderQty = Convert.ToInt32(dr["MinOrderQty"]);
            this.MinDeliveryQty = Convert.ToInt32(dr["MinDeliveryQty"]);
            this.SalesUOMCode = Convert.ToString(dr["SalesUOMCode"]);
            this.DeliveringPlantCode = Convert.ToString(dr["DeliveringPlantCode"]);
            this.MaterialHierarchy = Convert.ToString(dr["MaterialHierarchy"]);
            this.PFMaterialCode = Convert.ToString(dr["PFMaterialCode"]);
            this.IsVariableSalesUOM = Convert.ToInt32(dr["IsVariableSalesUOM"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}