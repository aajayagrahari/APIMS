using System;
using System.Collections.Generic;
using System.Text;

namespace RSMApp_Comman
{
    public class UserRoleList : List<User_Role_Details>
    {
        public UserRoleList(string argUserName, string argClientCode)
        {
            LoadAllRoleDetail(argUserName, argClientCode);
        }

        private void LoadAllRoleDetail(string argUserName, string argClientCode)
        {
            if (this.Count > 0)
                this.Clear();

            User_Role_DetailsManager objUser_Role_Details = new User_Role_DetailsManager();

            objUser_Role_Details.colGetUser_Role_Details(argUserName,argClientCode, this);
        }


        public void LoadUserRoleAuthJobDetail(string argUserName, string argClientCode)
        {
            if (this.Count > 0)
                this.Clear();

            User_Role_DetailsManager objUser_Role_Details = new User_Role_DetailsManager();

            objUser_Role_Details.colGetUser_Role_AuthJob_Details(argUserName, argClientCode, this);
        }

        public void LoadUserRoleAuthOrgDetail(string argUserName, string argClientCode)
        {
            if (this.Count > 0)
                this.Clear();

            User_Role_DetailsManager objUser_Role_Details = new User_Role_DetailsManager();

            objUser_Role_Details.colGetUser_Role_AuthOrg_Details(argUserName, argClientCode, this);
        }

        public User_Role_Details GetUserDetailByID(string argUserName, string argAuthRoleCode, string argClientCode)
        {
            foreach (User_Role_Details argUserRoleAuth in this)
            {
                if (argUserRoleAuth.UserName == argUserName && argUserRoleAuth.AuthRoleCode == argAuthRoleCode  && argUserRoleAuth.ClientCode == argClientCode)
                {
                    return argUserRoleAuth;
                }
            }
            return null;
        }

        public User_Role_Details GetUserDetails(string argUserName, string argUserAuthRoleCode, string argClientCode)
        {
            User_Role_Details objUserRoleDetails = new User_Role_Details();
            User_Role_DetailsManager objUserRoleDetailsManager = new User_Role_DetailsManager();

            objUserRoleDetails = objUserRoleDetailsManager.objGetUser_Role_Details(argUserName, argClientCode);

            return objUserRoleDetails;
        }


        public User_Role_Details GetUserRoleAuthJobDetail(string argUserName, string argModule, string argClientCode)
        {
            foreach (User_Role_Details argUserRoleAuth in this)
            {
                if (argUserRoleAuth.UserName == argUserName && argUserRoleAuth.Module == argModule && argUserRoleAuth.ClientCode == argClientCode)
                {
                    return argUserRoleAuth;
                }
            }
            return null;
        }

        public User_Role_Details GetUserRoleAuthOrgDetail(string argUserName, string argFieldName, string argClientCode)
        {
            foreach (User_Role_Details argUserRoleAuth in this)
            {
                if (argUserRoleAuth.UserName == argUserName && argUserRoleAuth.FieldName.Trim() == argFieldName.Trim() && argUserRoleAuth.ClientCode == argClientCode)
                {
                    return argUserRoleAuth;
                }
            }
            return null;
        }


        public bool CheckUserRoleAuthOrg(string argUserName, string argFieldName, string argValue, string argClientCode)
        {
            bool Result = false;

            foreach (User_Role_Details argUserRoleAuth in this)
            {
                if (argUserRoleAuth.UserName == argUserName && argUserRoleAuth.FieldName.Trim() == argFieldName.Trim() && argUserRoleAuth.ClientCode == argClientCode)
                {
                    if (CheckIsNUmeric(argValue) == false)
                    {
                        /*String*/
                        if (Convert.ToString(argUserRoleAuth.FieldValueTo) != "")
                        {
                            if ((string.Compare(argValue.Trim(), Convert.ToString(argUserRoleAuth.FieldValueFrom).Trim(), StringComparison.OrdinalIgnoreCase) >= 0) && (string.Compare(argValue, Convert.ToString(argUserRoleAuth.FieldValueTo), StringComparison.OrdinalIgnoreCase) <= 0))
                            {
                                Result = true;
                            }

                        }
                        else
                        {
                        
                            if (string.Compare(argValue.Trim(), Convert.ToString(argUserRoleAuth.FieldValueFrom).Trim(), StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                Result = true;
                            }
                        }

                        
                            
                        
                    }
                    else
                    {

                        if (argUserRoleAuth.FieldValueTo.ToString() != "")
                        {
                            if ((Convert.ToInt32(argValue) >= Convert.ToInt32(argUserRoleAuth.FieldValueFrom)) && (Convert.ToInt32(argValue) <= Convert.ToInt32(argUserRoleAuth.FieldValueTo)))
                            {
                                Result = true;
                            }
                        }
                        else
                        {
                            if (argUserRoleAuth.FieldValueFrom.ToString() != "")
                            {
                                if (Convert.ToInt32(argValue) >= Convert.ToInt32(argUserRoleAuth.FieldValueFrom))
                                {
                                    Result = true;
                                }
                            }
                        }

                    }

                    if (Result == true)
                    {
                        break;
                    }
                }
            }

            return Result;

        }


        public bool CheckIsNUmeric(string Value)
        {
            Double result;
            return Double.TryParse(Value, System.Globalization.NumberStyles.Integer,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }

    }
}
