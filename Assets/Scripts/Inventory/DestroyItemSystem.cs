using DI;
using Economy;
using Inventory;
using SystemHelper;
using UnityEngine;

public class DestroyItemSystem : SystemBase
{
    [field: SerializeField] public DestroyItemArea DestroyItemArea { get; private set; }
    
    private Camera _camera;
    private ResourceSystem _resourceSystem;
    private GameConfig _gameConfig;
    private ItemSpawnSystem _itemSpawnSystem;

    [Inject]
    public void Construct(Camera camera, ResourceSystem resourceSystem, GameConfig gameConfig, ItemSpawnSystem itemSpawnSystem)
    {
        _camera = camera;
        _resourceSystem = resourceSystem;
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

        if (!_resourceSystem.TryConsume(_gameConfig.DestroyCost.RequiredResources))
        {
            return false;
        }

        Destroy(item.gameObject);
        _itemSpawnSystem.CreateItem();
        return true;
    }
}
