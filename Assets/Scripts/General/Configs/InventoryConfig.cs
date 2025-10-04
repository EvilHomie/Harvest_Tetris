using Inventory;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Scriptable Objects/InventoryConfig")]
public class InventoryConfig : ScriptableObject
{
    [Header("INVENTORY CONFIG")]
    [field: SerializeField] public int ColumnCount { get; private set; }
    [field: SerializeField] public int RowCount { get; private set; }
    [field: SerializeField] public int CellSize { get; private set; }
    [field: SerializeField] public float SpaceSize { get; private set; }
    [field: SerializeField] public InventoryCell CellPF { get; private set; }
}