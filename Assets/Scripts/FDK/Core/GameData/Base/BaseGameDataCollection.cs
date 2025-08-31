using System.Collections.Generic;

namespace FDK.Core.GameData
{
    public abstract class BaseGameDataCollection<T> where T : BaseGameData
    {
        public List<T> Collections;
    }
}
