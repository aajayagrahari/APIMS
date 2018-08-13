
//Created On :: 05, October, 2012
//Private const string ClassName = "AccDeterminPro"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_FI
{
    [Serializable]
    public class AccDeterminPro
    {
        private int _AccDeterminProCode;
        private string _AccDMTableCode;
        private string _CustGroupCode;
        private string _MatGroup1Code;
        private string _AccKeyCode;
        private string _SalesOrganizationCode;
        private string _DistChannelCode;
        private string _PlantCode;
        private string _AccAssignGroupCodeCust;
        private string _AccAssignGroupCodeMat;
        private string _SOTypeCode;
        private string _DeliveryDocTypeCode;
        private string _BillingDocTypeCode;
        private string _AccountDocTypeCode;
        private string _PlantStateCode;
        private string _ChartACCode;
        private string _GLCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public int AccDeterminProCode
        {
            get
            {
                return _AccDeterminProCode;
            }
            set
            {
                _AccDeterminProCode = value;
            }
        }
        public string AccDMTableCode
        {
            get
            {
                return _AccDMTableCode;
            }
            set
            {
                _AccDMTableCode = value;
            }
        }
        public string CustGroupCode
        {
            get
            {
                return _CustGroupCode;
            }
            set
            {
                _CustGroupCode = value;
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
        public string AccKeyCode
        {
            get
            {
                return _AccKeyCode;
            }
            set
            {
                _AccKeyCode = value;
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
        public string AccAssignGroupCodeCust
        {
            get
            {
                return _AccAssignGroupCodeCust;
            }
            set
            {
                _AccAssignGroupCodeCust = value;
            }
        }
        public string AccAssignGroupCodeMat
        {
            get
            {
                return _AccAssignGroupCodeMat;
            }
            set
            {
                _AccAssignGroupCodeMat = value;
            }
        }
        public string SOTypeCode
        {
            get
            {
                return _SOTypeCode;
            }
            set
            {
                _SOTypeCode = value;
            }
        }
        public string DeliveryDocTypeCode
        {
            get
            {
                return _DeliveryDocTypeCode;
            }
            set
            {
                _DeliveryDocTypeCode = value;
            }
        }
        public string BillingDocTypeCode
        {
            get
            {
                return _BillingDocTypeCode;
            }
            set
            {
                _BillingDocTypeCode = value;
            }
        }
        public string AccountDocTypeCode
        {
            get
            {
                return _AccountDocTypeCode;
            }
            set
            {
                _AccountDocTypeCode = value;
            }
        }
        public string PlantStateCode
        {
            get
            {
                return _PlantStateCode;
            }
            set
            {
                _PlantStateCode = value;
            }
        }
        public string ChartACCode
        {
            get
            {
                return _ChartACCode;
            }
            set
            {
                _ChartACCode = value;
            }
        }
        public string GLCode
        {
            get
            {
                return _GLCode;
            }
            set
            {
                _GLCode = value;
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
            this.AccDeterminProCode = Convert.ToInt32(dr["AccDeterminProCode"]);
            this.AccDMTableCode = Convert.ToString(dr["AccDMTableCode"]);
            this.CustGroupCode = Convert.ToString(dr["CustGroupCode"]);
            this.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]);
            this.AccKeyCode = Convert.ToString(dr["AccKeyCode"]);
            this.SalesOrganizationCode = Convert.ToString(dr["SalesOrganizationCode"]);
            this.DistChannelCode = Convert.ToString(dr["DistChannelCode"]);
            this.PlantCode = Convert.ToString(dr["PlantCode"]);
            this.AccAssignGroupCodeCust = Convert.ToString(dr["AccAssignGroupCodeCust"]);
            this.AccAssignGroupCodeMat = Convert.ToString(dr["AccAssignGroupCodeMat"]);
            this.SOTypeCode = Convert.ToString(dr["SOTypeCode"]);
            this.DeliveryDocTypeCode = Convert.ToString(dr["DeliveryDocTypeCode"]);
            this.BillingDocTypeCode = Convert.ToString(dr["BillingDocTypeCode"]);
            this.AccountDocTypeCode = Convert.ToString(dr["AccountDocTypeCode"]);
            this.PlantStateCode = Convert.ToString(dr["PlantStateCode"]);
            this.ChartACCode = Convert.ToString(dr["ChartACCode"]);
            this.GLCode = Convert.ToString(dr["GLCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}