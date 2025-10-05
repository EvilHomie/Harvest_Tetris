using UnityEngine;
using UnityEngine.UI;

public class RelicBase : MonoBehaviour
{
    [field: SerializeField] public string Discription {  get; private set; }
    [field: SerializeField] public Image Image { get; private set; }

    public string GetDiscription()
    {
        return Discription;
    }
}
