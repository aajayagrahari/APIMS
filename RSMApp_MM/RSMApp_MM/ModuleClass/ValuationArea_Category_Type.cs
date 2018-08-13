
//Created On :: 12, September, 2012
//Private const string ClassName = "ValuationArea_Category_Type"
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
    public class ValuationArea_Category_Type
    {
        private string _PlantCode;
        private string _ValCatCode;
        private string _ValTypeCode;
        private string _ValAreaCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



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
        public string ValCatCode
        {
            get
            {
                return _ValCatCode;
            }
            set
            {
                _ValCatCode = value;
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
        public string ValAreaCode
        {
            get
            {
                return _ValAreaCode;
            }
            set
            {
                _ValAreaCode = value;
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
            this.PlantCode = Convert.ToString(dr["PlantCode"]);
            this.ValCatCode = Convert.ToString(dr["ValCatCode"]);
            this.ValTypeCode = Convert.ToString(dr["ValTypeCode"]);
            this.ValAreaCode = Convert.ToString(dr["ValAreaCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}