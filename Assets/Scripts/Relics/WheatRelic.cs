using DI;
using Economy;
using Inventory;
using UnityEngine;

public class WheatRelic : RelicBase
{
    private ResourcesProductionSystem _resourcesProductionSystem;

    [Inject]
    public void Construct(ResourcesProductionSystem resourcesProductionSystem)
    {
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
        if (!IsActive || item.ResourceType != ResourceType.Wheat)
        {
            return;
        }

        int cellsCount = item.Cells.Length;
        var bonusAmount = cellsCount * amount - amount;
        _resourcesProductionSystem.AddResources(ResourceType.Wheat, bonusAmount);

        Debug.Log($"{this.GetType().Name} produce Wheat = {bonusAmount}");
    }
}
