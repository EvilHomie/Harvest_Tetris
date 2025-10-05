using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectPopup : MonoBehaviour
{
    [field: SerializeField] public Image ProdImage { get; private set; }
    [field: SerializeField] public TextMeshProUGUI AmuntText { get; private set; }

    public float ShowTime;
}
