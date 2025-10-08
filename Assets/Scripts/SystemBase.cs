using DI;
using UnityEngine;

public abstract class SystemBase : MonoBehaviour
{
    protected GameFlowSystem GameFlowSystem;

    [Inject]
    public void Construct(GameFlowSystem gameFlowSystem)
    {
        GameFlowSystem = gameFlowSystem;
    }

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        UnSubscribe();
    }

    protected abstract void Subscribe();
    protected abstract void UnSubscribe();
}
