using Inventory;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig")]
public class GameConfig : ScriptableObject
{
    [field: SerializeField] public Item[] Items { get; private set; }
    [field: SerializeField] public int StartItemsCount { get; private set; }
}
