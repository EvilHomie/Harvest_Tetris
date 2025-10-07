using DI;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private Canvas _canvas;
    private InventoryConfig _inventoryConfig;

    [Inject]
    public void Construct(Canvas canvas, InventoryConfig inventoryConfig)
    {
        _canvas = canvas;
        _inventoryConfig = inventoryConfig;
    }


    private void Start()
    {
        InventoryHelper.CreateInventoryGrid(_inventoryConfig, _canvas.transform);
    }
}
