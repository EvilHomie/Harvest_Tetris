using DI;
using Economy;
using Inventory;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CommonRelic : RelicBase
{
    private ResourceSystem _resourceSystem;
    private InventorySystem _inventorySystem;
    private float _timer;
    private int _resourceTypeCount;
    private GameFlowSystem _gameFlowSystem;

    [Inject]
    public void Construct(GameFlowSystem gameFlowSystem, InventorySystem inventorySystem, ResourceSystem resourceSystem)
    {
        _resourceTypeCount = Enum.GetValues(typeof(ResourceType)).Length;
        _gameFlowSystem = gameFlowSystem;
        _inventorySystem = inventorySystem;
        _resourceSystem = resourceSystem;
    }

    private void OnEnable()
    {
        _gameFlowSystem.CustomUpdate += AddBonusResources;
    }

    private void OnDisable()
    {
        _gameFlowSystem.CustomUpdate += AddBonusResources;
    }

    private void AddBonusResources(float tickTime)
    {
        if (!IsActive || _inventorySystem.PlacedItems.Count < 3)
        {
            return;
        }

        _timer -= tickTime;

        if (_timer > 0)
        {
            return;
        }

        _timer = 1f;
        ResourceType randomType = (ResourceType)Random.Range(0, _resourceTypeCount);
        var bonusRes = new GameResource(randomType, 1);
        _resourceSystem.AddResource(bonusRes);
        Debug.Log($"{this.GetType().Name} produce {randomType} = {1}");
    }
    public override ResourceProductionContext ApplyEffects(ref ResourceProductionContext productionContext)
    {
        return productionContext;
    }
}
