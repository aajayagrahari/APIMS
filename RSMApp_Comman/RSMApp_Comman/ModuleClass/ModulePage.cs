
//Created On :: 05, May, 2012
//Private const string ClassName = "ModulePage"
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
    public class ModulePage
    {
        private string _ModuleType;
        private string _Module;
        private string _PageURL;
        private string _ParentModule;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;

        private int _idModulePage;
        private string _idPModulePage;

        private string _ParentModuleNode;

        private string _tModuleNode;

        private string _ClientCode;


        public string ModuleType
        {
            get
            {
                return _ModuleType;
            }
            set
            {
                _ModuleType = value;
            }
        }

        public string Module
        {
            get
            {
                return _Module;
            }
            set
            {
                _Module = value;
            }
        }
        
        public string ParentModule
        {
            get
            {
                return _ParentModule;
            }
            set
            {
                _ParentModule = value;
            }
        }

        public string PageURL
        {
            get
            {
                return _PageURL;
            }
            set
            {
                _PageURL = value;
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

        public int idModulePage
        {
            get
            {
                return _idModulePage;
            }
            set
            {
                _idModulePage = value;
            }
        }

        public string idPModulePage
        {
            get
            {
                return _idPModulePage;
            }
            set
            {
                _idPModulePage = value;
            }
        }

        public string ParentModuleNode
        {
            get
            {
                return _ParentModuleNode;
            }
            set
            {
                _ParentModuleNode = value;
            }
        }

        public string tModuleNode
        {
            get
            {
                return _tModuleNode;
            }
            set
            {
                _tModuleNode = value;
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

        public void SetObjectInfo(DataRow dr)
        {
            this.ModuleType = Convert.ToString(dr["ModuleType"]);
            this.Module = Convert.ToString(dr["Module"]);
            this.ParentModule = Convert.ToString(dr["ParentModule"]);
            this.idPModulePage = Convert.ToString(dr["idPModulePage"]);
            this.idModulePage = Convert.ToInt32(dr["idModulePage"]);
            this.PageURL = Convert.ToString(dr["PageURL"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
        }



        public void SetObjectInfo2(DataRow dr)
        {
            this.ModuleType = Convert.ToString(dr["ModuleType"]);
            this.Module = Convert.ToString(dr["Module"]);
            this.ParentModule = Convert.ToString(dr["ParentModule"]);
            if (Convert.ToString(dr["idPModulePage"]) == "")
            {
                this.idPModulePage = null;
            }
            else
            {
                this.idPModulePage = Convert.ToString(dr["idPModulePage"]);
            }
            this.idModulePage = Convert.ToInt32(dr["idModulePage"]);
            this.PageURL = Convert.ToString(dr["PageURL"]);
            this.tModuleNode = Convert.ToString(dr["tModuleNode"]);                     
        }
    }
}