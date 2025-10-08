using UnityEngine;

public class DestroyItemArea : MonoBehaviour
{
    [field: SerializeField] public RectTransform RTransform { get; private set; }
    [field: SerializeField] public CostArea CostArea { get; private set; }
}