using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameFlowSystem : MonoBehaviour
{
    public Action CustomStart {  get; set; }


    private void Start()
    {
        CustomStart?.Invoke();
    }

    void Update()
    {
        if (Keyboard.current[Key.Digit1].wasPressedThisFrame)
        {
            Time.timeScale = 1;
        }
        else if (Keyboard.current[Key.Digit2].wasPressedThisFrame)
        {
            Time.timeScale = 2;
        }
        else if (Keyboard.current[Key.Digit3].wasPressedThisFrame)
        {
            Time.timeScale = 3;
        }
    }
}
