using DI;
using Economy;
using Inventory;
using UnityEngine;

public class ProjectInstaller : Installer
{
    [SerializeField] InventoryConfig _inventoryConfig;
    [SerializeField] GameConfig _gameConfig;
    [SerializeField] ItemConfig _itemConfig;

    [SerializeField] ItemSpawnSystem _spawnArea;
    [SerializeField] Canvas _canvas;
    [SerializeField] ResourcesPanel _resourcesPanel;
    [SerializeField] ResourcesProductionSystem _resourcesCollectSystem;
    [SerializeField] Camera _camera;
    [SerializeField] DestroyItemSystem _destroyItemSystem;
    [SerializeField] InventorySystem _inventorySystem;
    protected override void InstallBindings()
    {
        Container.Bind<InventoryConfig>().FromInstance(_inventoryConfig);
        Container.Bind<GameConfig>().FromInstance(_gameConfig);
        Container.Bind<ItemConfig>().FromInstance(_itemConfig);
        Container.Bind<ItemSpawnSystem>().FromInstance(_spawnArea);
        Container.Bind<Canvas>().FromInstance(_canvas);
        Container.Bind<ResourcesPanel>().FromInstance(_resourcesPanel);
        Container.Bind<ResourcesProductionSystem>().FromInstance(_resourcesCollectSystem);
        Container.Bind<Camera>().FromInstance(_camera);
        Container.Bind<DestroyItemSystem>().FromInstance(_destroyItemSystem);
        Container.Bind<InventorySystem>().FromInstance(_inventorySystem);
    }
}