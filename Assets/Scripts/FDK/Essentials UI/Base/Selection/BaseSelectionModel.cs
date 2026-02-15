using FDK.UI.Base.Interface;
using System.Collections.Generic;
using UnityEngine.Events;

namespace FDK.UI
{
    public interface ISelectionModel<T> where T : IGameUIData
    {
        T CurrentSelected { get; }
        UnityEvent<T> OnDataUpdatedEvt { get; }
        void Initialize(List<T> gameUIDatas);
        void SetSelected(T gameData);
    }

    public abstract class BaseSelectionModel<T> : ISelectionModel<T> where T : IGameUIData
    {
        public T CurrentSelected { get; protected set; }
        public UnityEvent<T> OnDataUpdatedEvt { get; protected set; } = new();
        public List<T> GameData;

        public virtual void Initialize(List<T> gameData)
        {
            if (gameData == null) return;
            if (gameData.Count <= 0) return;

            GameData = gameData;
            CurrentSelected = GameData[0];
            OnDataUpdatedEvt.Invoke(CurrentSelected);
        }

        public void SetSelected(T gameData)
        {
            CurrentSelected = gameData;
            OnDataUpdatedEvt.Invoke(CurrentSelected);
        }
    }
}
