using FDK.UI.Base.Interface;
using System.Collections.Generic;

namespace FDK.GameData
{
    public interface ICraftingUIData : ISelectableUIData
    {
        string Name { get; }
        string Description { get; }
        List<FDKMaterialUIData> Materials { get; }
    }

    public class FDKCraftingUIData : ICraftingUIData
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public List<FDKMaterialUIData> Materials { get; private set; }

        public FDKCraftingUIData(string id, string name, string description, List<FDKMaterialUIData> materialData)
        {
            Name = name;
            Description = description;
            Materials = materialData;
            Id = id;
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
