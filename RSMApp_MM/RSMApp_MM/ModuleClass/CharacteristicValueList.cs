using System;
using System.Collections.Generic;
using System.Text;

namespace RSMApp_MM
{
    public class CharacteristicValueList : List<CharactersticsValueMaster>
    {
        public CharacteristicValueList(string argClientCode, string argObjectKey, string argObjectTable)
        {
            LoadAllCharacteristicValue(argClientCode, argObjectKey, argObjectTable);
        }

        private void LoadAllCharacteristicValue(string argClientCode, string argObjectKey, string argObjectTable)
        {
            if (this.Count > 0)
                this.Clear();

            CharactersticsValueMasterManager objCharacteristicValueManager = new CharactersticsValueMasterManager();

            objCharacteristicValueManager.colGetCharactersticsValue(argClientCode, argObjectKey, argObjectTable, this);
        }


        public void LoadCharacteristicValue4Class(string argClientCode, string argMaterialCode, string argClassType)
        {
            CharactersticsValueMasterManager objCharacteristicValueManager = new CharactersticsValueMasterManager();

            objCharacteristicValueManager.colGetCharactersticsValue4Class(argClassType, argMaterialCode, argClientCode , this);


        }

        public CharactersticsValueMaster GetCharacteristicValueID(string argCharactersticsName, string argObjectKey)
        {
            foreach (CharactersticsValueMaster argCharactersticsValueMaster in this)
            {
                if (argCharactersticsValueMaster.CharactersticsName == argCharactersticsName && argCharactersticsValueMaster.ObjectKey == argObjectKey)
                {
                    return argCharactersticsValueMaster;
                }
            }
            return null;
        }

        public CharactersticsValueMaster GetCharacteristicValueObject(string argObjectKey)
        {
            foreach (CharactersticsValueMaster argCharactersticsValueMaster in this)
            {
                if (argCharactersticsValueMaster.ObjectKey == argObjectKey)
                {
                    return argCharactersticsValueMaster;
                }
            }
            return null;
        }
    }
}
