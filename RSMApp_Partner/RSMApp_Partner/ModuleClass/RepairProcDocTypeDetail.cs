
//Created On :: 26, November, 2012
//Private const string ClassName = "RepairProcDocTypeDetail"
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
    public class RepairProcDocTypeDetail
    {
        private string _RepairProcDocTypeCode;
        private string _RepairProcType;
        private string _RepairProcessCode;
        private string _MatMaterialDocTypeCode;
        private string _MatDefaultStoreCode;
        private string _MatDefaultStockIndicator;
        private string _SpareMaterialDocTypeCode;
        private string _SpareDefaultStoreCode;
        private string _SpareDefaultStockIndicator;
        private string _DefectMaterialDocTypeCode;
        private string _DefectDefaultStoreCode;
        private string _DefectDefaultStockIndicator;
        private int _IsCallCloseApp;
        private int _IsRepairCloseApp;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string RepairProcDocTypeCode
        {
            get
            {
                return _RepairProcDocTypeCode;
            }
            set
            {
                _RepairProcDocTypeCode = value;
            }
        }
        public string RepairProcType
        {
            get
            {
                return _RepairProcType;
            }
            set
            {
                _RepairProcType = value;
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
        public string MatMaterialDocTypeCode
        {
            get
            {
                return _MatMaterialDocTypeCode;
            }
            set
            {
                _MatMaterialDocTypeCode = value;
            }
        }
        public string MatDefaultStoreCode
        {
            get
            {
                return _MatDefaultStoreCode;
            }
            set
            {
                _MatDefaultStoreCode = value;
            }
        }
        public string MatDefaultStockIndicator
        {
            get
            {
                return _MatDefaultStockIndicator;
            }
            set
            {
                _MatDefaultStockIndicator = value;
            }
        }
        public string SpareMaterialDocTypeCode
        {
            get
            {
                return _SpareMaterialDocTypeCode;
            }
            set
            {
                _SpareMaterialDocTypeCode = value;
            }
        }
        public string SpareDefaultStoreCode
        {
            get
            {
                return _SpareDefaultStoreCode;
            }
            set
            {
                _SpareDefaultStoreCode = value;
            }
        }
        public string SpareDefaultStockIndicator
        {
            get
            {
                return _SpareDefaultStockIndicator;
            }
            set
            {
                _SpareDefaultStockIndicator = value;
            }
        }
        public string DefectMaterialDocTypeCode
        {
            get
            {
                return _DefectMaterialDocTypeCode;
            }
            set
            {
                _DefectMaterialDocTypeCode = value;
            }
        }
        public string DefectDefaultStoreCode
        {
            get
            {
                return _DefectDefaultStoreCode;
            }
            set
            {
                _DefectDefaultStoreCode = value;
            }
        }
        public string DefectDefaultStockIndicator
        {
            get
            {
                return _DefectDefaultStockIndicator;
            }
            set
            {
                _DefectDefaultStockIndicator = value;
            }
        }

        public int IsCallCloseApp
        {
            get
            {
                return _IsCallCloseApp;
            }
            set
            {
                _IsCallCloseApp = value;
            }
        }

        public int IsRepairCloseApp
        {
            get
            {
                return _IsRepairCloseApp;
            }
            set
            {
                _IsRepairCloseApp = value;
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
            this.RepairProcDocTypeCode = Convert.ToString(dr["RepairProcDocTypeCode"]);
            this.RepairProcType = Convert.ToString(dr["RepairProcType"]);
            this.RepairProcessCode = Convert.ToString(dr["RepairProcessCode"]);
            this.MatMaterialDocTypeCode = Convert.ToString(dr["MatMaterialDocTypeCode"]);
            this.MatDefaultStoreCode = Convert.ToString(dr["MatDefaultStoreCode"]);
            this.MatDefaultStockIndicator = Convert.ToString(dr["MatDefaultStockIndicator"]);
            this.SpareMaterialDocTypeCode = Convert.ToString(dr["SpareMaterialDocTypeCode"]);
            this.SpareDefaultStoreCode = Convert.ToString(dr["SpareDefaultStoreCode"]);
            this.SpareDefaultStockIndicator = Convert.ToString(dr["SpareDefaultStockIndicator"]);
            this.DefectMaterialDocTypeCode = Convert.ToString(dr["DefectMaterialDocTypeCode"]);
            this.DefectDefaultStoreCode = Convert.ToString(dr["DefectDefaultStoreCode"]);
            this.DefectDefaultStockIndicator = Convert.ToString(dr["DefectDefaultStockIndicator"]);
            this.IsCallCloseApp = Convert.ToInt32(dr["IsCallCloseApp"]);
            this.IsRepairCloseApp = Convert.ToInt32(dr["IsRepairCloseApp"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}