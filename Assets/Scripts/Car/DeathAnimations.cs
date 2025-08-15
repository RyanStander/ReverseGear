using Events;
using UnityEngine;
using UnityEngine.Playables;
using EventType = Events.EventType;

namespace Car
{
    public class DeathAnimations : MonoBehaviour
    {
        [SerializeField] private PlayableDirector playableDirector;
        [SerializeField] private GameObject[] objectsToEnable;
        [SerializeField] private Rigidbody carRigidbody;
        private void OnValidate()
        {
            if (playableDirector == null)
                playableDirector = GetComponent<PlayableDirector>();
            
            if (carRigidbody == null)
                carRigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.PlayerCaught, OnPlayerCaught);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.PlayerCaught, OnPlayerCaught);
        }

        private void OnPlayerCaught (EventData eventData)
        {
            if (!eventData.IsEventOfType(out PlayerCaughtEvent playerCaughtEvent)) return;

            foreach (GameObject obj in objectsToEnable)
            {
                obj.SetActive(true);
            }
            playableDirector.Play();

            carRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
