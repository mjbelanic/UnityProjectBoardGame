using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera Positioning")]
    public Vector2 cameraOffset = new Vector2(30f, 50f);
    public float lookAtOffset = 2f;
    public bool lockMovement;

    [Header("Move Controls")]
    public float inOutSpeed = 50f;
    public float lateralSpeed = 50f;
    public float rotateSpeed = 45f;

    [Header("Move Bounds")]
    public Vector2 minBounds, maxBounds;

    [Header("Zoom Controls")]
    public float zoomSpeed = 150f;
    public float nearZoomLimit = 25f;
    public float farZoomLimit = 100f;
    public float startingZoom = 50f;

    IZoomStrategy zoomStrategy;
    Vector3 frameMove;
    Vector3 originalPosition;
    Quaternion originalRotation;
    float frameRotate;
    float frameZoom;
    float originalZoom;
    Camera cam;

    private void Awake()
    {
        lockMovement = false;
        cam = GetComponentInChildren<Camera>();
        cam.transform.localPosition = new Vector3(0f, Mathf.Abs(cameraOffset.y), -Mathf.Abs(cameraOffset.x));
        zoomStrategy = cam.orthographic ? (IZoomStrategy)new OrthographicZoomStategy(cam, startingZoom) : new PerspectiveZoomStrategy(cam, cameraOffset, startingZoom);
        cam.transform.LookAt(transform.position + Vector3.up * lookAtOffset);
    }

    private void OnEnable()
    {
        KeyboardInputManager.OnMoveInput += UpdateFrameMove;
        KeyboardInputManager.OnRotateInput += UpdateFrameRotate;
        KeyboardInputManager.OnZoomInput += UpdateFrameZoom;
        MouseInputManager.OnMoveInput += UpdateFrameMove;
        MouseInputManager.OnRotateInput += UpdateFrameRotate;
        MouseInputManager.OnZoomInput += UpdateFrameZoom;
    }

    private void OnDisable()
    {
        KeyboardInputManager.OnMoveInput -= UpdateFrameMove;
        KeyboardInputManager.OnRotateInput -= UpdateFrameRotate;
        KeyboardInputManager.OnZoomInput -= UpdateFrameZoom;
        MouseInputManager.OnMoveInput -= UpdateFrameMove;
        MouseInputManager.OnRotateInput -= UpdateFrameRotate;
        MouseInputManager.OnZoomInput -= UpdateFrameZoom;
    }

    private void UpdateFrameMove(Vector3 moveVector)
    {
        frameMove += moveVector;
    }

    private void UpdateFrameRotate(float rotateAmount)
    {
        frameRotate += rotateAmount;
    }

    private void UpdateFrameZoom(float zoomAmount)
    {
        frameZoom += zoomAmount;
    }

    internal void ZoomOutOnBoard()
    {
        originalZoom = frameZoom;
        zoomStrategy.ZoomOutFully(cam,farZoomLimit);
        frameZoom = 0f;
    }

    internal void FocusCameraToCenter()
    {
        if (GameManager.Instance.CurrentPlayerId == 0)
        {
            originalPosition = transform.position;
            originalRotation = transform.rotation;
        }
        Vector3 centerOfBoard = new Vector3(0.0f, 0.0f, 6.5f);
        Quaternion diceBoxCameraAngle = Quaternion.Euler(30, 0, 0);
        StartCoroutine(LerpPosition(centerOfBoard, diceBoxCameraAngle,2));
    }

    internal void ReturnCameraToOriginalPlace()
    {
        StartCoroutine(LerpPosition(originalPosition, originalRotation, 2));
    }

    IEnumerator LerpPosition(Vector3 targetPosition, Quaternion targetRotation, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        ZoomOutOnBoard();
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, time / duration);

            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }


    private void LateUpdate()
    {
        if (frameMove != Vector3.zero && !lockMovement)
        {
            Vector3 speedModFrameMove = new Vector3(frameMove.x * lateralSpeed, frameMove.y, frameMove.z * inOutSpeed);
            transform.position += transform.TransformDirection(speedModFrameMove) * Time.deltaTime;
            LockPositionInBounds();
            frameMove = Vector3.zero;
        }

        if (frameRotate != 0 && !lockMovement)
        {
            transform.Rotate(Vector3.up, frameRotate * Time.deltaTime * rotateSpeed);
            frameRotate = 0f;
        }

        if (frameZoom < 0f && !lockMovement)
        {
            zoomStrategy.ZoomIn(cam, Time.deltaTime * Mathf.Abs(frameZoom) * zoomSpeed, nearZoomLimit);
            frameZoom = 0f;
        }
        else if (frameZoom > 0f && !lockMovement)
        {
            zoomStrategy.ZoomOut(cam, Time.deltaTime * frameZoom * zoomSpeed, farZoomLimit);
            frameZoom = 0f;
        }
    }

    private void LockPositionInBounds()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, minBounds.y, maxBounds.y)
            );
    }
}
