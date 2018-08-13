
//Created On :: 28, September, 2012
//Private const string ClassName = "PaymentMaster"
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
    public class PaymentMaster
    {
        private string _PaymentDocCode;
        private string _AccountDocTypeCode;
        private string _DocumentDate;
        private string _PostingDate;
        private string _PostingPeriod;
        private string _CompanyCode;
        private string _BusinessAreaCode;
        private string _ProfitCenterCode;
        private string _BankAccount;
        private string _SpecialGLIndCode;
        private string _CurrencyCode;
        private string _Reference;
        private string _ValueDate;
        private string _AccountType;
        private string _CustomerCode;
        private int _Amount;
        private int _AmountInLc;
        private string _DocHeaderText;
        private string _ClearingText;
        private string _TradingPartnerBA;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;
        private int _IsCancel;



        public string PaymentDocCode
        {
            get
            {
                return _PaymentDocCode;
            }
            set
            {
                _PaymentDocCode = value;
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
        public string DocumentDate
        {
            get
            {
                return _DocumentDate;
            }
            set
            {
                _DocumentDate = value;
            }
        }
        public string PostingDate
        {
            get
            {
                return _PostingDate;
            }
            set
            {
                _PostingDate = value;
            }
        }
        public string PostingPeriod
        {
            get
            {
                return _PostingPeriod;
            }
            set
            {
                _PostingPeriod = value;
            }
        }
        public string CompanyCode
        {
            get
            {
                return _CompanyCode;
            }
            set
            {
                _CompanyCode = value;
            }
        }
        public string BusinessAreaCode
        {
            get
            {
                return _BusinessAreaCode;
            }
            set
            {
                _BusinessAreaCode = value;
            }
        }
        public string ProfitCenterCode
        {
            get
            {
                return _ProfitCenterCode;
            }
            set
            {
                _ProfitCenterCode = value;
            }
        }
        public string BankAccount
        {
            get
            {
                return _BankAccount;
            }
            set
            {
                _BankAccount = value;
            }
        }
        public string SpecialGLIndCode
        {
            get
            {
                return _SpecialGLIndCode;
            }
            set
            {
                _SpecialGLIndCode = value;
            }
        }
        public string CurrencyCode
        {
            get
            {
                return _CurrencyCode;
            }
            set
            {
                _CurrencyCode = value;
            }
        }
        public string Reference
        {
            get
            {
                return _Reference;
            }
            set
            {
                _Reference = value;
            }
        }
        public string ValueDate
        {
            get
            {
                return _ValueDate;
            }
            set
            {
                _ValueDate = value;
            }
        }
        public string AccountType
        {
            get
            {
                return _AccountType;
            }
            set
            {
                _AccountType = value;
            }
        }
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
        public int Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                _Amount = value;
            }
        }
        public int AmountInLc
        {
            get
            {
                return _AmountInLc;
            }
            set
            {
                _AmountInLc = value;
            }
        }
        public string DocHeaderText
        {
            get
            {
                return _DocHeaderText;
            }
            set
            {
                _DocHeaderText = value;
            }
        }
        public string ClearingText
        {
            get
            {
                return _ClearingText;
            }
            set
            {
                _ClearingText = value;
            }
        }
        public string TradingPartnerBA
        {
            get
            {
                return _TradingPartnerBA;
            }
            set
            {
                _TradingPartnerBA = value;
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
        public int IsCancel
        {
            get
            {
                return _IsCancel;
            }
            set
            {
                _IsCancel = value;
            }
        }


        public void SetObjectInfo(DataRow dr)
        {
            this.PaymentDocCode = Convert.ToString(dr["PaymentDocCode"]);
            this.AccountDocTypeCode = Convert.ToString(dr["AccountDocTypeCode"]);
            this.DocumentDate = Convert.ToString(dr["DocumentDate"]);
            this.PostingDate = Convert.ToString(dr["PostingDate"]);
            this.PostingPeriod = Convert.ToString(dr["PostingPeriod"]);
            this.CompanyCode = Convert.ToString(dr["CompanyCode"]);
            this.BusinessAreaCode = Convert.ToString(dr["BusinessAreaCode"]);
            this.ProfitCenterCode = Convert.ToString(dr["ProfitCenterCode"]);
            this.BankAccount = Convert.ToString(dr["BankAccount"]);
            this.SpecialGLIndCode = Convert.ToString(dr["SpecialGLIndCode"]);
            this.CurrencyCode = Convert.ToString(dr["CurrencyCode"]);
            this.Reference = Convert.ToString(dr["Reference"]);
            this.ValueDate = Convert.ToString(dr["ValueDate"]);
            this.AccountType = Convert.ToString(dr["AccountType"]);
            this.CustomerCode = Convert.ToString(dr["CustomerCode"]);
            this.Amount = Convert.ToInt32(dr["Amount"]);
            this.AmountInLc = Convert.ToInt32(dr["AmountInLc"]);
            this.DocHeaderText = Convert.ToString(dr["DocHeaderText"]);
            this.ClearingText = Convert.ToString(dr["ClearingText"]);
            this.TradingPartnerBA = Convert.ToString(dr["TradingPartnerBA"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
            this.IsCancel = Convert.ToInt32(dr["IsCancel"]);

        }
    }
}