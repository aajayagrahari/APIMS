using System;
using System.Collections.Generic;
using System.Text;

namespace RSMApp_Comman
{
    
    public class GlobalFunction
    {
        public string[] SpliteSQLReturnMsg(string strResult)
        {
            Char[] Splite = { ';' };
            string[] Splitter = Convert.ToString(strResult).Split(Splite);
            return Splitter;
        }

    }
}
