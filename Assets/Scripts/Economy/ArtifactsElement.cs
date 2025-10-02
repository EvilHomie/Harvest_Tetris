using UnityEngine;
using UnityEngine.UI;

public class ArtifactsElement : MonoBehaviour
{
    [SerializeField] Image _activeFrame;

    public void Enable()
    {
        _activeFrame.gameObject.SetActive(true);
    }

    public void Disable()
    {
        _activeFrame.gameObject.SetActive(false);
    }
}
