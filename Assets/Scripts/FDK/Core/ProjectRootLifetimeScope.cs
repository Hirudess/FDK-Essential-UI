using FDK.Core;
using FDK.Core.SaveFile;
using FDK.Dialogue;
using FDK.GameData;
using FDK.Inventory;
using FDK.Notification;
using FDK.Shop;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectRootLifetimeScope : LifetimeScope
{
    [SerializeField] private GameDataCollectionRef _gameDataCollectionRef;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_gameDataCollectionRef);
        builder.Register<GameDataCollectionService>(Lifetime.Singleton).As<IGameDataCollectionService>();
        builder.Register<SaveLoadFileSystemService>(Lifetime.Singleton).As<ISaveLoadFileSystemService>();
        builder.Register<PlayerGameplayDataService>(Lifetime.Singleton).As<IPlayerGameplayDataService>();

        builder.RegisterEntryPoint<CurrencySystem>(Lifetime.Singleton).As<ICurrencySystem>();
        builder.RegisterEntryPoint<PlayerItemInventoryService>(Lifetime.Singleton).As<IPlayerItemInventoryService>();
        builder.RegisterEntryPoint<TransactionSystem>(Lifetime.Singleton).As<ITransactionSystem>();

        builder.RegisterEntryPoint<DialogueService>(Lifetime.Singleton).As<IDialogueService>();
        builder.RegisterEntryPoint<GlobalNotificationService>(Lifetime.Singleton).As<IGlobalNotificationService>();
    }
}
