
//Created On :: 01, November, 2012
//Private const string ClassName = "AsgTechnicianDocType"
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
    public class AsgTechnicianDocType
    {
        private string _AsgTechDocTypeCode;
        private string _AsgTechDocTypeDesc;
        private int _ItemNoIncr;
        private string _NumRange;
        private string _AssignType;
        private string _MaterialDocTypeCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;


        private string _RepairProcessCode;
        public string _DefaultStoreCode;
        public string _DefaultFromStockIndicator;
        public string _DefaultToStockIndicator;
        


        public string AsgTechDocTypeCode
        {
            get
            {
                return _AsgTechDocTypeCode;
            }
            set
            {
                _AsgTechDocTypeCode = value;
            }
        }
        public string AsgTechDocTypeDesc
        {
            get
            {
                return _AsgTechDocTypeDesc;
            }
            set
            {
                _AsgTechDocTypeDesc = value;
            }
        }
        public int ItemNoIncr
        {
            get
            {
                return _ItemNoIncr;
            }
            set
            {
                _ItemNoIncr = value;
            }
        }
        public string NumRange
        {
            get
            {
                return _NumRange;
            }
            set
            {
                _NumRange = value;
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
        public string RepairProcessCode
        {
            get
            {
                return _RepairProcessCode;
            }
            set
            {
                _RepairProcessCode = value;
            }
        }

        public string DefaultStoreCode
        {
            get
            {
                return _DefaultStoreCode;
            }
            set
            {
                _DefaultStoreCode = value;
            }
        }

        public string DefaultFromStockIndicator
        {
            get
            {
                return _DefaultFromStockIndicator;
            }
            set
            {
                _DefaultFromStockIndicator = value;
            }
        }

        public string DefaultToStockIndicator
        {
            get
            {
                return _DefaultToStockIndicator;
            }
            set
            {
                _DefaultToStockIndicator = value;
            }
        }
        

        public void SetObjectInfo(DataRow dr)
        {
            this.AsgTechDocTypeCode = Convert.ToString(dr["AsgTechDocTypeCode"]);
            this.AsgTechDocTypeDesc = Convert.ToString(dr["AsgTechDocTypeDesc"]);
            this.ItemNoIncr = Convert.ToInt32(dr["ItemNoIncr"]);
            this.NumRange = Convert.ToString(dr["NumRange"]);
            this.AssignType = Convert.ToString(dr["AssignType"]);
            this.MaterialDocTypeCode = Convert.ToString(dr["MaterialDocTypeCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

            this.RepairProcessCode = Convert.ToString(dr["RepairProcessCode"]);
            this.DefaultStoreCode = Convert.ToString(dr["DefaultStoreCode"]);
            this.DefaultFromStockIndicator = Convert.ToString(dr["DefaultFromStockIndicator"]);
            this.DefaultToStockIndicator = Convert.ToString(dr["DefaultToStockIndicator"]);

        }
    }
}