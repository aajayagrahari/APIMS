
//Created On :: 13, June, 2012
//Private const string ClassName = "BillsofMaterial"
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
    public class BillsofMaterial
    {
        private int _idBOM;
        private string _MastMaterialCode;
        private string _MaterialCode;
        private string _MRevisionCode;
        private string _DRevisionCode;
        private string _ValidFrom;
        private string _ValidTo;
        private int _IsSerialBatch;
        private int _Quantity;
        private int _IsServiceRec;
        private int _IsDOARec;
        private int _IsAssignTech;
        private int _IsWarrantyApp;
        private int _IsExtWarrantyApp;
        private string _BaseWarrantyOn;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;
        private string _MaterialTypeCode;
        private string _MatGroup1Code;
        private string _MatGroup2Code;

        private string _MatDesc;
        private string _MastMaterialTypeCode;
        private string _MastMatGroup1Code;
        private string _MastMatGroup2Code;
        private string _MastMatDesc;

        private int _ErrFlag;
        private string _ErrMessage;

        public int idBOM
        {
            get
            {
                return _idBOM;
            }
            set
            {
                _idBOM = value;
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

        public int Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
            }
        }

        public  string MRevisionCode
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

        public string DRevisionCode
        {
            get
            {
                return _DRevisionCode;
            }
            set
            {
                _DRevisionCode = value;
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

        public int IsServiceRec
        {
            get
            {
                return _IsServiceRec;
            }
            set
            {
                _IsServiceRec = value;
            }
        }

        public int IsDOARec
        {
            get
            {
                return _IsDOARec;
            }
            set
            {
                _IsDOARec = value;
            }
        }

        public int IsAssignTech
        {
            get
            {
                return _IsAssignTech;
            }
            set
            {
                _IsAssignTech = value;
            }
        }

        public int IsSerialBatch
        {
            get
            {
                return _IsSerialBatch;
            }
            set
            {
                _IsSerialBatch = value;
            }
        }

        public int IsWarrantyApp
        {
            get
            {
                return _IsWarrantyApp;
            }
            set
            {
                _IsWarrantyApp = value;
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

        public string BaseWarrantyOn
        {
            get
            {
                return _BaseWarrantyOn;
            }
            set
            {
                _BaseWarrantyOn = value;
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

        public string MatDesc
        {
            get
            {
                return _MatDesc;
            }
            set
            {
                _MatDesc = value;
            }
        }

        public string MastMatDesc
        {
            get
            {
                return _MastMatDesc;
            }
            set
            {
                _MastMatDesc = value;
            }
        }

        public int ErrFlag
        {
            get
            {
                return _ErrFlag;
            }
            set
            {
                _ErrFlag = value;
            }
        }

        public string ErrMessage
        {
            get
            {
                return _ErrMessage;
            }
            set
            {
                _ErrMessage = value;
            }
        }

        public string MaterialTypeCode
        {
            get
            {
                return _MaterialTypeCode;
            }
            set
            {
                _MaterialTypeCode = value;
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

        public string MatGroup2Code
        {
            get
            {
                return _MatGroup2Code;
            }
            set
            {
                _MatGroup2Code = value;
            }
        }

        public string MastMaterialTypeCode
        {
            get
            {
                return _MastMaterialTypeCode;
            }
            set
            {
                _MastMaterialTypeCode = value;
            }
        }

        public string MastMatGroup1Code
        {
            get
            {
                return _MastMatGroup1Code;
            }
            set
            {
                _MastMatGroup1Code = value;
            }
        }

        public string MastMatGroup2Code
        {
            get
            {
                return _MastMatGroup2Code;
            }
            set
            {
                _MastMatGroup2Code = value;
            }
        }

        public void SetObjectInfo(DataRow dr)
        {
            this.idBOM = Convert.ToInt32(dr["idBOM"]);
            this.MastMaterialCode = Convert.ToString(dr["MastMaterialCode"]).Trim();
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]).Trim();
            this.MRevisionCode = Convert.ToString(dr["MRevisionCode"]).Trim();
            this.DRevisionCode = Convert.ToString(dr["DRevisionCode"]).Trim();
            this.ValidFrom = Convert.ToString(dr["ValidFrom"]).Trim();
            this.ValidTo = Convert.ToString(dr["ValidTo"]).Trim();
            this.IsSerialBatch = Convert.ToInt32(dr["IsSerialBatch"]);
            this.Quantity = Convert.ToInt32(dr["Quantity"]);
            this.IsServiceRec = Convert.ToInt32(dr["IsServiceRec"]);
            this.IsDOARec = Convert.ToInt32(dr["IsDOARec"]);
            this.IsAssignTech = Convert.ToInt32(dr["IsAssignTech"]);
            this.IsWarrantyApp = Convert.ToInt32(dr["IsWarrantyApp"]);
            this.IsExtWarrantyApp = Convert.ToInt32(dr["IsExtWarrantyApp"]);
            this.BaseWarrantyOn = Convert.ToString(dr["BaseWarrantyOn"]).Trim();
            this.ClientCode = Convert.ToString(dr["ClientCode"]).Trim();
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]).Trim();
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]).Trim();
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]).Trim();
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]).Trim();
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
            this.MatDesc = Convert.ToString(dr["MatDesc"]).Trim();
            this.MastMatDesc = Convert.ToString(dr["MastMatDesc"]).Trim();
            this.MastMaterialTypeCode = Convert.ToString(dr["MastMaterialTypeCode"]).Trim();
            this.MastMatGroup1Code = Convert.ToString(dr["MastMatGroup1Code"]).Trim();
            this.MastMatGroup2Code = Convert.ToString(dr["MastMatGroup2Code"]).Trim();
            this.MaterialTypeCode = Convert.ToString(dr["MaterialTypeCode"]).Trim();
            this.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]).Trim();
            this.MatGroup2Code = Convert.ToString(dr["MatGroup2Code"]).Trim();
        }
    }
}