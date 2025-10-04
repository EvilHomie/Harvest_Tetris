using DI;
using Economy;
using Inventory;
using UnityEngine;

public class ProjectInstaller : Installer
{
    [SerializeField] InventoryConfig _inventoryConfig;


    [SerializeField] InventoryGrid _inventoryGrid;
    [SerializeField] ResourcesPanel _resourcesPanel;
    [SerializeField] ResourcesCollectSystem _resourcesCollectSystem;
    protected override void InstallBindings()
    {
        Container.Bind<InventoryConfig>().FromInstance(_inventoryConfig);



        Container.Bind<InventoryGrid>().FromInstance(_inventoryGrid);
        Container.Bind<ResourcesPanel>().FromInstance(_resourcesPanel);
        Container.Bind<ResourcesCollectSystem>().FromInstance(_resourcesCollectSystem);
    }
}
