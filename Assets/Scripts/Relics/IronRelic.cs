using DI;
using Economy;
using Inventory;
using UnityEngine;

public class IronRelic : RelicBase
{
    //private InventorySystem _inventorySystem;
    //private ItemSpawnSystem _itemSpawnSystem;
    //private ResourceProducer _resourcesProductionSystem;

    //[Inject]
    //public void Construct(InventorySystem inventorySystem, ItemSpawnSystem itemSpawnSystem, ResourceProducer resourcesProductionSystem)
    //{
    //    _inventorySystem = inventorySystem;
    //    _itemSpawnSystem = itemSpawnSystem;
    //    _resourcesProductionSystem = resourcesProductionSystem;
    //}

    //private void OnEnable()
    //{
    //    //_resourcesProductionSystem.ResourceCollect += OnResourceCollect;
    //}

    //private void OnDisable()
    //{
    //    //_resourcesProductionSystem.ResourceCollect -= OnResourceCollect;
    //}

    //private void OnResourceCollect(Item item, int amount)
    //{
    //    if (!IsActive || item.ResourceType != ResourceType.Iron)
    //    {
    //        return;
    //    }

    //    var bonusAmount = amount * 10 - amount;
    //    bool result = Random.value < 0.1f;

    //    //_resourcesProductionSystem.AddResources(ResourceType.Iron, bonusAmount);
    //    Debug.Log($"{this.GetType().Name} produce Iron = {bonusAmount}");

    //    if (result)
    //    {
    //        _inventorySystem.RemoveItem(item);
    //        Destroy(item.gameObject);
    //        _itemSpawnSystem.CreateItem();
    //    }
    //}
}
