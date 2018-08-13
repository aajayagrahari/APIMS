
//Created On :: 10, September, 2012
//Private const string ClassName = "MaterialBatch"
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
    public class MaterialBatch
    {
        private string _MaterialCode;
        private string _PlantCode;
        private string _BatchNumber;
        private string _ExpiryDate;
        private string _AvailabilityDate;
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

        public string BatchNumber
        {
            get
            {
                return _BatchNumber;
            }
            set
            {
                _BatchNumber = value;
            }
        }

        public string ExpiryDate
        {
            get
            {
                return _ExpiryDate;
            }
            set
            {
                _ExpiryDate = value;
            }
        }

        public string AvailabilityDate
        {
            get
            {
                return _AvailabilityDate;
            }
            set
            {
                _AvailabilityDate = value;
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
            this.BatchNumber = Convert.ToString(dr["BatchNumber"]);
            this.ExpiryDate = Convert.ToString(dr["ExpiryDate"]);
            this.AvailabilityDate = Convert.ToString(dr["AvailabilityDate"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
        }
    }
}