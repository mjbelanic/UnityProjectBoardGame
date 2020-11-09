using UnityEngine;

public class KeyboardInputManager : InputManager
{
    //EVENTS
    public static event MoveInputHandler OnMoveInput;
    public static event RotateInputHandler OnRotateInput;
    public static event ZoomInputHandler OnZoomInput;

    private void Update()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            OnMoveInput?.Invoke(Vector3.forward);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            OnMoveInput?.Invoke(-Vector3.right);
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            OnMoveInput?.Invoke(-Vector3.forward);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            OnMoveInput?.Invoke(Vector3.right);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            OnRotateInput?.Invoke(1f);
        }
        if (Input.GetKey(KeyCode.E))
        {
            OnRotateInput?.Invoke(-1f);
        }

        if (Input.GetKey(KeyCode.Z))
        {
            OnZoomInput?.Invoke(-1f);
        }
        if (Input.GetKey(KeyCode.X))
        {
            OnZoomInput?.Invoke(1f);
        }

    }
}
