using FDK.GameData;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FDK.Core.GameData
{
    public abstract class BaseGameDataPreset<T, U> : BasePreset where T : BaseGameDataCollection<U> where U : BaseGameData
    {
        [Header("Load JSON")]
        public TextAsset JsonAsset;

        [SerializeField]
        protected string _pathFolder = "/Contents/Database/JSON/";
        [SerializeField]
        protected string _gameDataName = "Static-Data-Shops";

        public T GameData;

        public void SaveAsJson()
        {
            var path = $"{Application.dataPath}{_pathFolder}";
            if (!Directory.Exists(path))
            {
                Debug.LogError($"Path {path} isn't found.");
                return;
            }
            var filePath = Path.Combine(path, _gameDataName);
            var jsonData = JsonUtility.ToJson(GameData, prettyPrint: true);
            File.WriteAllText(filePath, jsonData);

            Debug.Log($"{this.GetType()} | Updated {filePath} ");
        }

        public void LoadJson()
        {
            if (JsonAsset == null) return;
            var gameData = JsonUtility.FromJson<T>(JsonAsset.text);
            if (gameData == null)
            {
                Debug.LogError($"FDK Core | Failed to get item data collection {nameof(GameDataCollectionService)}");
                return;
            }
            GameData = gameData;

            EditorUtility.SetDirty(this);
        }
    }
}
