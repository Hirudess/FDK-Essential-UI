using FDK.Core.SaveFile;
using UnityEngine;
using VContainer;

namespace FDK.Core
{
    public class StartGameExample : MonoBehaviour
    {
        private ISaveLoadFileSystemService _fileSystemService;
        private bool _isInjected;

        [Inject]
        public void Inject(ISaveLoadFileSystemService saveLoadFileSystemService)
        {
            if (_isInjected) { return; }
            _isInjected = true;
            _fileSystemService = saveLoadFileSystemService;
        }

        private void Awake()
        {
            _fileSystemService.FirstTimeChecking();
        }
    }
}
