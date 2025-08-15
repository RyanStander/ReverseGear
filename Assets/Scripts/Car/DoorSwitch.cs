using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using EventType = UnityEngine.EventType;
using Random = UnityEngine.Random;

namespace Car
{
    public class DoorSwitch : MonoBehaviour
    {
        [SerializeField] private Vector3 closedRotation;
        [SerializeField] private Vector3 openRotation = new(0, 90, 0);
        [SerializeField] private float doorOpenDuration;
        [SerializeField] private float doorCloseDuration;

        [SerializeField] private AudioSource doorAudioSource;
        [SerializeField] private AudioClip[] doorOpenSounds;
        [SerializeField] private AudioClip[] doorCloseSounds;
        [SerializeField] private float doorCloseDelay = 0.8f;

        private Coroutine toggleDoorCoroutine;
        private Coroutine doorSoundCoroutine;
        private bool doorIsOpening = false;

        private void OnValidate()
        {
            if (doorAudioSource == null)
                doorAudioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(Events.EventType.ToggleCamera, OnToggleCamera);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(Events.EventType.ToggleCamera, OnToggleCamera);
        }

        private void OnToggleCamera(EventData eventData)
        {
            if (!eventData.IsEventOfType(out ToggleCameraEvent toggleCameraEvent)) return;

            doorIsOpening = toggleCameraEvent.IsPushingMode;

            if (toggleDoorCoroutine != null)
            {
                StopCoroutine(toggleDoorCoroutine);
                toggleDoorCoroutine = null;
            }

            toggleDoorCoroutine = StartCoroutine(ToggleDoor());

            StartCoroutine(PlayDoorSound());
        }

        private IEnumerator ToggleDoor()
        {
            float duration = doorIsOpening ? doorOpenDuration : doorCloseDuration;
            Vector3 rotation = doorIsOpening ? openRotation : closedRotation;

            float elapsedTime = 0f;
            Vector3 startRotation = transform.localEulerAngles;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                transform.localEulerAngles = Vector3.Lerp(startRotation, rotation, t);
                yield return null;
            }
        }

        private IEnumerator PlayDoorSound()
        {
            AudioClip[] sounds = doorIsOpening ? doorOpenSounds : doorCloseSounds;

            if (!doorIsOpening)
                yield return new WaitForSeconds(doorCloseDelay);

            if (sounds.Length == 0) yield break;
            int randomIndex = Random.Range(0, sounds.Length);
            doorAudioSource.PlayOneShot(sounds[randomIndex]);
        }
    }
}
