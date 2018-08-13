
//Created On :: 05, October, 2012
//Private const string ClassName = "AccDeterminTableMaster"
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
    public class AccDeterminTableMaster
    {
        private string _AccDMTableCode;
        private string _TableDescription;
        private int _TableSequence;
        private string _TableType;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



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
        public string TableDescription
        {
            get
            {
                return _TableDescription;
            }
            set
            {
                _TableDescription = value;
            }
        }
        public int TableSequence
        {
            get
            {
                return _TableSequence;
            }
            set
            {
                _TableSequence = value;
            }
        }
        public string TableType
        {
            get
            {
                return _TableType;
            }
            set
            {
                _TableType = value;
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
            this.AccDMTableCode = Convert.ToString(dr["AccDMTableCode"]);
            this.TableDescription = Convert.ToString(dr["TableDescription"]);
            this.TableSequence = Convert.ToInt32(dr["TableSequence"]);
            this.TableType = Convert.ToString(dr["TableType"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}