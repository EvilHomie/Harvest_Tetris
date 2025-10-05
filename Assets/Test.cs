using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    [SerializeField] int startCost;

    [SerializeField] float tempCost;
    [SerializeField] int roundedCost;


    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            tempCost += tempCost * 0.2f;

            roundedCost = Mathf.RoundToInt(tempCost);
        }
    }
}
