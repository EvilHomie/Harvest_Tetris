using DI;
using Economy;
using Inventory;
using UnityEngine;

public class ProjectInstaller : Installer
{
    [SerializeField] InventoryGrid _inventoryGrid;
    [SerializeField] ResourcesPanel _resourcesPanel;
    [SerializeField] ResourcesCollectSystem _resourcesCollectSystem;
    protected override void InstallBindings()
    {
        Container.Bind<InventoryGrid>().FromInstance(_inventoryGrid);
        Container.Bind<ResourcesPanel>().FromInstance(_resourcesPanel);
        Container.Bind<ResourcesCollectSystem>().FromInstance(_resourcesCollectSystem);
    }
}
