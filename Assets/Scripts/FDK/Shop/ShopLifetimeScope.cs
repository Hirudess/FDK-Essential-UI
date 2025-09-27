using FDK.Shop;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ShopLifetimeScope : LifetimeScope
{
    [SerializeField] private ShopManagerRef _shopManagerRef;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_shopManagerRef);
        builder.RegisterEntryPoint<ShopManager>().AsSelf();
    }
}
