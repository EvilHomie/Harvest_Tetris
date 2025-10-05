using Inventory;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig")]
public class GameConfig : ScriptableObject
{
    [field: SerializeField] public Item[] Items { get; private set; }
    [field: SerializeField] public int StartItemsCount { get; private set; }
    [field: SerializeField] public Cost[] DestroyCosts { get; private set; }
    [field: SerializeField] public ProductionSpeed[] ProductionSpeeds { get; private set; }
    [field: SerializeField] public RelicBase[] Relics { get; private set; }

    [field: SerializeField] public Cost[] RelicStartCost { get; private set; }
    [field: SerializeField] public float IncreaseCostMod { get; private set; }
}

[Serializable]
public struct Cost
{
    public Cost(ResourceType resourceType, int amount)
    {
        ResourceType = resourceType;
        Amount = amount;
    }

    [field: SerializeField] public ResourceType ResourceType { get; private set; }
    [field: SerializeField] public int Amount { get; private set; }
}

[Serializable]
public struct ProductionSpeed
{
    [field: SerializeField] public TileModifier TileModifier { get; private set; }
    [field: SerializeField] public float Mod { get; private set; }
}