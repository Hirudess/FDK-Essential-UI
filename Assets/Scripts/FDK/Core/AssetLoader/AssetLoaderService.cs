using Cysharp.Threading.Tasks;
using FDK.Core.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FDK.Core.AssetLoader
{
    public class AssetLoaderService : BaseService
    {
        public AssetLoaderService()
        {

        }


        private async UniTask<GameObject> LoadGameObjectAsync(string assetAddress)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(assetAddress);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject loadedPrefab = handle.Result;
                return loadedPrefab;
            }
            else
            {
                Debug.LogError($"Failed to load asset: {assetAddress} - {handle.OperationException}");
            }

            Addressables.Release(handle);
            return null;
        }
    }
}
