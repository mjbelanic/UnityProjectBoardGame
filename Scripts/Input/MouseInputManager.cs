using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputManager : InputManager
{
    Vector2Int screen;
    float mousePositionOnRotateStart;

    //EVENTS
    public static event MoveInputHandler OnMoveInput;
    public static event RotateInputHandler OnRotateInput;
    public static event ZoomInputHandler OnZoomInput;

    private void Awake()
    {
        screen = new Vector2Int(Screen.width, Screen.height);
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        bool mouseValid = (mousePosition.y <= screen.y * 1.05f && mousePosition.y >= screen.y * -0.05f &&
            mousePosition.x <= screen.x * 1.05f && mousePosition.x >= screen.x * -0.05f);

        if (!mouseValid)
        {
            return;
        }

        //movement
        if (mousePosition.y > screen.y * 0.95f)
        {
            OnMoveInput?.Invoke(Vector3.forward);
        }
        else if (mousePosition.y < screen.y * 0.05f)
        {
            OnMoveInput?.Invoke(-Vector3.forward);
        }

        if (mousePosition.x > screen.x * 0.95f)
        {
            OnMoveInput?.Invoke(Vector3.right);
        }
        else if (mousePosition.x < screen.x * 0.05f)
        {
            OnMoveInput?.Invoke(-Vector3.right);
        }

        //rotation
        if (Input.GetMouseButtonDown(1))
        {
            mousePositionOnRotateStart = mousePosition.x;
        }
        else if (Input.GetMouseButton(1))
        {
            if(mousePosition.x < mousePositionOnRotateStart)
            {
                OnRotateInput?.Invoke(-1f);
            }
            else if(mousePosition.x > mousePositionOnRotateStart)
            {
                OnRotateInput?.Invoke(1f);
            }
        }

        // zoom
        if(Input.mouseScrollDelta.y > 0)
        {
            OnZoomInput?.Invoke(-3f);
        }
        else if(Input.mouseScrollDelta.y < 0)
        {
            OnZoomInput?.Invoke(3f);
        }
    }
}
