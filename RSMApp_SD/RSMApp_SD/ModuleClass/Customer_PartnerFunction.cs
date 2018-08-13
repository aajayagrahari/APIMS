
//Created On :: 28, July, 2012
//Private const string ClassName = "Customer_PartnerFunction"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    [Serializable]
    public class Customer_PartnerFunction
    {
        private string _CustomerCode;
        private string _SalesOrganizationCode;
        private string _DivisionCode;
        private string _DistChannelCode;
        private string _PFunctionCode;
        private int _PartnerCounter;
        private string _PartnerTable;
        private string _PartnerCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;

        private string _Name1;



        public string CustomerCode
        {
            get
            {
                return _CustomerCode;
            }
            set
            {
                _CustomerCode = value;
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
        public string DivisionCode
        {
            get
            {
                return _DivisionCode;
            }
            set
            {
                _DivisionCode = value;
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
        public string PFunctionCode
        {
            get
            {
                return _PFunctionCode;
            }
            set
            {
                _PFunctionCode = value;
            }
        }
        public int PartnerCounter
        {
            get
            {
                return _PartnerCounter;
            }
            set
            {
                _PartnerCounter = value;
            }
        }
        public string PartnerTable
        {
            get
            {
                return _PartnerTable;
            }
            set
            {
                _PartnerTable = value;
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



        public string Name1
        {
            get
            {
                return _Name1;
            }
            set
            {
                _Name1 = value;
            }
        }


        public void SetObjectInfo(DataRow dr)
        {
            this.CustomerCode = Convert.ToString(dr["CustomerCode"]);
            this.SalesOrganizationCode = Convert.ToString(dr["SalesOrganizationCode"]);
            this.DivisionCode = Convert.ToString(dr["DivisionCode"]);
            this.DistChannelCode = Convert.ToString(dr["DistChannelCode"]);
            this.PFunctionCode = Convert.ToString(dr["PFunctionCode"]);
            this.PartnerCounter = Convert.ToInt32(dr["PartnerCounter"]);
            this.PartnerTable = Convert.ToString(dr["PartnerTable"]);
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
            this.Name1 = Convert.ToString(dr["Name1"]);

        }
    }
}