using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardMouseControllerInput : MonoBehaviour, IControllerInput
{
    [SerializeField]
    private bool isLeftController;

    public Transform GetTransform()
    {
        return transform;
    }

    public bool IsTriggerPressed()
    {
        if (isLeftController)
        {
            return Keyboard.current.qKey.isPressed || Mouse.current.leftButton.isPressed;
        }
        else
        {
            return Keyboard.current.eKey.isPressed || Mouse.current.rightButton.isPressed;
        }
        // I changed the code to use the the keyboard inputs for the mouse inputs, so that the particle system still works without a mouse input.
        // It works similarly to the previous code, instead it adds the functionality of the "Q" and "E" keyboard presses
    }
}
