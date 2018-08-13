using System;
using System.Collections.Generic;
using System.Text;

namespace RSMApp_Authentication
{
    [Flags]
    public enum AuthTransaction
    {
        CanView = 0,
        CanCreate = 1,
        CanModify = 2,
        CanDelete =3

    }
}
