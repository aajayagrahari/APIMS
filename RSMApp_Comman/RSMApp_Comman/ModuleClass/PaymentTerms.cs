
//Created On :: 03, October, 2012
//Private const string ClassName = "PaymentTerms"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Comman
{
    [Serializable]
    public class PaymentTerms
    {
        private string _PaymentTermsCode;
        private string _PaymentTermsDesc1;
        private string _PaymentTermsDesc2;
        private string _PaymentTermsDesc3;
        private string _PaymentTermsDesc4;
        private string _PaymentTermsDesc5;
        private string _DayLimit;
        private string _ClientCode;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string PaymentTermsCode
        {
            get
            {
                return _PaymentTermsCode;
            }
            set
            {
                _PaymentTermsCode = value;
            }
        }
        public string PaymentTermsDesc1
        {
            get
            {
                return _PaymentTermsDesc1;
            }
            set
            {
                _PaymentTermsDesc1 = value;
            }
        }
        public string PaymentTermsDesc2
        {
            get
            {
                return _PaymentTermsDesc2;
            }
            set
            {
                _PaymentTermsDesc2 = value;
            }
        }
        public string PaymentTermsDesc3
        {
            get
            {
                return _PaymentTermsDesc3;
            }
            set
            {
                _PaymentTermsDesc3 = value;
            }
        }
        public string PaymentTermsDesc4
        {
            get
            {
                return _PaymentTermsDesc4;
            }
            set
            {
                _PaymentTermsDesc4 = value;
            }
        }
        public string PaymentTermsDesc5
        {
            get
            {
                return _PaymentTermsDesc5;
            }
            set
            {
                _PaymentTermsDesc5 = value;
            }
        }
        public string DayLimit
        {
            get
            {
                return _DayLimit;
            }
            set
            {
                _DayLimit = value;
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
            this.PaymentTermsCode = Convert.ToString(dr["PaymentTermsCode"]);
            this.PaymentTermsDesc1 = Convert.ToString(dr["PaymentTermsDesc1"]);
            this.PaymentTermsDesc2 = Convert.ToString(dr["PaymentTermsDesc2"]);
            this.PaymentTermsDesc3 = Convert.ToString(dr["PaymentTermsDesc3"]);
            this.PaymentTermsDesc4 = Convert.ToString(dr["PaymentTermsDesc4"]);
            this.PaymentTermsDesc5 = Convert.ToString(dr["PaymentTermsDesc5"]);
            this.DayLimit = Convert.ToString(dr["DayLimit"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}