using DI;
using Inventory;
using UnityEngine;

public class ProjectInstaller : Installer
{
    [SerializeField] InventoryGrid _inventoryGrid;
    protected override void InstallBindings()
    {
        Container.Bind<InventoryGrid>().FromInstance(_inventoryGrid);
    }
}
