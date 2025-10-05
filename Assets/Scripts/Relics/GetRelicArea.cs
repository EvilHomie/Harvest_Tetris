using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetRelicArea : MonoBehaviour
{
    [field: SerializeField] public Button GetButton { get; private set; }
    [field: SerializeField] public RectTransform NextRelicArea { get; private set; }
    [field: SerializeField] public TextMeshProUGUI DiscriptionText { get; private set; }
    [field: SerializeField] public CostView WheatCost { get; private set; }
    [field: SerializeField] public CostView WoodCost { get; private set; }
    [field: SerializeField] public CostView IronCost { get; private set; }
    public RelicBase CurrentRelic { get; set; }
}
