using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FDK.Inventory
{
    public class InventorySceneLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private InventoryHudRef _inventoryHudRef;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_inventoryHudRef);
            builder.RegisterEntryPoint<InventoryManager>(Lifetime.Singleton).As<IInventoryManager>();
        }
    }
}
