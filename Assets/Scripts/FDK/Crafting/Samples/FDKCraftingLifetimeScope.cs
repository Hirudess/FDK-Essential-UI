using FDK.Crafting;
using FDK.Shop;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class FDKCraftingLifetimeScope : LifetimeScope
{
    [SerializeField] private CraftingShopManagerRef _craftManagerRef;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_craftManagerRef);
        builder.RegisterEntryPoint<CraftingManager>().AsSelf();
    }
}
