#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace FDK
{
    /// <summary>
    /// Base class for properties preset assets.
    /// </summary>
    public abstract class BasePreset : ScriptableObject
    {
#if UNITY_EDITOR

        [ContextMenu("Save")]
        public void Save()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        protected virtual void OnValidate()
        {
            EditorUtility.SetDirty(this);
        }

#endif
    }
}
