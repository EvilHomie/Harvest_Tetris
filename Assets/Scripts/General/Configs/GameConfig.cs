using Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig")]
public class GameConfig : ScriptableObject
{
    [field: SerializeField] public Item[] Items { get; private set; }
    [field: SerializeField] public int StartItemsCount { get; private set; }
    [field: SerializeField] public Cost DestroyCost { get; private set; }
    [field: SerializeField] public ProductionSpeed[] ProductionSpeeds { get; private set; }
    [field: SerializeField] public RelicBase[] Relics { get; private set; }

    [field: SerializeField] public Cost RelicStartCost { get; private set; }
    [field: SerializeField] public float IncreaseCostMod { get; private set; }
}

[Serializable]
public struct Cost
{
    [field: SerializeField] public RequiredResource[] RequiredResources { get; private set; }  
    
    public Cost(RequiredResource[] requiredResources)
    {
        RequiredResources = requiredResources;
    }

    public Cost(List<RequiredResource> requiredResources)
    {
        RequiredResources = requiredResources.ToArray();
    }
}

[Serializable]
public struct ProductionSpeed
{
    [field: SerializeField] public TileModifier TileModifier { get; private set; }
    [field: SerializeField] public float Mod { get; private set; }
}

[Serializable]
public struct RequiredResource
{
    [field: SerializeField] public ResourceType ResourceType { get; private set; }
    [field: SerializeField] public int Amount { get; private set; }

    public RequiredResource(ResourceType resourceType, int amount)
    {
        ResourceType = resourceType;
        Amount = amount;
    }
}