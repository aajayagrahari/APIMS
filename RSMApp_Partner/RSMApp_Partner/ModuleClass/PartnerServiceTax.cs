
//Created On :: 29, November, 2012
//Private const string ClassName = "PartnerServiceTax"
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
    public class PartnerServiceTax
    {
        private int _ServiceTaxID;
        private double _ServiceTaxPer;
        private string _ValidFrom;
        private string _ValidTo;
        private string _ProcedureType;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public int ServiceTaxID
        {
            get
            {
                return _ServiceTaxID;
            }
            set
            {
                _ServiceTaxID = value;
            }
        }
        public double ServiceTaxPer
        {
            get
            {
                return _ServiceTaxPer;
            }
            set
            {
                _ServiceTaxPer = value;
            }
        }
        public string ValidFrom
        {
            get
            {
                return _ValidFrom;
            }
            set
            {
                _ValidFrom = value;
            }
        }
        public string ValidTo
        {
            get
            {
                return _ValidTo;
            }
            set
            {
                _ValidTo = value;
            }
        }

        public string ProcedureType
        {
            get
            {
                return _ProcedureType;
            }
            set
            {
                _ProcedureType = value;
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
            this.ServiceTaxID = Convert.ToInt32(dr["ServiceTaxID"]);
            this.ServiceTaxPer = Convert.ToInt32(dr["ServiceTaxPer"]);
            this.ValidFrom = Convert.ToString(dr["ValidFrom"]);
            this.ValidTo = Convert.ToString(dr["ValidTo"]);
            this.ProcedureType = Convert.ToString(dr["ProcedureType"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}