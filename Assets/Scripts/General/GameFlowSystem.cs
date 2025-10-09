using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameFlowSystem : MonoBehaviour
{
    public Action CustomStart { get; set; }
    public Action<float> CustomUpdate { get; set; }

    private float _speedMod = 1;

    private void Start()
    {
        CustomStart?.Invoke();
    }

    void Update()
    {
        if (Keyboard.current[Key.Digit1].wasPressedThisFrame) _speedMod = 1;
        else if (Keyboard.current[Key.Digit2].wasPressedThisFrame) _speedMod = 2;
        else if (Keyboard.current[Key.Digit3].wasPressedThisFrame) _speedMod = 3;

        if (_speedMod <= 0)
        {
            return;
        }

        var tickTime = Time.deltaTime * _speedMod;
        CustomUpdate?.Invoke(tickTime);
    }
}
