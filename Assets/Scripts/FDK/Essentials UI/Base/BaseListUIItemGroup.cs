using System.Collections.Generic;
using UnityEngine;

namespace FDK.UI.Base
{
    public class BaseListUIItemGroup<T> : BaseUIPanel where T : BaseUiItem
    {
        [SerializeField] protected T ItemPrefab;
        [SerializeField] protected Transform Content;

        protected List<T> Items = new();
    }
}
