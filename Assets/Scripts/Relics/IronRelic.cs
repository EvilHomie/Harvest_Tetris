using DI;
using Inventory;
using UnityEngine;

public class IronRelic : RelicBase
{
    private InventorySystem _inventorySystem;
    private ItemSpawnSystem _itemSpawnSystem;

    [Inject]
    public void Construct(InventorySystem inventorySystem, ItemSpawnSystem itemSpawnSystem)
    {
        _inventorySystem = inventorySystem;
        _itemSpawnSystem = itemSpawnSystem;
    }

    public override ResourceProductionContext ApplyEffects(ref ResourceProductionContext productionContext)
    {
        if (!IsActive || productionContext.ProducedResource.Type != ResourceType.Iron)
        {
            return productionContext;
        }

        var prodAmount = productionContext.ProducedResource.Amount;
        var bonusAmount = prodAmount * 10 - prodAmount;
        productionContext.ProducedResource.Add(bonusAmount);
        bool result = Random.value < 0.1f;

        Debug.Log($"{this.GetType().Name} produce Iron = {bonusAmount}");

        if (result)
        {
            var item = productionContext.ProductionItem;
            _inventorySystem.RemoveItem(item);
            Destroy(item.gameObject);
            _itemSpawnSystem.CreateItem();
        }

        return productionContext;
    }
}
