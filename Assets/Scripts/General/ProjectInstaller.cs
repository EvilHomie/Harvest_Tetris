using DI;
using Economy;
using Inventory;
using UnityEngine;

public class ProjectInstaller : Installer
{
    [SerializeField] InventoryConfig _inventoryConfig;
    [SerializeField] GameConfig _gameConfig;
    [SerializeField] ItemConfig _itemConfig;

    [SerializeField] GameFlowSystem _gameFlowSystem;
    [SerializeField] ItemSpawnSystem _spawnArea;
    [SerializeField] Canvas _canvas;
    [SerializeField] ResourcesPanel _resourcesPanel;
    [SerializeField] ResourceSystem _resourceSystem;
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
        Container.Bind<ResourceSystem>().FromInstance(_resourceSystem);
        Container.Bind<Camera>().FromInstance(_camera);
        Container.Bind<DestroyItemSystem>().FromInstance(_destroyItemSystem);
        Container.Bind<InventorySystem>().FromInstance(_inventorySystem);
        Container.Bind<GameFlowSystem>().FromInstance(_gameFlowSystem);
    }
}