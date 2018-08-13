
//Created On :: 18, December, 2012
//Private const string ClassName = "PartnerMatPriceList"
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
    public class PartnerMatPriceList
    {
        private int _PriceListID;
        private string _PartnerTypeCode;
        private string _MaterialCode;
        private double _Price;
        private string _UOMCode;
        private string _ValidFrom;
        private string _ValidTo;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public int PriceListID
        {
            get
            {
                return _PriceListID;
            }
            set
            {
                _PriceListID = value;
            }
        }
        public string PartnerTypeCode
        {
            get
            {
                return _PartnerTypeCode;
            }
            set
            {
                _PartnerTypeCode = value;
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
        public double Price
        {
            get
            {
                return _Price;
            }
            set
            {
                _Price = value;
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
            this.PriceListID = Convert.ToInt32(dr["PriceListID"]);
            this.PartnerTypeCode = Convert.ToString(dr["PartnerTypeCode"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.Price = Convert.ToInt32(dr["Price"]);
            this.UOMCode = Convert.ToString(dr["UOMCode"]);
            this.ValidFrom = Convert.ToString(dr["ValidFrom"]);
            this.ValidTo = Convert.ToString(dr["ValidTo"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}