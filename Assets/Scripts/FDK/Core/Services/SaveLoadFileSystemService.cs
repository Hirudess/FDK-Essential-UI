using FDK.Core.Services;
using System.IO;
using UnityEngine;

namespace FDK.Core.SaveFile
{
    public interface ISaveLoadFileSystemService
    {
        void FirstTimeChecking();
        PlayerSaveData Load();
        void Save();
        void Delete();
    }

    public class SaveLoadFileSystemService : BaseService, ISaveLoadFileSystemService
    {
        private const string _filename = "player_save.json";
        private IPlayerGameplayDataService _gameplayDataService;


        [SerializeField]
        public SaveLoadFileSystemService(IPlayerGameplayDataService playerGameplayDataService)
        {
            SetReady(true);
            _gameplayDataService = playerGameplayDataService;
        }

        public void FirstTimeChecking()
        {
            Delete();
            var playerGameplayData = Load();
            _gameplayDataService.SetPlayerGameplayData(playerGameplayData);
        }

        private PlayerSaveData CreateNewSaveFile()
        {
            Debug.Log("Save Load Service | Create ");
            return new PlayerSaveData();
        }

        public PlayerSaveData Load()
        {
            string filePath = Path.Combine(Application.persistentDataPath, _filename);
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                PlayerSaveData playerData = JsonUtility.FromJson<PlayerSaveData>(jsonData);
                Debug.Log("Save Load Service | Load " + filePath);
                return playerData;
            }
            else
            {
                return CreateNewSaveFile();
            }
        }

        public void Save()
        {
            _gameplayDataService.PrepareSaving();

            var data = _gameplayDataService.PlayerSaveData;
            if (data == null) return;
            string filePath = Path.Combine(Application.persistentDataPath, _filename);
            string jsonData = JsonUtility.ToJson(data, prettyPrint: true);
            File.WriteAllText(filePath, jsonData);
            Debug.Log("Save Load Service | Save " + filePath);
        }

        public void Delete()
        {
            string filePath = Path.Combine(Application.persistentDataPath, _filename);
            if (File.Exists(filePath))
            {
                Debug.Log("Save Load Service | Delete " + filePath);
                File.Delete(filePath);
            }
        }
    }
}
