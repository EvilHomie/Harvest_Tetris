using DI;
using Economy;
using Inventory;
using UnityEngine;

public class ProjectInstaller : Installer
{
    [SerializeField] InventoryConfig _inventoryConfig;
    [SerializeField] GameConfig _gameConfig;
    [SerializeField] SpawnArea _spawnArea;
    [SerializeField] Canvas _canvas;

    [SerializeField] InventoryGrid _inventoryGrid;
    [SerializeField] ResourcesPanel _resourcesPanel;
    [SerializeField] ResourcesCollectSystem _resourcesCollectSystem;
    protected override void InstallBindings()
    {
        Container.Bind<InventoryConfig>().FromInstance(_inventoryConfig);
        Container.Bind<SpawnArea>().FromInstance(_spawnArea);
        Container.Bind<GameConfig>().FromInstance(_gameConfig);
        Container.Bind<Canvas>().FromInstance(_canvas);



        Container.Bind<InventoryGrid>().FromInstance(_inventoryGrid);
        Container.Bind<ResourcesPanel>().FromInstance(_resourcesPanel);
        Container.Bind<ResourcesCollectSystem>().FromInstance(_resourcesCollectSystem);
    }
}
