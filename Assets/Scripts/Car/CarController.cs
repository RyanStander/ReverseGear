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

        [SerializeField] private float maxPushSpeed = 1f;
        [SerializeField] private float motorTorquePush = 2000f;
        
        [SerializeField] private float steeringRange = 30f;
        [SerializeField] private float steeringRangeAtMaxSpeed = 10f;
        
        [SerializeField] private float brakeTorque = 2000f;

        [SerializeField] private Vector3 centerOfMass;
        [SerializeField] private Rigidbody carRigidbody;
        
        private float moveInput;
        private float steerInput;
        private float speedFactor;

        [SerializeField]private bool isInCar = true;
        
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
            float forwardSpeed = Vector3.Dot(transform.forward, carRigidbody.velocity);
            float givenMaxSpeed = isInCar ? maxSpeed : maxPushSpeed;
            //normalise the speed factor
            speedFactor = Mathf.InverseLerp(0, givenMaxSpeed, Mathf.Abs(forwardSpeed));
            
            Steer();
            
            
            if (!isInCar)
            {
                MoveCar(maxPushSpeed, motorTorquePush);

                //we dont want it to move backwards
                if (!(forwardSpeed < 0)) 
                    return;
                
                foreach (Wheel wheel in wheels)
                    wheel.WheelCollider.brakeTorque = brakeTorque;

                return;
            }
            
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
            
            MoveCar(motorTorque, moveInput);
        }

        private void MoveCar(float givenMotorTorque, float givenMoveInput = 1)
        {
            
            
            float currentMotorTorque = Mathf.Lerp(givenMotorTorque, 0, speedFactor);
            
            foreach (Wheel wheel in wheels)
            {
                // Apply torque to motorized wheels
                wheel.WheelCollider.motorTorque = givenMoveInput * currentMotorTorque;
                // Release brakes when accelerating
                wheel.WheelCollider.brakeTorque = 0f;
            }
        }

        private void Steer()
        {
            float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);
            
            foreach (Wheel wheel in wheels)
            {
                if(wheel.Axel == Axel.Rear) continue;
                
                float steeringAngle = steerInput * currentSteerRange;
                wheel.WheelCollider.steerAngle = Mathf.Lerp(wheel.WheelCollider.steerAngle, steeringAngle, 0.6f);
            }
        }

        private void Brake()
        {
            if(Input.GetKey(KeyCode.Space))
            {
                foreach (Wheel wheel in wheels)
                {
                    wheel.WheelCollider.motorTorque = 0f;
                    wheel.WheelCollider.brakeTorque = brakeTorque;

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

        public void SetControlActive(bool active)
        {
            isInCar = active;
        }
    }
}
