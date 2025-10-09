using DI;
using Economy;
using Inventory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RelicSystem : SystemBase
{
    [SerializeField] ActiveRelicArea _activeRelicArea;
    [SerializeField] GetRelicArea _getRelicArea;
    private GameConfig _gameConfig;
    private ResourceSystem _resourceSystem;
    private ItemSpawnSystem _itemSpawnSystem;
    private Container _container;
    private Dictionary<ResourceType, float> _nextRelicCost = new();
    private readonly List<RelicBase> _inactiveRelics = new();
    private readonly List<RelicBase> _activeRelics = new();
    private Cost _nextCostRounded;

    [Inject]
    public void Construct(GameConfig gameConfig, ResourceSystem resourceSystem, ItemSpawnSystem itemSpawnSystem, Container container)
    {
        _gameConfig = gameConfig;
        _resourceSystem = resourceSystem;
        _itemSpawnSystem = itemSpawnSystem;
        _container = container;
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
        CreateRelicsCopy();
        _nextRelicCost = _gameConfig.RelicStartCost.RequiredResources.ToDictionary(cost => cost.Type, r => (float)r.Amount);
        _getRelicArea.CostArea.Init();
        _getRelicArea.ConfigureForRelic(TryGetRelic);
        ShowNextRelic();
        ShowCost();
    }

    private void CreateRelicsCopy()
    {
        foreach (var relic in _gameConfig.Relics)
        {
            relic.gameObject.SetActive(false);
            var copy = Instantiate(relic);
            _inactiveRelics.Add(copy);
            _container.InjectMonoBehaviour(copy);
            copy.IsActive = false;
        }
    }

    private void ShowCost()
    {
        List<GameResource> requiredResources = new(_nextRelicCost.Count);

        foreach (var (type, amount) in _nextRelicCost)
        {
            int rounded = Mathf.RoundToInt(amount);
            requiredResources.Add(new(type, rounded));
        }

        _nextCostRounded = new(requiredResources);
        _getRelicArea.CostArea.UpdateCost(_nextCostRounded);
    }

    private void IncreaseCost()
    {
        foreach (var (type, amount) in _nextRelicCost)
        {
            float increased = amount * (1f + _gameConfig.IncreaseCostMod);
            _nextRelicCost[type] = increased;
        }
    }    

    private void ShowNextRelic()
    {
        if (_inactiveRelics.Count == 0)
        {
            _getRelicArea.ConfigureForItem(TryGetNewItem);
            return;
        }

        _getRelicArea.ShowNewRandomRelic(_inactiveRelics);
    }

    private void TryGetRelic()
    {
        if (!_resourceSystem.TryConsume(_nextCostRounded.RequiredResources))
        {
            return;
        }

        SetRelicAsActive();
        IncreaseCost();
        ShowCost();
        ShowNextRelic();
    }

    private void SetRelicAsActive()
    {
        var relic = _getRelicArea.NextRelic;
        relic.transform.SetParent(_activeRelicArea.RelicsArea.transform, false);
        _activeRelics.Add(relic);
        relic.IsActive = true;
    }

    private void TryGetNewItem()
    {
        if (!_resourceSystem.TryConsume(_nextCostRounded.RequiredResources))
        {
            return;
        }

        IncreaseCost();
        ShowCost();
        _itemSpawnSystem.CreateItem();
    }
}
