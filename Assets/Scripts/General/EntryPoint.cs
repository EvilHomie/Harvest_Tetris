using DI;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void InitDI()
    {
        var installer = FindFirstObjectByType<Installer>(FindObjectsInactive.Include);
        installer.Init();
    }
}
