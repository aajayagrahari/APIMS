
//Created On :: 16, October, 2012
//Private const string ClassName = "PartnerStock"
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
    public class PartnerStock
    {
        private string _PartnerCode;
        private string _StoreCode;
        private string _PartnerEmployeeCode;
        private string _MaterialCode;
        private int _UnrestrictedStock;
        private int _RestrictedStock;
        private int _BlockedStock;
        private int _ReturnedStock;
        private int _QCStock;
        private int _MAP;
        private double _OnSiteStock;
        private string _UOMCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



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
        public string StoreCode
        {
            get
            {
                return _StoreCode;
            }
            set
            {
                _StoreCode = value;
            }
        }
        public string PartnerEmployeeCode
        {
            get
            {
                return _PartnerEmployeeCode;
            }
            set
            {
                _PartnerEmployeeCode = value;
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
        public int UnrestrictedStock
        {
            get
            {
                return _UnrestrictedStock;
            }
            set
            {
                _UnrestrictedStock = value;
            }
        }
        public int RestrictedStock
        {
            get
            {
                return _RestrictedStock;
            }
            set
            {
                _RestrictedStock = value;
            }
        }
        public int BlockedStock
        {
            get
            {
                return _BlockedStock;
            }
            set
            {
                _BlockedStock = value;
            }
        }
        public int ReturnedStock
        {
            get
            {
                return _ReturnedStock;
            }
            set
            {
                _ReturnedStock = value;
            }
        }
        public int QCStock
        {
            get
            {
                return _QCStock;
            }
            set
            {
                _QCStock = value;
            }
        }
        public int MAP
        {
            get
            {
                return _MAP;
            }
            set
            {
                _MAP = value;
            }
        }
        public double OnSiteStock
        {
            get
            {
                return _OnSiteStock;
            }
            set
            {
                _OnSiteStock = value;
            }
        }

        public string UOMCode
        {
            get
            {
                return _UOMCode;
            }
            set
            {
                _UOMCode = value;
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
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.StoreCode = Convert.ToString(dr["StoreCode"]);
            this.PartnerEmployeeCode = Convert.ToString(dr["PartnerEmployeeCode"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.UnrestrictedStock = Convert.ToInt32(dr["UnrestrictedStock"]);
            this.RestrictedStock = Convert.ToInt32(dr["RestrictedStock"]);
            this.BlockedStock = Convert.ToInt32(dr["BlockedStock"]);
            this.ReturnedStock = Convert.ToInt32(dr["ReturnedStock"]);
            this.QCStock = Convert.ToInt32(dr["QCStock"]);
            this.MAP = Convert.ToInt32(dr["MAP"]);
            this.OnSiteStock = Convert.ToDouble(dr["OnSiteStock"]);
            this.UOMCode = Convert.ToString(dr["UOMCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}