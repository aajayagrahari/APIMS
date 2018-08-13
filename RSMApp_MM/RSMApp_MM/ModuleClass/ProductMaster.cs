
//Created On :: 19, October, 2012
//Private const string ClassName = "ProductMaster"
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
    public class ProductMaster
    {
        private int _idProduct;
        private string _ProductNode;
        private int _ProductLevel;
        private string _SerialNo;
        private string _MastSerialNo;
        private string _MastMaterialCode;
        private string _MRevisionCode;
        private string _CompanyCode;
        private string _MaterialCode;
        private string _MatGroup1Code;
        private string _WarrantyCode;
        private string _ManufacturingDate;
        private string _PrimarySalesDate;
        private DateTime _CustInvoiceDate;
        private string _CustInvoiceNo;
        private string _CustType;
        private string _CustName;
        private string _CustAddress1;
        private string _CustAddress2;
        private string _CustPhone;
        private string _CustMobile;
        private string _CustEmail;
        private string _CustGender;
        private string _WarrantyOn;
        private string _ExtWarrantyOn;
        private string _ExtWarrantyReason;
        private string _ValidFrom;
        private string _ValidTo;
        private string _ExtValidFrom;
        private string _ExtValidTo;
        private int _IsExtWarrantyApp;
        private int _IsWarrantyExp;
        private int _IsAbandoned;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;
        private int _IsExploded;



        public int idProduct
        {
            get
            {
                return _idProduct;
            }
            set
            {
                _idProduct = value;
            }
        }
        public string ProductNode
        {
            get
            {
                return _ProductNode;
            }
            set
            {
                _ProductNode = value;
            }
        }
        public int ProductLevel
        {
            get
            {
                return _ProductLevel;
            }
            set
            {
                _ProductLevel = value;
            }
        }
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
        public string MastSerialNo
        {
            get
            {
                return _MastSerialNo;
            }
            set
            {
                _MastSerialNo = value;
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
        public string MRevisionCode
        {
            get
            {
                return _MRevisionCode;
            }
            set
            {
                _MRevisionCode = value;
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
        public string WarrantyCode
        {
            get
            {
                return _WarrantyCode;
            }
            set
            {
                _WarrantyCode = value;
            }
        }
        public string ManufacturingDate
        {
            get
            {
                return _ManufacturingDate;
            }
            set
            {
                _ManufacturingDate = value;
            }
        }
        public string PrimarySalesDate
        {
            get
            {
                return _PrimarySalesDate;
            }
            set
            {
                _PrimarySalesDate = value;
            }
        }
        public DateTime CustInvoiceDate
        {
            get
            {
                return _CustInvoiceDate;
            }
            set
            {
                _CustInvoiceDate = value;
            }
        }
        public string CustInvoiceNo
        {
            get
            {
                return _CustInvoiceNo;
            }
            set
            {
                _CustInvoiceNo = value;
            }
        }
        public string CustType
        {
            get
            {
                return _CustType;
            }
            set
            {
                _CustType = value;
            }
        }
        public string CustName
        {
            get
            {
                return _CustName;
            }
            set
            {
                _CustName = value;
            }
        }
        public string CustAddress1
        {
            get
            {
                return _CustAddress1;
            }
            set
            {
                _CustAddress1 = value;
            }
        }
        public string CustAddress2
        {
            get
            {
                return _CustAddress2;
            }
            set
            {
                _CustAddress2 = value;
            }
        }
        public string CustPhone
        {
            get
            {
                return _CustPhone;
            }
            set
            {
                _CustPhone = value;
            }
        }
        public string CustMobile
        {
            get
            {
                return _CustMobile;
            }
            set
            {
                _CustMobile = value;
            }
        }
        public string CustEmail
        {
            get
            {
                return _CustEmail;
            }
            set
            {
                _CustEmail = value;
            }
        }
        public string CustGender
        {
            get
            {
                return _CustGender;
            }
            set
            {
                _CustGender = value;
            }
        }
        public string WarrantyOn
        {
            get
            {
                return _WarrantyOn;
            }
            set
            {
                _WarrantyOn = value;
            }
        }
        public string ExtWarrantyOn
        {
            get
            {
                return _ExtWarrantyOn;
            }
            set
            {
                _ExtWarrantyOn = value;
            }
        }
        public string ExtWarrantyReason
        {
            get
            {
                return _ExtWarrantyReason;
            }
            set
            {
                _ExtWarrantyReason = value;
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
        public string ExtValidFrom
        {
            get
            {
                return _ExtValidFrom;
            }
            set
            {
                _ExtValidFrom = value;
            }
        }
        public string ExtValidTo
        {
            get
            {
                return _ExtValidTo;
            }
            set
            {
                _ExtValidTo = value;
            }
        }
        public int IsExtWarrantyApp
        {
            get
            {
                return _IsExtWarrantyApp;
            }
            set
            {
                _IsExtWarrantyApp = value;
            }
        }
        public int IsWarrantyExp
        {
            get
            {
                return _IsWarrantyExp;
            }
            set
            {
                _IsWarrantyExp = value;
            }
        }
        public int IsAbandoned
        {
            get
            {
                return _IsAbandoned;
            }
            set
            {
                _IsAbandoned = value;
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

        public int IsExploded
        {
            get
            {
                return _IsExploded;
            }
            set
            {
                _IsExploded = value;
            }
        }


        public void SetObjectInfo(DataRow dr)
        {
            this.idProduct = Convert.ToInt32(dr["idProduct"]);
            this.ProductNode = Convert.ToString(dr["ProductNode"]);
            this.ProductLevel = Convert.ToInt32(dr["ProductLevel"]);
            this.SerialNo = Convert.ToString(dr["SerialNo"]);
            this.MastSerialNo = Convert.ToString(dr["MastSerialNo"]);
            this.MastMaterialCode = Convert.ToString(dr["MastMaterialCode"]);
            this.MRevisionCode = Convert.ToString(dr["MRevisionCode"]);
            this.CompanyCode = Convert.ToString(dr["CompanyCode"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]);
            this.WarrantyCode = Convert.ToString(dr["WarrantyCode"]);
            this.ManufacturingDate = Convert.ToString(dr["ManufacturingDate"]);
            this.PrimarySalesDate = Convert.ToString(dr["PrimarySalesDate"]);
            this.CustInvoiceDate = Convert.ToDateTime(dr["CustInvoiceDate"]);
            this.CustInvoiceNo = Convert.ToString(dr["CustInvoiceNo"]);
            this.CustType = Convert.ToString(dr["CustType"]);
            this.CustName = Convert.ToString(dr["CustName"]);
            this.CustAddress1 = Convert.ToString(dr["CustAddress1"]);
            this.CustAddress2 = Convert.ToString(dr["CustAddress2"]);
            this.CustPhone = Convert.ToString(dr["CustPhone"]);
            this.CustMobile = Convert.ToString(dr["CustMobile"]);
            this.CustEmail = Convert.ToString(dr["CustEmail"]);
            this.CustGender = Convert.ToString(dr["CustGender"]);
            this.WarrantyOn = Convert.ToString(dr["WarrantyOn"]);
            this.ExtWarrantyOn = Convert.ToString(dr["ExtWarrantyOn"]);
            this.ExtWarrantyReason = Convert.ToString(dr["ExtWarrantyReason"]);
            this.ValidFrom = Convert.ToString(dr["ValidFrom"]);
            this.ValidTo = Convert.ToString(dr["ValidTo"]);
            this.ExtValidFrom = Convert.ToString(dr["ExtValidFrom"]);
            this.ExtValidTo = Convert.ToString(dr["ExtValidTo"]);
            this.IsExtWarrantyApp = Convert.ToInt32(dr["IsExtWarrantyApp"]);
            this.IsWarrantyExp = Convert.ToInt32(dr["IsWarrantyExp"]);
            this.IsAbandoned = Convert.ToInt32(dr["IsAbandoned"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}