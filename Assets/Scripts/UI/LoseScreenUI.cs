using Events;
using UnityEngine;
using EventType = Events.EventType;

namespace UI
{
    public class LoseScreenUI : MonoBehaviour
    {
        [SerializeField] private GameObject loseScreen;

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.PlayerCaught, OnPlayerLose);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.PlayerCaught, OnPlayerLose);
        }

        private void OnPlayerLose(EventData eventData)
        {
            if (!eventData.IsEventOfType(out PlayerCaughtEvent _)) return;
            loseScreen.SetActive(true);
        }
    }
}
