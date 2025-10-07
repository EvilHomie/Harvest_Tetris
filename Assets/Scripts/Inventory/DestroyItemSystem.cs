using DI;
using Economy;
using Inventory;
using UnityEngine;

public class DestroyItemSystem : MonoBehaviour
{
    [field: SerializeField] public DestroyItemArea DestroyItemArea;

    private Camera _camera;
    private ResourcesProductionSystem _resourcesCollectSystem;
    private GameConfig _gameConfig;
    private ItemSpawnerSystem _spawnerSystem;

    [Inject]
    public void Construct(Camera camera, ResourcesProductionSystem resourcesCollectSystem, GameConfig gameConfig, ItemSpawnerSystem itemSpawnerSystem)
    {
        _camera = camera;
        _resourcesCollectSystem = resourcesCollectSystem;
        _gameConfig = gameConfig;
        _spawnerSystem = itemSpawnerSystem;
    }

    private void Start()
    {
        foreach (var cost in _gameConfig.DestroyCosts)
        {
            if (cost.ResourceType == ResourceType.Wood)
            {
                DestroyItemArea.WoodCost.AmountText.text = cost.Amount.ToString();
            }
            else if (cost.ResourceType == ResourceType.Wheat)
            {
                DestroyItemArea.WheatCost.AmountText.text = cost.Amount.ToString();
            }
            else if (cost.ResourceType == ResourceType.Iron)
            {
                DestroyItemArea.IronCost.AmountText.text = cost.Amount.ToString();
            }                
        }
    }

    public bool TryDestroyItem(Item item)
    {
        if (IsItemOverDestroyArea(item) && TrySpendResources())
        {
            Destroy(item.gameObject);
            _spawnerSystem.CreateItem();
            return true;
        }

        return false;
    }

    private bool IsItemOverDestroyArea(Item item)
    {
        Vector2 screenPoint = _camera.WorldToScreenPoint(item.RTransform.position);
        return RectTransformUtility.RectangleContainsScreenPoint(DestroyItemArea.RTransform, screenPoint, _camera);
    }

    private bool TrySpendResources()
    {
        return _resourcesCollectSystem.TrySpendResources(_gameConfig.DestroyCosts);
    }
}
