
//Created On :: 15, December, 2012
//Private const string ClassName = "SerializeStockMissingParts"
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
    public class SerializeStockMissingParts
    {
        private string _SerialNo;
        private string _MastMaterialCode;
        private string _MaterialCode;
        private string _MatDesc;
        private string _MatGroup1Code;
        private string _PartnerCode;
        private string _PlantCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private string _DocType;
        private int _IsDeleted;



        public string SerialNo
        {
            get
            {
                return _SerialNo;
            }
            set
            {
                _SerialNo = value;
            }
        }
        public string MastMaterialCode
        {
            get
            {
                return _MastMaterialCode;
            }
            set
            {
                _MastMaterialCode = value;
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

        public string DocType
        {
            get
            {
                return _DocType;
            }
            set
            {
                _DocType = value;
            }

        }


        public void SetObjectInfo(DataRow dr)
        {
            this.SerialNo = Convert.ToString(dr["SerialNo"]);
            this.MastMaterialCode = Convert.ToString(dr["MastMaterialCode"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.MatDesc = Convert.ToString(dr["MatDesc"]);
            this.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]);
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.PlantCode = Convert.ToString(dr["PlantCode"]);
            this.DocType = Convert.ToString(dr["DocType"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}