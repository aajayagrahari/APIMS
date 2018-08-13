
//Created On :: 21, July, 2012
//Private const string ClassName = "BOM_Serial"
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
    public class BOM_Serial
    {
        private string _MaterialCode;
        private string _MRevisionCode;
        private string _SerialFrom;
        private string _SerialTo;
        private string _BatchDate;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;

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

        public string SerialFrom
        {
            get
            {
                return _SerialFrom;
            }
            set
            {
                _SerialFrom = value;
            }
        }

        public string SerialTo
        {
            get
            {
                return _SerialTo;
            }
            set
            {
                _SerialTo = value;
            }
        }

        public string BatchDate
        {
            get
            {
                return _BatchDate;
            }
            set
            {
                _BatchDate = value;
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
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.MRevisionCode = Convert.ToString(dr["MRevisionCode"]);
            this.SerialFrom = Convert.ToString(dr["SerialFrom"]);
            this.SerialTo = Convert.ToString(dr["SerialTo"]);
            this.BatchDate = Convert.ToDateTime(dr["BatchDate"]).ToString("yyyy-MM-dd");
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
        }
    }
}