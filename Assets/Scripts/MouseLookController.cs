using Events;
using UnityEngine;
using EventType = Events.EventType;

public class MouseLookController : MonoBehaviour
{
    [Header("Camera Targets")]
    [SerializeField] private Transform carLookTarget;
    [SerializeField] private Transform pushLookTarget;

    [Header("Sensitivity")]
    [SerializeField] private float sensitivity = 3f;

    [Header("Car Mode Limits")]
    [SerializeField] private float carYawLimit = 135f;
    [SerializeField] private float carPitchMin = -30f;
    [SerializeField] private float carPitchMax = 60f;

    [Header("Push Mode Limits")]
    [SerializeField] private float pushYawLimit = 90f;
    [SerializeField] private float pushPitchMin = -30f;
    [SerializeField] private float pushPitchMax = 60f;

    private bool isPushMode = false;
    private float yaw;
    private float pitch;

    private Transform currentTarget;

    private void OnEnable()
    {
        EventManager.currentManager.Subscribe(EventType.ToggleCamera, OnToggleCamera);
        EventManager.currentManager.Subscribe(EventType.PlayerWin, OnGameEnd);
        EventManager.currentManager.Subscribe(EventType.PlayerCaught, OnGameEnd);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Default mode is inside car
        currentTarget = carLookTarget;
    }

    private void OnDisable()
    {
        EventManager.currentManager.Unsubscribe(EventType.ToggleCamera, OnToggleCamera);
        EventManager.currentManager.Unsubscribe(EventType.PlayerWin, OnGameEnd);
        EventManager.currentManager.Unsubscribe(EventType.PlayerCaught, OnGameEnd);
    }

    private void Update()
    {
        HandleMouseLook();
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        yaw += mouseX;
        pitch -= mouseY;

        if (isPushMode)
        {
            yaw = Mathf.Clamp(yaw, -pushYawLimit, pushYawLimit);
            pitch = Mathf.Clamp(pitch, pushPitchMin, pushPitchMax);
        }
        else
        {
            yaw = Mathf.Clamp(yaw, -carYawLimit, carYawLimit);
            pitch = Mathf.Clamp(pitch, carPitchMin, carPitchMax);
        }

        currentTarget.localRotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private void OnToggleCamera(EventData eventData)
    {
        if (!eventData.IsEventOfType(out ToggleCameraEvent toggleEvent)) return;

        isPushMode = toggleEvent.IsPushingMode;
        yaw = 0f; 
        pitch = 0f;
        currentTarget = isPushMode ? pushLookTarget : carLookTarget;
    }
    
    private void OnGameEnd(EventData eventData)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
