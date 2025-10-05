using TMPro;
using UnityEngine;

public class DestroyItemArea : MonoBehaviour
{
    [field: SerializeField] public RectTransform RTransform { get; private set; }
    [field: SerializeField] public TextMeshProUGUI WheatCostText { get; private set; }
    [field: SerializeField] public TextMeshProUGUI WoodCostText { get; private set; }
    [field: SerializeField] public TextMeshProUGUI IronCostText { get; private set; }

    private void Awake()
    {
        RTransform = GetComponent<RectTransform>();
    }
}
