using Inventory;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig")]
public class GameConfig : ScriptableObject
{
    [field: SerializeField] public Item[] Items { get; private set; }
    [field: SerializeField] public int StartItemsCount { get; private set; }
    [field: SerializeField] public Cost[] DestroyCosts { get; private set; }
}

[Serializable]
public struct Cost
{
    [field: SerializeField] public ResourceType ResourceType { get; private set; }
    [field: SerializeField] public int Amount { get; private set; }
}