using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace MainMenu
{
    public class SwapFollowTargetAfterTime : MonoBehaviour
    {
        [SerializeField] private Transform[] targets;
        [SerializeField] private float swapInterval = 5f;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        private int currentTargetIndex = 0;
        private float timeStamp;

        private bool hasVirtualCamera;
        private void OnValidate()
        {
            if(virtualCamera == null)
                virtualCamera = GetComponent<CinemachineVirtualCamera>();
            
            if (targets != null && targets.Length != 0)
                virtualCamera.LookAt = targets[0];
        }

        private void Start()
        {
            hasVirtualCamera = virtualCamera != null;
            timeStamp = Time.time + swapInterval;
        }

        private void Update()
        {
            if (targets.Length == 0 && hasVirtualCamera) return;

            if (!(Time.time >= timeStamp)) 
                return;
            
            SwapTarget();
            timeStamp = Time.time + swapInterval;
        }

        private void SwapTarget()
        {
            currentTargetIndex++;
            if (currentTargetIndex >= targets.Length)
                currentTargetIndex = 0;
            
            virtualCamera.LookAt = targets[currentTargetIndex];
        }
    }
}
