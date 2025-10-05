using DI;
using Economy;
using Inventory;
using UnityEngine;

public class BalanceRelic : RelicBase
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
        if (!IsActive)
        {
            return;
        }

        ResourceType smallestResType = ResourceType.Wood;
        int smallestAmount = int.MaxValue;

        foreach (var res in _resourcesProductionSystem.CollectedResources)
        {
            if (res.Value < smallestAmount)
            {
                smallestAmount = res.Value;
                smallestResType = res.Key;
            }
        }

        _resourcesProductionSystem.AddResources(smallestResType, 1);
        Debug.Log($"{this.GetType().Name} produce {smallestResType} = {1}");
    }
}
