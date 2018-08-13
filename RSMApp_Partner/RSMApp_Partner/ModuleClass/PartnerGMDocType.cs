
//Created On :: 29, November, 2012
//Private const string ClassName = "PartnerGMDocType"
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
    public class PartnerGMDocType
    {
        private string _PartnerGMDocTypeCode;
        private string _PartnerGMDocTypeDesc;
        private int _ItemNoIncr;
        private string _NumRange;
        private string _AssignType;
        private string _MaterialDocTypeCode;
        private string _RepairProcessCode;
        private string _DefaultFromStoreCode;
        private string _DefaultFromStockIndicator;
        private string _DefaultToStoreCode;
        private string _DefaultToStockIndicator;
        private int _AgainstRO;
        private int _CreateRepOrder;
        private int _RecAgainstIssueDoc;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string PartnerGMDocTypeCode
        {
            get
            {
                return _PartnerGMDocTypeCode;
            }
            set
            {
                _PartnerGMDocTypeCode = value;
            }
        }
        public string PartnerGMDocTypeDesc
        {
            get
            {
                return _PartnerGMDocTypeDesc;
            }
            set
            {
                _PartnerGMDocTypeDesc = value;
            }
        }
        public int ItemNoIncr
        {
            get
            {
                return _ItemNoIncr;
            }
            set
            {
                _ItemNoIncr = value;
            }
        }
        public string NumRange
        {
            get
            {
                return _NumRange;
            }
            set
            {
                _NumRange = value;
            }
        }
        public string AssignType
        {
            get
            {
                return _AssignType;
            }
            set
            {
                _AssignType = value;
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
        public string RepairProcessCode
        {
            get
            {
                return _RepairProcessCode;
            }
            set
            {
                _RepairProcessCode = value;
            }
        }
        public string DefaultFromStoreCode
        {
            get
            {
                return _DefaultFromStoreCode;
            }
            set
            {
                _DefaultFromStoreCode = value;
            }
        }
        public string DefaultFromStockIndicator
        {
            get
            {
                return _DefaultFromStockIndicator;
            }
            set
            {
                _DefaultFromStockIndicator = value;
            }
        }
        public string DefaultToStoreCode
        {
            get
            {
                return _DefaultToStoreCode;
            }
            set
            {
                _DefaultToStoreCode = value;
            }
        }
        public string DefaultToStockIndicator
        {
            get
            {
                return _DefaultToStockIndicator;
            }
            set
            {
                _DefaultToStockIndicator = value;
            }
        }
        public int AgainstRO
        {
            get
            {
                return _AgainstRO;
            }
            set
            {
                _AgainstRO = value;
            }
        }
        public int CreateRepOrder
        {
            get
            {
                return _CreateRepOrder;
            }
            set
            {
                _CreateRepOrder = value;
            }
        }
        public int RecAgainstIssueDoc
        {
            get
            {
                return _RecAgainstIssueDoc;
            }
            set
            {
                _RecAgainstIssueDoc = value;
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
            this.PartnerGMDocTypeCode = Convert.ToString(dr["PartnerGMDocTypeCode"]);
            this.PartnerGMDocTypeDesc = Convert.ToString(dr["PartnerGMDocTypeDesc"]);
            this.ItemNoIncr = Convert.ToInt32(dr["ItemNoIncr"]);
            this.NumRange = Convert.ToString(dr["NumRange"]);
            this.AssignType = Convert.ToString(dr["AssignType"]);
            this.MaterialDocTypeCode = Convert.ToString(dr["MaterialDocTypeCode"]);
            this.RepairProcessCode = Convert.ToString(dr["RepairProcessCode"]);
            this.DefaultFromStoreCode = Convert.ToString(dr["DefaultFromStoreCode"]);
            this.DefaultFromStockIndicator = Convert.ToString(dr["DefaultFromStockIndicator"]);
            this.DefaultToStoreCode = Convert.ToString(dr["DefaultToStoreCode"]);
            this.DefaultToStockIndicator = Convert.ToString(dr["DefaultToStockIndicator"]);
            this.AgainstRO = Convert.ToInt32(dr["AgainstRO"]);
            this.CreateRepOrder = Convert.ToInt32(dr["CreateRepOrder"]);
            this.RecAgainstIssueDoc = Convert.ToInt32(dr["RecAgainstIssueDoc"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}