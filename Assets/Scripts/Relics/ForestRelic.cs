using DI;
using Economy;
using Inventory;
using UnityEngine;

public class ForestRelic : RelicBase
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
        if (!IsActive || item.ResourceType != ResourceType.Wood)
        {
            return;
        }

        bool result = Random.value < 0.5f;

        if (result)
        {
            _resourcesProductionSystem.AddResources(ResourceType.Wheat, amount);
            Debug.Log($"{this.GetType().Name} produce Wheat = {amount}");
        }

    }
}
