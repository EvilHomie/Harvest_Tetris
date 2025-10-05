using UnityEngine;
using UnityEngine.UI;

public abstract class RelicBase : MonoBehaviour
{
    [field: SerializeField] public bool IsActive { get; set; } = false;
    [field: SerializeField] public string Discription {  get; private set; }
    [field: SerializeField] public Image Image { get; private set; }

    public string GetDiscription()
    {
        return Discription;
    }
}
