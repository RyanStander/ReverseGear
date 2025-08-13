using System;
using UnityEngine;

namespace Car
{
    public class CarSounds : MonoBehaviour
    {
        [SerializeField] private float maxSpeed;

        [SerializeField] private Rigidbody carRigidbody;
        [SerializeField] private AudioSource engineSound;

        [SerializeField] private float minPitch;
        [SerializeField] private float maxPitch;
        private float pitchVelocity;

        private void OnValidate()
        {
            if (carRigidbody == null)
                carRigidbody = GetComponentInParent<Rigidbody>();

            if (engineSound == null)
                engineSound = GetComponent<AudioSource>();
        }

        private void Update()
        {
            EngineSound();
        }

        private void EngineSound()
        {
            float currentSpeed = carRigidbody.velocity.magnitude;
            float targetPitch = Mathf.Lerp(minPitch, maxPitch, currentSpeed / maxSpeed);
            targetPitch = Mathf.Clamp(targetPitch, minPitch, maxPitch);
            engineSound.pitch = Mathf.SmoothDamp(engineSound.pitch, targetPitch, ref pitchVelocity, 0.1f);
        }
    }
}
