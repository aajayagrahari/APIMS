using System;
using System.Collections.Generic;
using System.Text;

namespace RSMApp_Authentication
{
    public class UserAuthentication
    {
        private string _UserName;
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }

        private string _Module;
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

        private string _TransactionType;
        public string TransactionType
        {
            get
            {
                return _TransactionType;
            }
            set
            {
                _TransactionType = value;
            }
        }
        
    }
}
