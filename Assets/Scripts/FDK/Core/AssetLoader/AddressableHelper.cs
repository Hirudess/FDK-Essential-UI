
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine.AddressableAssets;
using static UnityEngine.AddressableAssets.Addressables;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

#nullable enable

namespace Agate.Module.AddressablesHelper
{
    public static class AddressablesHelper
    {
        private static PropertyInfo? _addressablesProperty = null;
        private static PropertyInfo AddressablesProperty
        {
            get
            {
                if (_addressablesProperty == null)
                {
                    _addressablesProperty = typeof(Addressables).GetProperty(
                        "m_Addressables",
                        BindingFlags.Static | BindingFlags.NonPublic);
                }
                return _addressablesProperty;
            }
        }

        private static FieldInfo? _addressablesField = null;
        private static FieldInfo AddressablesField
        {
            get
            {
                if (_addressablesField == null)
                {
                    _addressablesField = typeof(Addressables).GetField(
                        "m_AddressablesInstance",
                        BindingFlags.Static | BindingFlags.NonPublic);
                }
                return _addressablesField;
            }
        }

        private static FieldInfo? _hasStartedInitializationField = null;
        private static FieldInfo HasStartedInitializationField
        {
            get
            {
                if (_hasStartedInitializationField == null)
                {
                    _hasStartedInitializationField = AddressablesImplType.GetField(
                        "hasStartedInitialization",
                        BindingFlags.Instance | BindingFlags.NonPublic);
                }
                return _hasStartedInitializationField;
            }
        }

        private static FieldInfo? _initializationOperationField = null;
        private static FieldInfo InitializationOperationField
        {
            get
            {
                if (_initializationOperationField == null)
                {
                    _initializationOperationField = AddressablesImplType.GetField(
                        "m_InitializationOperation",
                        BindingFlags.Instance | BindingFlags.NonPublic);
                }
                return _initializationOperationField;
            }
        }

        private static Type AddressablesImplType => AddressablesProperty.PropertyType;

        private static bool _hasStartedInitialization = false;
        private static bool _hasFinishedInitialization = false;

        /// <summary>
        /// Note: Uses reflection
        /// </summary>
        public static bool HasStartedInitialization
        {
            get
            {
                if (_hasStartedInitialization)
                {
                    return true;
                }
                var instance = AddressablesProperty.GetValue(null);
                if (instance != null)
                {
                    var value = (bool)HasStartedInitializationField.GetValue(instance);
                    if (value)
                    {
                        _hasStartedInitialization = true;
                    }
                    return value;
                }
                return false;
            }
        }

        /// <summary>
        /// Note: Uses reflection
        /// </summary>
        public static bool HasFinishedInitialization
        {
            get
            {
                if (_hasFinishedInitialization)
                {
                    return true;
                }
                if (HasStartedInitialization)
                {
                    var instance = AddressablesProperty.GetValue(null);
                    if (instance != null)
                    {
                        var m_InitializationOperation = (AsyncOperationHandle<IResourceLocator>)InitializationOperationField.GetValue(instance);
                        var isDone = !m_InitializationOperation.IsValid() || m_InitializationOperation.IsDone;
                        if (isDone)
                        {
                            _hasFinishedInitialization = true;
                        }
                        return isDone;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Will be called after <see cref="ReinitializeAddressables"/>.
        /// </summary>
        public static event Action? OnReinitializedAddressables;

        /// <summary>
        /// Reinitialize the addressables instance, will trigger <see cref="OnReinitializedAddressables"/>.
        /// <br></br>
        /// Note: on Runtime, reinitialization does nothing to the Addressables system itself
        /// (but will still trigger <see cref="OnReinitializedAddressables"/>).
        /// </summary>
        /// <returns></returns>
        public static AsyncOperationHandle<IResourceLocator> ReinitializeAddressables()
        {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
            {
                // Release previous instance of m_AddressablesInstance (DO m_AddressablesInstance.ReleaseSceneManagerOperation();)
                var prevInstance = AddressablesProperty.GetValue(null);
                var releaseMethod = AddressablesImplType.GetMethod("ReleaseSceneManagerOperation", BindingFlags.Instance | BindingFlags.NonPublic);
                releaseMethod.Invoke(prevInstance, null);

                // Create new m_AddressablesInstance
                var allocationStrategy = new UnityEngine.ResourceManagement.Util.LRUCacheAllocationStrategy(1000, 1000, 100, 10);
                var newInstance = Activator.CreateInstance(AddressablesImplType, new object[] { allocationStrategy });
                AddressablesField.SetValue(null, newInstance);
            }
#endif

            var handle = Addressables.InitializeAsync();
            _hasStartedInitialization = true;
            handle.Completed += handle =>
            {
                _hasFinishedInitialization = true;
                OnReinitializedAddressables?.Invoke();
            };

#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
            {
                handle.WaitForCompletion();
            }
#endif
            return handle;
        }

        #region Utils

        public static async UniTask<T> WaitAsync<T>(this AsyncOperationHandle<T> handle, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                if (handle.IsValid())
                    Addressables.Release(handle);

                cancellationToken.ThrowIfCancellationRequested();
            }

            var (isCanceled, result) = await handle
                .WithCancellation(cancellationToken)
                .SuppressCancellationThrow();

            if (isCanceled && handle.IsValid())
                Addressables.Release(handle);

            cancellationToken.ThrowIfCancellationRequested();

            return result;
        }

        public static async UniTask WaitAsync(this AsyncOperationHandle handle, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Addressables.Release(handle);
                cancellationToken.ThrowIfCancellationRequested();
            }


            var isCanceled = await handle
                .WithCancellation(cancellationToken)

                .SuppressCancellationThrow();

            if (isCanceled)
                Addressables.Release(handle);

            cancellationToken.ThrowIfCancellationRequested();
        }

        public readonly struct ValidateResult
        {
            public readonly bool IsValid;
            public readonly IList<IResourceLocation>? Locations;

            private ValidateResult(bool isValid, IList<IResourceLocation>? locations) : this()
            {
                IsValid = isValid;
                Locations = locations;
            }

            public ValidateResult(IList<IResourceLocation>? locations) : this()
            {
                IsValid = locations != null && locations.Count > 0;
                Locations = locations;
            }

            public void Deconstruct(out bool isValid, out IList<IResourceLocation>? locations)
            {
                isValid = IsValid;
                locations = Locations;
            }

            public void Deconstruct(out bool isValid)
            {
                isValid = IsValid;
            }

            public static implicit operator ValidateResult((bool IsValid, IList<IResourceLocation>? Locations) obj)
            {
                return new ValidateResult(obj.IsValid, obj.Locations);
            }

            public static implicit operator bool(ValidateResult obj)
            {
                return obj.IsValid;
            }
        }

        public static async
            UniTask
        <ValidateResult> ValidateAsync(
            object key,
            Type? type = null,
            bool forceSynchronous = false,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var locationHandle = Addressables.LoadResourceLocationsAsync(
                key,
                type);

            if (forceSynchronous)
            {
                locationHandle.WaitForCompletion();
            }

            var locations = await locationHandle.WaitAsync(cancellationToken);
            Addressables.Release(locationHandle);

            cancellationToken.ThrowIfCancellationRequested();

            return new ValidateResult(locations);
        }

        public static
            UniTask

        <ValidateResult> ValidateAsync<TObject>(
            object key,
            bool forceSynchronous = false,
            CancellationToken cancellationToken = default)
        {
            return ValidateAsync(
                key,
                typeof(TObject),
                forceSynchronous,
                cancellationToken);
        }

        public static async
            UniTask

        <ValidateResult> ValidateAsync(
            IEnumerable keys,
            MergeMode mode,
            Type? type = null,
            bool forceSynchronous = false,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var locationHandle = Addressables.LoadResourceLocationsAsync(
                keys,
                mode,
                type);

            if (forceSynchronous)
            {
                locationHandle.WaitForCompletion();
            }

            var locations = await locationHandle.WaitAsync(cancellationToken);
            Addressables.Release(locationHandle);

            cancellationToken.ThrowIfCancellationRequested();

            return new ValidateResult(locations);
        }

        public static
            UniTask

        <ValidateResult> ValidateAsync<TObject>(
            IEnumerable keys,
            MergeMode mode,
            bool forceSynchronous = false,
            CancellationToken cancellationToken = default)
        {
            return ValidateAsync(
                keys,
                mode,
                typeof(TObject),
                forceSynchronous,
                cancellationToken);
        }

        public readonly struct ValidateThenLoadResult<TObject>
        {
            public readonly bool IsValid;
            public readonly IResourceLocation? Location;
            public readonly AsyncOperationHandle<TObject> Handle;

            private ValidateThenLoadResult(
                bool isValid,
                IResourceLocation? location,
                AsyncOperationHandle<TObject> handle) : this()
            {
                IsValid = isValid;
                Location = location;
                Handle = handle;
            }

            public ValidateThenLoadResult(
                IResourceLocation? location,
                AsyncOperationHandle<TObject> handle) : this()
            {
                IsValid = handle.IsValid() && location != null;
                Location = location;
                Handle = handle;
            }

            public void Deconstruct(
                out bool isValid,
                out IResourceLocation? location,
                out AsyncOperationHandle<TObject> handle)
            {
                isValid = IsValid;
                location = Location;
                handle = Handle;
            }

            public void Deconstruct(
                out bool isValid,
                out AsyncOperationHandle<TObject> handle)
            {
                isValid = IsValid;
                handle = Handle;
            }

            public static implicit operator AsyncOperationHandle<TObject>(ValidateThenLoadResult<TObject> obj)
            {
                return obj.Handle;
            }

            public static implicit operator ValidateThenLoadResult<TObject>((bool IsValid, IResourceLocation? Location, AsyncOperationHandle<TObject> Handle) obj)
            {
                return new ValidateThenLoadResult<TObject>(obj.IsValid, obj.Location, obj.Handle);
            }

            public static implicit operator ValidateThenLoadResult<TObject>((IResourceLocation? Location, AsyncOperationHandle<TObject> Handle) obj)
            {
                return new ValidateThenLoadResult<TObject>(obj.Location, obj.Handle);
            }
        }

        public readonly struct ValidateThenLoadResults<TObject>
        {
            public readonly bool IsValid;
            public readonly IList<IResourceLocation>? Locations;
            public readonly AsyncOperationHandle<IList<TObject>> Handle;

            private ValidateThenLoadResults(
                bool isValid,
                IList<IResourceLocation>? locations,
                AsyncOperationHandle<IList<TObject>> handle) : this()
            {
                IsValid = isValid;
                Locations = locations;
                Handle = handle;
            }

            public ValidateThenLoadResults(
                IList<IResourceLocation>? locations,
                AsyncOperationHandle<IList<TObject>> handle) : this()
            {
                IsValid = handle.IsValid() && locations != null && locations.Count > 0;
                Locations = locations;
                Handle = handle;
            }

            public void Deconstruct(
                out bool isValid,
                out IList<IResourceLocation>? locations,
                out AsyncOperationHandle<IList<TObject>> handle)
            {
                isValid = IsValid;
                locations = Locations;
                handle = Handle;
            }

            public void Deconstruct(
                out bool isValid,
                out AsyncOperationHandle<IList<TObject>> handle)
            {
                isValid = IsValid;
                handle = Handle;
            }

            public static implicit operator AsyncOperationHandle<IList<TObject>>(ValidateThenLoadResults<TObject> obj)
            {
                return obj.Handle;
            }

            public static implicit operator ValidateThenLoadResults<TObject>((bool IsValid, IList<IResourceLocation>? Locations, AsyncOperationHandle<IList<TObject>> Handle) obj)
            {
                return new ValidateThenLoadResults<TObject>(obj.IsValid, obj.Locations, obj.Handle);
            }

            public static implicit operator ValidateThenLoadResults<TObject>((IList<IResourceLocation>? Locations, AsyncOperationHandle<IList<TObject>> Handle) obj)
            {
                return new ValidateThenLoadResults<TObject>(obj.Locations, obj.Handle);
            }
        }

        public static async UniTask<ValidateThenLoadResult<TObject>> ValidateThenLoadAssetAsync<TObject>(
            object key,
            bool forceSynchronous = false,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var locationHandle = Addressables.LoadResourceLocationsAsync(
                key,
                typeof(TObject));

            if (forceSynchronous)
            {
                locationHandle.WaitForCompletion();
            }

            var locations = await locationHandle.WaitAsync(cancellationToken);
            Addressables.Release(locationHandle);

            cancellationToken.ThrowIfCancellationRequested();

            if (locations.Count > 0)
            {
                var location = locations[0];
                var assetHandle = Addressables.LoadAssetAsync<TObject>(location);
                return new ValidateThenLoadResult<TObject>(location, assetHandle);
            }
            return default;
        }

        public static UniTask<ValidateThenLoadResults<TObject>> ValidateThenLoadAssetsAsync<TObject>(
            object key,
            Action<TObject>? callback,
            CancellationToken cancellationToken = default)
        {
            return ValidateThenLoadAssetsAsync(
                key,
                callback,
                true,
                false,
                cancellationToken);
        }

        public static UniTask<ValidateThenLoadResults<TObject>> ValidateThenLoadAssetsAsync<TObject>(
            object key,
            Action<TObject>? callback,
            bool forceSynchronous,
            CancellationToken cancellationToken = default)
        {
            return ValidateThenLoadAssetsAsync(
                key,
                callback,
                true,
                forceSynchronous,
                cancellationToken);
        }

        public static async UniTask<ValidateThenLoadResults<TObject>> ValidateThenLoadAssetsAsync<TObject>(
            object key,
            Action<TObject>? callback,
            bool releaseDependenciesOnFailure,
            bool forceSynchronous,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var locationHandle = Addressables.LoadResourceLocationsAsync(
                key,
                typeof(TObject));

            if (forceSynchronous)
            {
                locationHandle.WaitForCompletion();
            }

            var locations = await locationHandle.WaitAsync(cancellationToken);
            Addressables.Release(locationHandle);

            cancellationToken.ThrowIfCancellationRequested();

            if (locations.Count > 0)
            {
                var assetHandle = Addressables.LoadAssetsAsync<TObject>(
                    locations,
                    callback,
                    releaseDependenciesOnFailure);

                return new ValidateThenLoadResults<TObject>(locations, assetHandle);
            }
            return new ValidateThenLoadResults<TObject>(locations, default);
        }
        #endregion
    }
}

#nullable restore
