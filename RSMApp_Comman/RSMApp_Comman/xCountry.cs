using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace RSMApp_Comman
{
    [Serializable]
    public class xCountry
    {
        private string _CountryCode;
        public string CountryCode
        {
            get
            {
                return _CountryCode;
            }
            set
            {
                _CountryCode = value;
            }
        }

        private string _CountryName;
        public string CountryName
        {
            get
            {
                return _CountryName;
            }
            set
            {
                _CountryName = value;
            }
        }

        public void SetObjectInfo(DataRow dr)
        {
            this.CountryCode = Convert.ToString(dr["CountryCode"]);
            this.CountryName = Convert.ToString(dr["CountryName"]);
        }





    }
}
