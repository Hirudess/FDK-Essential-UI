
using UnityEngine;

namespace FDK.Core.Services
{
    public abstract class BaseService
    {
        public bool IsReady { get; protected set; }

        protected virtual void SetReady(bool isReady)
        {
            IsReady = isReady;
            Debug.Log($"FDK Core | Service : {this.GetType()} Ready");
        }
    }
}
