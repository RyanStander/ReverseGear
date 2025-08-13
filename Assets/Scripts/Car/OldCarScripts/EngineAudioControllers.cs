using UnityEngine;

namespace Car.OldCarScripts
{
    [RequireComponent(typeof(AudioSource))]
    public class EngineAudioController : MonoBehaviour
    {
        [SerializeField] private Rigidbody carRigidbody;
        [SerializeField] private float minPitch = 0.8f;
        [SerializeField] private float maxPitch = 1.5f;
        [SerializeField] private float pitchSensitivity = 0.02f;

        private AudioSource engineAudio;

        private void OnValidate()
        {
            if (carRigidbody == null)
                carRigidbody = GetComponent<Rigidbody>();

            if (engineAudio == null)
                engineAudio = GetComponent<AudioSource>();
        }

        private void Start()
        {
            engineAudio.loop = true;
            engineAudio.playOnAwake = true;
            engineAudio.Play();
        }

        private void Update()
        {
            float reverseSpeed = Vector3.Dot(carRigidbody.velocity, -transform.forward);

            if (reverseSpeed > 0.1f)
            {
                float targetPitch = Mathf.Lerp(minPitch, maxPitch, reverseSpeed * pitchSensitivity);
                engineAudio.pitch = Mathf.Clamp(targetPitch, minPitch, maxPitch);
            }
            else
            {
                engineAudio.pitch = Mathf.Lerp(engineAudio.pitch, minPitch, Time.deltaTime * 2f);
            }
        }
    }
}
