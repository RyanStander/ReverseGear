using System;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private float maxAcceleration = 30f;
        [SerializeField] private float speedMultiplier = 500f;
        [SerializeField] private float brakeAcceleration = 50f;
        [SerializeField] private List<Wheel> wheels;

        [SerializeField] private Rigidbody carRigidbody;
        private float moveInput;

        private void OnValidate()
        {
            if(carRigidbody == null)
                carRigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            GetInputs();
        }

        private void LateUpdate()
        {
            Move();
        }

        private void GetInputs()
        {
            moveInput = Input.GetAxis("Vertical");
        }

        private void Move()
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.WheelCollider.motorTorque = moveInput * maxAcceleration * speedMultiplier * Time.deltaTime;
            }
        }
    }
}
