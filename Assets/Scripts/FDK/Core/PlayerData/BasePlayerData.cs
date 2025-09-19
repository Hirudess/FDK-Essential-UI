using System;

namespace FDK.Core
{
    [System.Serializable]
    public abstract class BasePlayerData
    {
        public string _guid;

        public BasePlayerData()
        {
            _guid = Guid.NewGuid().ToString();
        }
    }
}
