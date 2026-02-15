using FDK.Core;
using FDK.GameData;
using UnityEngine.Scripting;
using VContainer.Unity;

namespace FDK.Equipment
{
    [System.Serializable]
    public struct FDKEquipmentRef
    {
        public FDKWeaponEquipSelectableUI WeaponSlot;
        public FDKArmorEquipSelectableUI ArmorSlot;
        public FDKEquipmentSelectionUI Selection;
    }

    public interface IFDKEquipmentManager
    {
        void EquipArmor(string armorId);
        void EquipWeapon(string weaponId);
        void Initialize();
        void UnequipArmor();
        void UnequipWeapon();
    }

    public class FDKEquipmentManager : IStartable, IFDKEquipmentManager
    {
        private readonly FDKWeaponEquipSelectableUI _weaponSlot;
        private readonly FDKArmorEquipSelectableUI _armorSlot;
        private readonly FDKEquipmentSelectionUI _selection;

        private readonly IPlayerGameplayDataService _playerGameplayDataService;
        private readonly IGameDataCollectionService _gameDataCollectionService;

        private EquipLocation _equipLocation;

        [Preserve]
        public FDKEquipmentManager(IPlayerGameplayDataService playerGameplayDataService, IGameDataCollectionService gameDataCollectionService, FDKEquipmentRef equipmentRef)
        {
            _playerGameplayDataService = playerGameplayDataService;
            _gameDataCollectionService = gameDataCollectionService;

            _selection = equipmentRef.Selection;
            _weaponSlot = equipmentRef.WeaponSlot;
            _armorSlot = equipmentRef.ArmorSlot;
            Initialize();
        }

        public void Initialize()
        {
            _weaponSlot.Initialize(_ => SelectWeapon());
            _armorSlot.Initialize(_ => SelectArmor());
            var containers = new BaseEquipmentSlotUI[2]
            {
                _weaponSlot,
                _armorSlot,
            };

            _selection.Initialize(containers);
        }

        public void EquipWeapon(string weaponId)
        {
            var item = _gameDataCollectionService.ItemCollection.GetItem(weaponId);
            if (item == null) return;
            _playerGameplayDataService.PlayerSaveData.PlayerEquip.EquipWeapon(item.Id);
            var weaponSlotUIData = new WeaponSelectableUIData(item.Id, item.Name, item.Description);
        }

        public void UnequipWeapon()
        {
            _playerGameplayDataService.PlayerSaveData.PlayerEquip.EquipWeapon(null);
        }

        public void EquipArmor(string armorId)
        {
            var item = _gameDataCollectionService.ItemCollection.GetItem(armorId);
            if (item == null) return;

            _playerGameplayDataService.PlayerSaveData.PlayerEquip.EquipArmor(item.Id);
            var armorSelectableUIData = new ArmorSelectableUIData(item.Id, item.Name, item.Description);
            _armorSlot.UpdateGameData(armorSelectableUIData);
        }

        public void UnequipArmor()
        {
            _playerGameplayDataService.PlayerSaveData.PlayerEquip.EquipArmor(null);
            _armorSlot.UpdateGameData(null);
        }

        private void SelectWeapon()
        {
            _equipLocation = EquipLocation.Weapon;
            UnityEngine.Debug.LogError("Current Selected" + _equipLocation);
        }
        private void SelectArmor()
        {
            _equipLocation = EquipLocation.Body;
            UnityEngine.Debug.LogError("Current Selected" + _equipLocation);
        }

        public void Start()
        {
            Initialize();
        }
    }
}
