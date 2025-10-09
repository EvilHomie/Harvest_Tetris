using DI;
using Economy;
using Inventory;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CommonRelic : RelicBase
{
    //private ResourceProducer _resourcesProductionSystem;
    //private InventorySystem _inventorySystem;

    //private float _timer;
    //private int _resourceTypeCount;

    //[Inject]
    //public void Construct(InventorySystem inventorySystem, ResourceProducer resourcesProductionSystem)
    //{
    //    _inventorySystem = inventorySystem;
    //    _resourcesProductionSystem = resourcesProductionSystem;
    //}
    //void Awake()
    //{
    //    _resourceTypeCount = Enum.GetValues(typeof(ResourceType)).Length;
    //}

    //private void Update()
    //{
    //    AddBonusRes();
    //}

    //private void AddBonusRes()
    //{
    //    if (!IsActive || _inventorySystem.PlacedItems.Count < 3)
    //    {
    //        return;
    //    }

    //    _timer -= Time.deltaTime;

    //    if (_timer > 0)
    //    {
    //        return;
    //    }

    //    _timer = 1f;
    //    ResourceType randomType = (ResourceType)Random.Range(0, _resourceTypeCount);
    //    _resourcesProductionSystem.AddResources(randomType, 1);
    //    Debug.Log($"{this.GetType().Name} produce {randomType} = {1}");
    //}
}
