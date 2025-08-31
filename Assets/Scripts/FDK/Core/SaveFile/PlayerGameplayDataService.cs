using FDK.Core.SaveFile;
using FDK.Core.Services;
using FDK.Inventory;
using VContainer;

namespace FDK.Core
{
    public interface IPlayerGameplayDataService
    {
        PlayerSaveData PlayerSaveData { get; }
        void SetPlayerGameplayData(PlayerSaveData playerSaveData);
        void PrepareSaving();
    }

    public class PlayerGameplayDataService : BaseService, IPlayerGameplayDataService
    {
        public PlayerSaveData PlayerSaveData { get; private set; }

        private readonly IPlayerItemInventoryService _playerItemInventoryService;

        [Preserve]
        public PlayerGameplayDataService(IPlayerItemInventoryService playerItemInventoryService)
        {
            _playerItemInventoryService = playerItemInventoryService;
            SetReady(true);
        }

        public void SetPlayerGameplayData(PlayerSaveData playerSaveData)
        {
            PlayerSaveData = playerSaveData;
            SetReady(true);
        }

        public void PrepareSaving()
        {
            CommitSaving();
        }

        private void WriteInventoryData()
        {
            if (PlayerSaveData == null) return;
            var inventory = _playerItemInventoryService.GetAllItems();
            PlayerSaveData.Inventory = new SaveFile.Inventory(inventory);
        }

        private void WritePlayerInfoData()
        {
            PlayerSaveData.PlayerInfo.PlaytimeHours += 1;
        }

        private void CommitSaving()
        {
            WriteInventoryData();
            WritePlayerInfoData();
        }
    }
}
