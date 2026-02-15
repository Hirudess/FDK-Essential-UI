using FDK.Equipment;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class FDKEquipmentLifetimeScope : LifetimeScope
{
    [SerializeField]
    private FDKEquipmentRef _equipmentRef;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_equipmentRef);
        builder.RegisterEntryPoint<FDKEquipmentManager>().As<IFDKEquipmentManager>();
    }
}
