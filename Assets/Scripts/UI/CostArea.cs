using System.Collections.Generic;
using UnityEngine;

public class CostArea : MonoBehaviour
{
    [field: SerializeField] public CostView WheatCost { get; private set; }
    [field: SerializeField] public CostView WoodCost { get; private set; }
    [field: SerializeField] public CostView IronCost { get; private set; }

    private readonly Dictionary<ResourceType, CostView> _costViewsByType = new();

    public void Init()
    {
        _costViewsByType.Add(ResourceType.Iron, IronCost);
        _costViewsByType.Add(ResourceType.Wheat, WheatCost);
        _costViewsByType.Add(ResourceType.Wood, WoodCost);

        foreach (var view in _costViewsByType.Values)
        {
            view.gameObject.SetActive(false);
        }
    }

    public void UpdateCost(Cost cost)
    {
        foreach (var view in _costViewsByType.Values)
        {
            view.gameObject.SetActive(false);
        }

        foreach (var requiredResource in cost.RequiredResources)
        {
            if (requiredResource.Amount < 1)
            {
                continue;
            }

            _costViewsByType[requiredResource.Type].AmountText.text = requiredResource.Amount.ToString();
            _costViewsByType[requiredResource.Type].gameObject.SetActive(true);
        }
    }
}
