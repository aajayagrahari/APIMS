
//Created On :: 19, October, 2012
//Private const string ClassName = "PartnerMaterialDocType"
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
    public class PartnerMaterialDocType
    {
        private string _MaterialDocTypeCode;
        private string _MaterialDocTypeDesc;
        private string _FromPartner;
        private string _FromStore;
        private string _FromEmployee;
        private string _FromPlant;
        private string _ToPartner;
        private string _ToStore;
        private string _ToEmployee;
        private string _ToPlant;
        private string _ToMaterialCode;
        private string _AllowedFromStock;
        private string _AllowedToStock;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



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
        public string MaterialDocTypeDesc
        {
            get
            {
                return _MaterialDocTypeDesc;
            }
            set
            {
                _MaterialDocTypeDesc = value;
            }
        }
        public string FromPartner
        {
            get
            {
                return _FromPartner;
            }
            set
            {
                _FromPartner = value;
            }
        }
        public string FromStore
        {
            get
            {
                return _FromStore;
            }
            set
            {
                _FromStore = value;
            }
        }
        public string FromEmployee
        {
            get
            {
                return _FromEmployee;
            }
            set
            {
                _FromEmployee = value;
            }
        }
        public string FromPlant
        {
            get
            {
                return _FromPlant;
            }
            set
            {
                _FromPlant = value;
            }
        }
        public string ToPartner
        {
            get
            {
                return _ToPartner;
            }
            set
            {
                _ToPartner = value;
            }
        }
        public string ToStore
        {
            get
            {
                return _ToStore;
            }
            set
            {
                _ToStore = value;
            }
        }
        public string ToEmployee
        {
            get
            {
                return _ToEmployee;
            }
            set
            {
                _ToEmployee = value;
            }
        }
        public string ToPlant
        {
            get
            {
                return _ToPlant;
            }
            set
            {
                _ToPlant = value;
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
        public string AllowedFromStock
        {
            get
            {
                return _AllowedFromStock;
            }
            set
            {
                _AllowedFromStock = value;
            }
        }
        public string AllowedToStock
        {
            get
            {
                return _AllowedToStock;
            }
            set
            {
                _AllowedToStock = value;
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
            this.MaterialDocTypeCode = Convert.ToString(dr["MaterialDocTypeCode"]);
            this.MaterialDocTypeDesc = Convert.ToString(dr["MaterialDocTypeDesc"]);
            this.FromPartner = Convert.ToString(dr["FromPartner"]);
            this.FromStore = Convert.ToString(dr["FromStore"]);
            this.FromEmployee = Convert.ToString(dr["FromEmployee"]);
            this.FromPlant = Convert.ToString(dr["FromPlant"]);
            this.ToPartner = Convert.ToString(dr["ToPartner"]);
            this.ToStore = Convert.ToString(dr["ToStore"]);
            this.ToEmployee = Convert.ToString(dr["ToEmployee"]);
            this.ToPlant = Convert.ToString(dr["ToPlant"]);
            this.ToMaterialCode = Convert.ToString(dr["ToMaterialCode"]);
            this.AllowedFromStock = Convert.ToString(dr["AllowedFromStock"]);
            this.AllowedToStock = Convert.ToString(dr["AllowedToStock"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}