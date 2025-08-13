using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace MainMenu
{
    public class SwapFollowTargetAfterTime : MonoBehaviour
    {
        [SerializeField] private GameObject[] cameras;
        [SerializeField] private float swapInterval = 5f;
        private int currentTargetIndex = 0;
        private float timeStamp;

        private void Start()
        {
            timeStamp = Time.time + swapInterval;
            if (cameras.Length == 0)
                return;

            for (int i = 1; i < cameras.Length; i++)
            {
                cameras[i].SetActive(false);
            }
            
            cameras[0].SetActive(true);
        }

        private void Update()
        {
            if (cameras.Length == 0) return;

            if (!(Time.time >= timeStamp))
                return;

            SwapTarget();
            timeStamp = Time.time + swapInterval;
        }

        private void SwapTarget()
        {
            currentTargetIndex++;
            if (currentTargetIndex >= cameras.Length)
                currentTargetIndex = 0;

            cameras[currentTargetIndex].SetActive(true);

            for (int i = 0; i < cameras.Length; i++)
            {
                if (i != currentTargetIndex)
                {
                    cameras[i].SetActive(false);
                }
            }
        }
    }
}
