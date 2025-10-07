using DI;
using Economy;
using Inventory;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RelicSystem : MonoBehaviour
{
    [SerializeField] ActiveRelicArea _activeRelicArea;
    [SerializeField] GetRelicArea _getRelicArea;
    private GameConfig _gameConfig;
    private ResourcesProductionSystem _resourcesProductionSystem;
    private ItemSpawnSystem _itemSpawnSystem;
    private Container _container;
    private readonly Dictionary<ResourceType, float> _nextRelicCost = new();
    private readonly Dictionary<ResourceType, int> _nextRelicCostRounded = new();
    private readonly List<RelicBase> _inactiveRelics = new();
    private readonly List<RelicBase> _activeRelics = new();


    [Inject]
    public void Construct(GameConfig gameConfig, ResourcesProductionSystem resourcesProductionSystem, ItemSpawnSystem itemSpawnSystem, Container container)
    {
        _gameConfig = gameConfig;
        _resourcesProductionSystem = resourcesProductionSystem;
        _itemSpawnSystem = itemSpawnSystem;
        _container = container;
    }

    private void Start()
    {
        _getRelicArea.GetButton.onClick.AddListener(TryGetRelic);

        foreach (var cost in _gameConfig.RelicStartCost)
        {
            _nextRelicCost.Add(cost.ResourceType, cost.Amount);
            ShowCost(cost.ResourceType);
        }

        CreateCopy();
        ShowNextRelic();
    }
    private void OnDisable()
    {
        _getRelicArea.GetButton.onClick.RemoveAllListeners();
    }

    private void UpdateCost()
    {
        foreach (var key in _nextRelicCost.Keys.ToList())
        {
            _nextRelicCost[key] += _nextRelicCost[key] * _gameConfig.IncreaseCostMod;
            ShowCost(key);
        }
    }

    private void ShowCost(ResourceType key)
    {
        var roundedCost = Mathf.RoundToInt(_nextRelicCost[key]);
        _nextRelicCostRounded[key] = roundedCost;

        if (key == ResourceType.Wood)
        {
            _getRelicArea.WoodCost.AmountText.text = roundedCost.ToString();
        }
        else if (key == ResourceType.Wheat)
        {
            _getRelicArea.WheatCost.AmountText.text = roundedCost.ToString();
        }
        else if (key == ResourceType.Iron)
        {
            _getRelicArea.IronCost.AmountText.text = roundedCost.ToString();
        }
    }

    private void CreateCopy()
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

    private void ShowNextRelic()
    {
        if (_inactiveRelics.Count == 0)
        {
            ConfigureForRandomItem();
            return;
        }

        int randomIndex = Random.Range(0, _inactiveRelics.Count);
        var relic = _inactiveRelics[randomIndex];
        _getRelicArea.DiscriptionText.text = relic.Discription;
        relic.transform.SetParent(_getRelicArea.NextRelicArea.transform, false);
        relic.transform.localPosition = Vector3.zero;
        relic.gameObject.SetActive(true);
        _getRelicArea.CurrentRelic = relic;
        _inactiveRelics.RemoveAt(randomIndex);
    }

    private void TryGetRelic()
    {
        List<Cost> costs = new();

        foreach (var cost in _nextRelicCostRounded)
        {
            costs.Add(new Cost(cost.Key, cost.Value));
        }

        if (!_resourcesProductionSystem.TrySpendResources(costs.ToArray()))
        {
            return;
        }

        SetRelicAsActive();
        UpdateCost();
        ShowNextRelic();
    }

    private void SetRelicAsActive()
    {
        var relic = _getRelicArea.CurrentRelic;
        relic.transform.SetParent(_activeRelicArea.RelicsArea.transform, false);
        _activeRelics.Add(relic);
        relic.IsActive = true;
    }

    private void ConfigureForRandomItem()
    {
        _getRelicArea.GetButton.onClick.RemoveAllListeners();
        var buttonText = _getRelicArea.GetButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "Get New Item";
        _getRelicArea.GetButton.onClick.AddListener(GetNewItem);
        _getRelicArea.NextRelicArea.gameObject.SetActive(false);
        _getRelicArea.DiscriptionText.transform.parent.gameObject.SetActive(false);
    }

    private void GetNewItem()
    {
        List<Cost> costs = new();

        foreach (var cost in _nextRelicCostRounded)
        {
            costs.Add(new Cost(cost.Key, cost.Value));
        }

        if (!_resourcesProductionSystem.TrySpendResources(costs.ToArray()))
        {
            return;
        }

        UpdateCost();
        _itemSpawnSystem.CreateItem();
    }
}
