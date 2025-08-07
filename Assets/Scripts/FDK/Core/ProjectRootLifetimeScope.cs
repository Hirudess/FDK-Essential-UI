using Assets.Scripts.FDK.Essentials_UI.Shop;
using FDK.Core;
using FDK.Dialogue;
using FDK.GameData;
using FDK.Inventory;
using FDK.Notification;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectRootLifetimeScope : LifetimeScope
{
    [SerializeField] private GameDataCollectionRef _gameDataCollectionRef;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_gameDataCollectionRef);
        builder.RegisterEntryPoint<GameDataCollectionService>(Lifetime.Singleton).As<IGameDataCollectionService>();
        builder.RegisterEntryPoint<CurrencySystem>(Lifetime.Singleton).As<ICurrencySystem>();
        builder.RegisterEntryPoint<PlayerItemInventoryService>(Lifetime.Singleton).As<IPlayerItemInventoryService>();
        builder.RegisterEntryPoint<PlayerCharacterInventoryService>(Lifetime.Singleton).As<IPlayerCharacterInventoryService>();
        builder.RegisterEntryPoint<TransactionSystem>(Lifetime.Singleton).As<ITransactionSystem>();

        builder.RegisterEntryPoint<DialogueService>(Lifetime.Singleton).As<IDialogueService>();
        builder.RegisterEntryPoint<GlobalNotificationService>(Lifetime.Singleton).As<IGlobalNotificationService>();
    }
}
