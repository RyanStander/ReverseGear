using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using EventType = Events.EventType;

public class CameraSwitchController : MonoBehaviour
{
    [SerializeField] private GameObject insideCamera;
    [SerializeField] private GameObject outsideCamera;

    private void OnEnable()
    {
        EventManager.currentManager.Subscribe(EventType.ToggleCamera,OnToggleCamera);
    }

    private void OnDisable()
    {
        EventManager.currentManager.Unsubscribe(EventType.ToggleCamera, OnToggleCamera);
    }

    private void OnToggleCamera(EventData eventData)
    {
        if (!eventData.IsEventOfType(out ToggleCameraEvent toggleCameraEvent)) return;
        insideCamera.SetActive(!toggleCameraEvent.IsPushingMode);
        outsideCamera.SetActive(toggleCameraEvent.IsPushingMode);
    }
}
