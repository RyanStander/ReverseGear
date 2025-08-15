using System;
using UnityEngine;

namespace Car
{
    public class CarCrashSfx : MonoBehaviour
    {
        [SerializeField] private Rigidbody carRigidBody;
        [SerializeField] private AudioClip[] crashSounds;
        [SerializeField] private AudioClip[] bumpSounds;
        [SerializeField] private AudioSource crashAudioSource;

        [SerializeField] private float hardCrashSpeed = 6;

        private void OnValidate()
        {
            if (carRigidBody == null)
                carRigidBody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            bool isHardCrash = carRigidBody.velocity.magnitude > hardCrashSpeed;

            if(other.gameObject.CompareTag("Monster"))
                return;
            
            if (isHardCrash)
            {
                if (crashSounds.Length <= 0)
                    return;
                int randomIndex = UnityEngine.Random.Range(0, crashSounds.Length);
                crashAudioSource.PlayOneShot(crashSounds[randomIndex]);
            }
            else if (bumpSounds.Length > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, bumpSounds.Length);
                crashAudioSource.PlayOneShot(bumpSounds[randomIndex]);
            }
        }
    }
}
