using FDK.Core.Services;
using System.IO;
using UnityEngine;

namespace FDK.Core.SaveFile
{
    public interface ISaveLoadFileSystemService
    {
        void FirstTimeChecking();
        PlayerSaveData Load();
        void Save(PlayerSaveData data);
    }

    public class SaveLoadFileSystemService : BaseService, ISaveLoadFileSystemService
    {
        private const string _filename = "player_save.json";

        [SerializeField]
        public SaveLoadFileSystemService()
        {
            SetReady(true);
        }

        public void FirstTimeChecking()
        {
            Load();
        }

        private PlayerSaveData CreateNewSaveFile()
        {
            return new PlayerSaveData();
        }

        public PlayerSaveData Load()
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, _filename);
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                PlayerSaveData playerData = JsonUtility.FromJson<PlayerSaveData>(jsonData);
                Debug.Log("Loaded player: " + playerData.PlayerInfo.PlayerName);
                return playerData;
            }
            else
            {
                return CreateNewSaveFile();
            }
        }

        public void Save(PlayerSaveData data)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, _filename);
            string jsonData = JsonUtility.ToJson(data, prettyPrint: true);
            File.WriteAllText(filePath, jsonData);
            Debug.Log("Saved to: " + filePath);
        }
    }
}
