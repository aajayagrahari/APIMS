
//Created On :: 18, October, 2012
//Private const string ClassName = "CallType"
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
    public class CallType
    {
        private string _CallTypeCode;
        private string _CallTypeDesc;
        private int _IsReceivable;
        private int _IsApprovalReq;
        private int _IsAdvReplacement;
        private int _IsTopLevelItem;
        private int _IsBomExplodAllowed;
        private int _IsBOMItemReceivedChk;
        private int _CallCloseByCertify;
        private string _DefaultStoreCode;
        private string _DefaultStockIndicator;
        private string _MaterialDocTypeCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;

        private string _DefaultNRStoreCode;
        private string _DefaultNRStockIndicator;



        public string CallTypeCode
        {
            get
            {
                return _CallTypeCode;
            }
            set
            {
                _CallTypeCode = value;
            }
        }
        public string CallTypeDesc
        {
            get
            {
                return _CallTypeDesc;
            }
            set
            {
                _CallTypeDesc = value;
            }
        }
        public int IsReceivable
        {
            get
            {
                return _IsReceivable;
            }
            set
            {
                _IsReceivable = value;
            }
        }
        public int IsApprovalReq
        {
            get
            {
                return _IsApprovalReq;
            }
            set
            {
                _IsApprovalReq = value;
            }
        }
        public int IsAdvReplacement
        {
            get
            {
                return _IsAdvReplacement;
            }
            set
            {
                _IsAdvReplacement = value;
            }
        }
        public int IsTopLevelItem
        {
            get
            {
                return _IsTopLevelItem;
            }
            set
            {
                _IsTopLevelItem = value;
            }
        }
        public int IsBomExplodAllowed
        {
            get
            {
                return _IsBomExplodAllowed;
            }
            set
            {
                _IsBomExplodAllowed = value;
            }
        }
        public int IsBOMItemReceivedChk
        {
            get
            {
                return _IsBOMItemReceivedChk;
            }
            set
            {
                _IsBOMItemReceivedChk = value;
            }
        }
        public int CallCloseByCertify
        {
            get
            {
                return _CallCloseByCertify;
            }
            set
            {
                _CallCloseByCertify = value;
            }
        }
        public string DefaultStoreCode
        {
            get
            {
                return _DefaultStoreCode;
            }
            set
            {
                _DefaultStoreCode = value;
            }
        }
        public string DefaultStockIndicator
        {
            get
            {
                return _DefaultStockIndicator;
            }
            set
            {
                _DefaultStockIndicator = value;
            }
        }
        public string MaterialDocTypeCode
        {
            get
            {
                return _MaterialDocTypeCode;
            }
            set
            {
                _MaterialDocTypeCode = value;
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

        public string DefaultNRStoreCode
        {
            get
            {
                return _DefaultNRStoreCode;
            }
            set
            {
                _DefaultNRStoreCode = value;
            }
        }

        public string DefaultNRStockIndicator
        {
            get
            {
                return _DefaultNRStockIndicator;
            }
            set
            {
                _DefaultNRStockIndicator = value;
            }
        }




        public void SetObjectInfo(DataRow dr)
        {
            this.CallTypeCode = Convert.ToString(dr["CallTypeCode"]);
            this.CallTypeDesc = Convert.ToString(dr["CallTypeDesc"]);
            this.IsReceivable = Convert.ToInt32(dr["IsReceivable"]);
            this.IsApprovalReq = Convert.ToInt32(dr["IsApprovalReq"]);
            this.IsAdvReplacement = Convert.ToInt32(dr["IsAdvReplacement"]);
            this.IsTopLevelItem = Convert.ToInt32(dr["IsTopLevelItem"]);
            this.IsBomExplodAllowed = Convert.ToInt32(dr["IsBomExplodAllowed"]);
            this.IsBOMItemReceivedChk = Convert.ToInt32(dr["IsBOMItemReceivedChk"]);
            this.CallCloseByCertify = Convert.ToInt32(dr["CallCloseByCertify"]);
            this.DefaultStoreCode = Convert.ToString(dr["DefaultStoreCode"]);
            this.DefaultStockIndicator = Convert.ToString(dr["DefaultStockIndicator"]);
            this.MaterialDocTypeCode = Convert.ToString(dr["MaterialDocTypeCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

            this.DefaultNRStockIndicator = Convert.ToString(dr["DefaultNRStockIndicator"]);
            this.DefaultNRStoreCode = Convert.ToString(dr["DefaultNRStoreCode"]);
        }
    }
}