using FDK.Core.SaveFile;
using FDK.Core.Services;
using VContainer;

namespace FDK.Core
{
    public class PlayerGameplayDataService : BaseService
    {
        public PlayerSaveData PlayerGameplayData { get; private set; }

        [Preserve]
        public PlayerGameplayDataService()
        {
        }

        public void SetPlayerGameplayData(PlayerSaveData playerSaveData)
        {
            PlayerGameplayData = playerSaveData;
            SetReady(true);
        }
    }
}
