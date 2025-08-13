using System;
using UnityEngine;

namespace Enemies
{
    public class StareAtCamera : MonoBehaviour
    {
        [SerializeField] private Transform head;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Vector3 rotationOffset;
        [SerializeField] private float rotationSpeed = 180f;

        private void OnValidate()
        {
            if (cameraTransform == null)
                cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            if (cameraTransform == null || head == null)
                return;

            Vector3 directionToCamera = cameraTransform.position - head.position;
            Quaternion lookRotation = Quaternion.LookRotation(directionToCamera, Vector3.up);
            lookRotation *= Quaternion.Euler(rotationOffset);
            head.rotation = Quaternion.RotateTowards(head.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            head.rotation = Quaternion.RotateTowards(head.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
