using FDK.UI.Base.Interface;
using System.Collections.Generic;

namespace FDK.GameData
{
    public class FDKCraftingUIData : IGameUIData
    {
        public string Name;
        public string Description;

        public List<FDKMaterialUIData> MaterialData;

        public FDKCraftingUIData(string name, string description, List<FDKMaterialUIData> materialData)
        {
            Name = name;
            Description = description;
            MaterialData = materialData;
        }
    }

    public class FDKMaterialUIData : IGameUIData
    {
        public FDKMaterialUIData(string name, string amount)
        {
            Name = name;
            AmountNeeded = amount;
        }


        public string Name;
        public string AmountNeeded;
    }
}
