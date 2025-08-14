using System;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private float maxSpeed = 20f;
        [SerializeField] private float motorTorque = 2000f;
        [SerializeField] private List<Wheel> wheels;
        
        [SerializeField] private float steerSensitivity = 1f;
        [SerializeField] private float maxSteeringAngle = 30f;

        [SerializeField] private float brakeAcceleration = 50f;
        [SerializeField] private float brakeMultiplier = 300;

        [SerializeField] private Vector3 centerOfMass;
        [SerializeField] private Rigidbody carRigidbody;
        
        private float moveInput;
        private float steerInput;

        private bool isInCar = true;
        
        private void OnValidate()
        {
            if(carRigidbody == null)
                carRigidbody = GetComponent<Rigidbody>();
            
            if (carRigidbody != null)
                carRigidbody.centerOfMass = centerOfMass;
        }

        private void Update()
        {
            GetInputs();
            AnimateWheels();
        }

        private void FixedUpdate()
        {
            Steer();
            
            if (!isInCar)
                return;
            Reverse();
            Brake();
        }

        private void GetInputs()
        {
            moveInput = Input.GetAxis("Vertical");
            steerInput = Input.GetAxis("Horizontal");
        }

        private void Reverse()
        {
            if (moveInput > 0f)
                return;
            
            float forwardSpeed = Vector3.Dot(transform.forward, carRigidbody.velocity);
            //normalise the speed factor
            float speedFactor = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed));
            
            float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);
            
            foreach (Wheel wheel in wheels)
            {
                // Apply torque to motorized wheels
                wheel.WheelCollider.motorTorque = moveInput * currentMotorTorque;
                // Release brakes when accelerating
                wheel.WheelCollider.brakeTorque = 0f;
            }
        }

        private void Steer()
        {
            foreach (Wheel wheel in wheels)
            {
                if(wheel.Axel == Axel.Rear) continue;
                
                float steeringAngle = steerInput * steerSensitivity * maxSteeringAngle;
                wheel.WheelCollider.steerAngle = Mathf.Lerp(wheel.WheelCollider.steerAngle, steeringAngle, 0.6f);
            }
        }

        private void Brake()
        {
            if(Input.GetKey(KeyCode.Space))
            {
                foreach (Wheel wheel in wheels)
                {
                    wheel.WheelCollider.brakeTorque = brakeAcceleration * brakeMultiplier * Time.deltaTime;

                }
            }
            else
            {
                foreach (Wheel wheel in wheels)
                {
                    wheel.WheelCollider.brakeTorque = 0f;
                }
            }
        }

        private void AnimateWheels()
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.WheelCollider.GetWorldPose(out Vector3 wheelPosition, out Quaternion wheelRotation);
                wheel.WheelModel.transform.position = wheelPosition;
                wheel.WheelModel.transform.rotation = wheelRotation;
            }
        }
    }
}
