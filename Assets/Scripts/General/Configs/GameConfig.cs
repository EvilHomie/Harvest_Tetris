using Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig")]
public class GameConfig : ScriptableObject
{
    [field: SerializeField] public Item[] Items { get; private set; }
    [field: SerializeField] public int StartItemsCount { get; private set; }
    [field: SerializeField] public Cost DestroyCost { get; private set; }
    [field: SerializeField] public ProductionModSet ProductionModSet { get; private set; }
    [field: SerializeField] public RelicBase[] Relics { get; private set; }

    [field: SerializeField] public Cost RelicStartCost { get; private set; }
    [field: SerializeField] public float IncreaseCostMod { get; private set; }
}

[Serializable]
public struct Cost
{
    [field: SerializeField] public GameResource[] RequiredResources { get; private set; }  
    
    public Cost(GameResource[] requiredResources)
    {
        RequiredResources = requiredResources;
    }

    public Cost(List<GameResource> requiredResources)
    {
        RequiredResources = requiredResources.ToArray();
    }
}

[Serializable]
public struct ProductionModSet
{
    [field: SerializeField] public ProductionMod[] ProductionMods { get; private set; }

    public readonly float GetMod(TileModifier tileModifier)
    {
        return ProductionMods.First(mod =>  mod.TileModifier == tileModifier).Mod;
    }
}

[Serializable]
public struct ProductionMod
{
    [field: SerializeField] public TileModifier TileModifier { get; private set; }
    [field: SerializeField] public float Mod { get; private set; }
}

[Serializable]
public struct GameResource
{
    [field: SerializeField] public ResourceType Type { get; private set; }
    [field: SerializeField] public int Amount { get; private set; }

    public GameResource(ResourceType resourceType, int amount)
    {
        Type = resourceType;
        Amount = amount;
    }
}