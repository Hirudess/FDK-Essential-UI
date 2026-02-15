using FDK.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SelectionLifetimeScope : LifetimeScope
{
    [SerializeField]
    private SelectableUIRef _selectableUIRef;
    [SerializeField]
    private GenericSelectableListData _selectableListData;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_selectableListData);
        builder.RegisterInstance(_selectableUIRef);
        builder.Register<GenericSelectionModel>(Lifetime.Singleton).As<ISelectionModel>();
        builder.RegisterEntryPoint<GenericSelectableListController>(Lifetime.Singleton).As<ISelectableController>();
    }
}
