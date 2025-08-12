using FDK.Core.SaveFile;
using FDK.Core.Services;
using VContainer;

namespace FDK.Core
{
    public interface IPlayerGameplayDataService
    {
        PlayerSaveData PlayerGameplayData { get; }
        void SetPlayerGameplayData(PlayerSaveData playerSaveData);
    }

    public class PlayerGameplayDataService : BaseService, IPlayerGameplayDataService
    {
        public PlayerSaveData PlayerGameplayData { get; private set; }

        [Preserve]
        public PlayerGameplayDataService()
        {
            SetReady(true);
        }

        public void SetPlayerGameplayData(PlayerSaveData playerSaveData)
        {
            PlayerGameplayData = playerSaveData;
            SetReady(true);
        }
    }
}
