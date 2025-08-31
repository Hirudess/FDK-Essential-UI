using FDK;
using UnityEngine;

namespace TacticsRPG.Game.GameData
{
    public interface IGameData
    {
        string ID { get; }
    }

    public abstract class BaseGameData : BasePreset, IGameData
    {
        [SerializeField] protected string _id;
        public string ID => _id;
    }
}
