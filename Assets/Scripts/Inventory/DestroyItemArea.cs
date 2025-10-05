using TMPro;
using UnityEngine;

public class DestroyItemArea : MonoBehaviour
{
    [field: SerializeField] public RectTransform RTransform { get; private set; }
    [field: SerializeField] public CostView WheatCost { get; private set; }
    [field: SerializeField] public CostView WoodCost { get; private set; }
    [field: SerializeField] public CostView IronCost { get; private set; }

    private void Awake()
    {
        RTransform = GetComponent<RectTransform>();
    }
}
