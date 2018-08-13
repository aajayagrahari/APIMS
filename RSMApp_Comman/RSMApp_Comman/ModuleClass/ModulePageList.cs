using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSMApp_Comman
{
   public class ModulePageList : List<ModulePage>
    {
        public ModulePageList(string argParentModule, string argClientCode)
        {
            LoadAllModulePages(argParentModule, argClientCode);
        }

        private void LoadAllModulePages(string argParentModule, string argClientCode)
        {
            if (this.Count > 0)
                this.Clear();

            ModulePageManager objModulePageManager = new ModulePageManager();
            objModulePageManager.colGetModulePage4Menu(argParentModule, argClientCode, this);
        }
    }
}
