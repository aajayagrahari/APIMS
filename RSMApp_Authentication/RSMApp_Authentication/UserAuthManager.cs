using System;
using System.Collections.Generic;
using System.Text;

namespace RSMApp_Authentication
{
    public class UserAuthManager 
    {
        public bool CheckUserAuthentication(UserAuthentication objUserAuth)
        {
            bool IsAllowed = false;

            if (objUserAuth.TransactionType == AuthTransaction.CanView.ToString())
            {
                IsAllowed = true;
            }

            if (objUserAuth.TransactionType == AuthTransaction.CanCreate.ToString())
            {
                IsAllowed = true;
            }

            if (objUserAuth.TransactionType == AuthTransaction.CanModify.ToString())
            {
                IsAllowed = true;
            }

            if (objUserAuth.TransactionType == AuthTransaction.CanDelete.ToString())
            {
                IsAllowed = true;
            }


            return IsAllowed;
        }

    }
}
