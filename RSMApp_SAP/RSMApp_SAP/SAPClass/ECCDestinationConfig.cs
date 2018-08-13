using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAP.Middleware.Connector;
using RSMApp_SAP;

   public class ECCDestinationConfig : IDestinationConfiguration
   {
      // public string argClientCode = "";
       public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

       bool IDestinationConfiguration.ChangeEventsSupported()
       {
           return false;
           throw new Exception("The method or operation is not implemented.");
       }

       event RfcDestinationManager.ConfigurationChangeHandler IDestinationConfiguration.ConfigurationChanged
       {
           add { throw new Exception("The method or operation is not implemented."); }
           remove { throw new Exception("The method or operation is not implemented."); }
       }

       RfcConfigParameters IDestinationConfiguration.GetParameters(string destinationName)
       {
           RfcConfigParameters param = new RfcConfigParameters();

           SAPConnectionConfigManager ObjSapConConfigManager = new SAPConnectionConfigManager();
           SAPConnectionConfig objSapconConfig = (SAPConnectionConfig)ObjSapConConfigManager.objGetActiveSAPConnectionConfig(destinationName, 1);
           
           if (objSapconConfig != null)
           {
               
                   //DataBase
                   param.Add(RfcConfigParameters.SAPRouter, objSapconConfig.SAPRouter);
                   param.Add(RfcConfigParameters.AppServerHost, objSapconConfig.AppServerHost);
                   param.Add(RfcConfigParameters.SystemNumber, objSapconConfig.SystemNumber);
                   param.Add(RfcConfigParameters.SystemID, objSapconConfig.SystemID);
                   param.Add(RfcConfigParameters.User, objSapconConfig.SAPUser);
                   param.Add(RfcConfigParameters.Password, objSapconConfig.SAPPassword);
                   param.Add(RfcConfigParameters.Client, objSapconConfig.SAPClient);
                   param.Add(RfcConfigParameters.Language, objSapconConfig.SAPLanguage);
                   param.Add(RfcConfigParameters.PoolSize, objSapconConfig.PoolSize);
                   param.Add(RfcConfigParameters.MaxPoolSize, objSapconConfig.MaxPoolSize);
                   param.Add(RfcConfigParameters.IdleTimeout, objSapconConfig.IdleTimeout);

               
           }

           return param;
       }
   }
