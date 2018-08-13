
//Created On :: 30, October, 2012
//Private const string ClassName = "PartnerGoodsMovement"
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
    public class PartnerGoodsMovement
    {
        private string _PGoodsMovementCode;
        private string _PartnerGMDocTypeCode;
        private string _FromPlantCode;
        private string _FromPartnerCode;
        private string _FromStoreCode;
        private string _FromPartnerEmployeeCode;
        private string _ToPlantCode;
        private string _ToPartnerCode;
        private string _ToStoreCode;
        private string _ToPartnerEmployeeCode;
        private int _TotalQuantity;
        private DateTime _GoodsMovDate;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;

        private string _PartnerCode;

        private string _AssignType;

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

        public string PartnerGMDocTypeCode
        {
            get
            {
                return _PartnerGMDocTypeCode;
            }
            set
            {
                _PartnerGMDocTypeCode = value;
            }
        }

        public string FromPlantCode
        {
            get
            {
                return _FromPlantCode;
            }
            set
            {
                _FromPlantCode = value;
            }
        }
        public string FromPartnerCode
        {
            get
            {
                return _FromPartnerCode;
            }
            set
            {
                _FromPartnerCode = value;
            }
        }
        public string FromStoreCode
        {
            get
            {
                return _FromStoreCode;
            }
            set
            {
                _FromStoreCode = value;
            }
        }
        public string FromPartnerEmployeeCode
        {
            get
            {
                return _FromPartnerEmployeeCode;
            }
            set
            {
                _FromPartnerEmployeeCode = value;
            }
        }
        public string ToPlantCode
        {
            get
            {
                return _ToPlantCode;
            }
            set
            {
                _ToPlantCode = value;
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
        public int TotalQuantity
        {
            get
            {
                return _TotalQuantity;
            }
            set
            {
                _TotalQuantity = value;
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

        public DateTime GoodsMovDate
        {
            get
            {
                return _GoodsMovDate;
            }
            set
            {
                _GoodsMovDate = value;
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

        public string AssignType
        {
            get
            {
                return _AssignType;
            }
            set
            {
                _AssignType = value;
            }
        }




        public void SetObjectInfo(DataRow dr)
        {
            this.PGoodsMovementCode = Convert.ToString(dr["PGoodsMovementCode"]);
            this.PartnerGMDocTypeCode = Convert.ToString(dr["PartnerGMDocTypeCode"]);
            this.FromPlantCode = Convert.ToString(dr["FromPlantCode"]);
            this.FromPartnerCode = Convert.ToString(dr["FromPartnerCode"]);
            this.FromStoreCode = Convert.ToString(dr["FromStoreCode"]);
            this.FromPartnerEmployeeCode = Convert.ToString(dr["FromPartnerEmployeeCode"]);
            this.ToPlantCode = Convert.ToString(dr["ToPlantCode"]);
            this.ToPartnerCode = Convert.ToString(dr["ToPartnerCode"]);
            this.ToStoreCode = Convert.ToString(dr["ToStoreCode"]);
            this.ToPartnerEmployeeCode = Convert.ToString(dr["ToPartnerEmployeeCode"]);
            this.TotalQuantity = Convert.ToInt32(dr["TotalQuantity"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.GoodsMovDate = Convert.ToDateTime(dr["GoodsMovDate"]);
        }
    }
}