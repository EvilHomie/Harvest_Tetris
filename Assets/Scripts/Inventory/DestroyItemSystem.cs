using DI;
using Economy;
using Inventory;
using SystemHelper;
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

    private void Init()
    {
        DestroyItemArea.CostArea.Init();
        DestroyItemArea.CostArea.UpdateCost(_gameConfig.DestroyCost);
    }

    public bool TryDestroyItem(Item item)
    {
        if (!Utils.IsTargetOverElement(item.RTransform, DestroyItemArea.RTransform, _camera))
        {
            return false;
        }

        if (!_resourcesCollectSystem.TrySpendResources(_gameConfig.DestroyCost))
        {
            return false;
        }

        Destroy(item.gameObject);
        _itemSpawnSystem.CreateItem();
        return true;
    }
}
