using System;
using UnityEngine;

namespace Car
{
    public class CarReverseController : MonoBehaviour
    {
        [SerializeField] private float reverseSpeed = 10f;
        [SerializeField] private float turnSpeed = 100f;
        [SerializeField] private float maxSteeringAngle = 540f;
        [SerializeField] private float returnToCenterSpeed = 100f;
        [SerializeField] private Rigidbody rigidbody;
        private bool isInCar = true;

        private void OnValidate()
        {
            if (rigidbody == null)
                rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (rigidbody.velocity.magnitude > 0.1f || !isInCar)
                HandleSteering();
            
            if (!isInCar)
                return;
            
            HandleReverseMovement();
        }

        private void HandleSteering()
        {
            float turnAmount = Input.GetAxis("Horizontal") * turnSpeed;

            transform.Rotate(Vector3.up * (turnAmount * Time.fixedDeltaTime));
        }

        private void HandleReverseMovement()
        {
            if (!Input.GetKey(KeyCode.S))
                return;

            Vector3 reverseForce = -transform.forward * reverseSpeed;
            rigidbody.AddForce(reverseForce, ForceMode.Acceleration);
        }

        public void SetControlActive(bool active)
        {
            isInCar = active;
        }
    }
}
