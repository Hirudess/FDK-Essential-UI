using System.Collections.Generic;

namespace FDK.Core.GameData
{
    public abstract class BaseGameDataCollection<T> where T : BaseGameData
    {
        public List<T> Collections;
        protected Dictionary<string, T> _gameDataDict = new();

        public void CreateDict()
        {
            if (Collections == null) Collections = new();

            foreach (var shop in Collections)
            {
                _gameDataDict.Add(shop.Id, shop);
            }
        }

        public T GetItem(string key)
        {
            if (_gameDataDict.TryGetValue(key, out var gameData))
            {
                return gameData;
            }
            return null;
        }
    }
}
