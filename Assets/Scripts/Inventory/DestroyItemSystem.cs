using DI;
using Economy;
using Inventory;
using UnityEngine;

public class DestroyItemSystem : SystemBase
{
    [field: SerializeField] public DestroyItemArea DestroyItemArea { get; private set; }

    private Camera _camera;
    private ResourcesProductionSystem _resourcesCollectSystem;
    private GameConfig _gameConfig;
    private ItemSpawnSystem _itemSpawnSystem;

    [Inject]
    public void Construct(Camera camera, ResourcesProductionSystem resourcesCollectSystem, GameConfig gameConfig, ItemSpawnSystem itemSpawnSystem)
    {
        _camera = camera;
        _resourcesCollectSystem = resourcesCollectSystem;
        _gameConfig = gameConfig;
        _itemSpawnSystem = itemSpawnSystem;
    }

    protected override void Subscribe()
    {
        GameFlowSystem.CustomStart += Init;
    }

    protected override void UnSubscribe()
    {
        GameFlowSystem.CustomStart -= Init;
    }

    public bool TryDestroyItem(Item item)
    {
        if (IsItemOverDestroyArea(item) && TrySpendResources())
        {
            Destroy(item.gameObject);
            _itemSpawnSystem.CreateItem();
            return true;
        }

        return false;
    }

    private void Init()
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
