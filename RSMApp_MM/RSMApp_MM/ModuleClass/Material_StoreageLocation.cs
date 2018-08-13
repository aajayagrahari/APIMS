
//Created On :: 17, May, 2012
//Private const string ClassName = "Material_StoreageLocation"
// Updated By Ashutosh Agrahari 
// Updated Date 29/05/2012
// Batch Column Added.
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
    public class Material_StoreageLocation
    {
        private string _MaterialCode;
        private string _PlantCode;
        private string _StoreCode;
        private int _UnrestrictedStock;
        private int _RestrictedStock;
        private int _InQltyInspection;
        private int _Blocked;
        private int _StockInTransit;
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
        public int UnrestrictedStock
        {
            get
            {
                return _UnrestrictedStock;
            }
            set
            {
                _UnrestrictedStock = value;
            }
        }
        public int RestrictedStock
        {
            get
            {
                return _RestrictedStock;
            }
            set
            {
                _RestrictedStock = value;
            }
        }
        public int InQltyInspection
        {
            get
            {
                return _InQltyInspection;
            }
            set
            {
                _InQltyInspection = value;
            }
        }
        public int Blocked
        {
            get
            {
                return _Blocked;
            }
            set
            {
                _Blocked = value;
            }
        }
        public int StockInTransit
        {
            get
            {
                return _StockInTransit;
            }
            set
            {
                _StockInTransit = value;
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
            this.PlantCode = Convert.ToString(dr["PlantCode"]);
            this.StoreCode = Convert.ToString(dr["StoreCode"]);
            this.UnrestrictedStock = Convert.ToInt32(dr["UnrestrictedStock"]);
            this.RestrictedStock = Convert.ToInt32(dr["RestrictedStock"]);
            this.InQltyInspection = Convert.ToInt32(dr["InQltyInspection"]);
            this.Blocked = Convert.ToInt32(dr["Blocked"]);
            this.StockInTransit = Convert.ToInt32(dr["StockInTransit"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}