using FDK.UI.Base.Interface;
using System.Collections.Generic;

namespace FDK.UI.Selection
{
    public abstract class BaseStaticSelectionModel<T> : BaseSelectionModel<T> where T : IGameUIData
    {
        public override void Initialize(List<T> gameData)
        {
            if (gameData == null) return;
            if (gameData.Count <= 0) return;

            GameData = gameData;
            CurrentSelected = GameData[0];
            OnDataUpdatedEvt.Invoke(CurrentSelected);
        }
    }
}
