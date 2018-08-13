using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWM.Common.CustomException
{
    public class MSWMException: Exception
    {
        public MSWMException(string code, string category, string errorMessage = "") : base(errorMessage)
        {
            Code = code;
            Category = category;
            ErrorMessage = errorMessage;
        }

        public string Code { get; set; }

        public string ErrorMessage { get; set; }

        public string Category { get; set; }
    }
}
