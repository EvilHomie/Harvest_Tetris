using DI;
using Economy;
using Inventory;
using UnityEngine;

public class IronRelic : RelicBase
{
    private InventorySystem _inventorySystem;
    private ItemSpawnerSystem _spawnerSystem;
    private ResourcesProductionSystem _resourcesProductionSystem;

    [Inject]
    public void Construct(InventorySystem inventorySystem, ItemSpawnerSystem spawnerSystem, ResourcesProductionSystem resourcesProductionSystem)
    {
        _inventorySystem = inventorySystem;
        _spawnerSystem = spawnerSystem;
        _resourcesProductionSystem = resourcesProductionSystem;
    }

    private void OnEnable()
    {
        _resourcesProductionSystem.ResourceCollect += OnResourceCollect;
    }

    private void OnDisable()
    {
        _resourcesProductionSystem.ResourceCollect -= OnResourceCollect;
    }

    private void OnResourceCollect(Item item, int amount)
    {
        if (!IsActive || item.ResourceType != ResourceType.Iron)
        {
            return;
        }

        var bonusAmount = amount * 10 - amount;
        bool result = Random.value < 0.1f;

        _resourcesProductionSystem.AddResources(ResourceType.Iron, bonusAmount);
        Debug.Log($"{this.GetType().Name} produce Iron = {bonusAmount}");

        if (result)
        {
            _inventorySystem.RemoveItem(item);
            Destroy(item.gameObject);
            _spawnerSystem.CreateItem();
        }
    }
}
