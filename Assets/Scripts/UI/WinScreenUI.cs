using Events;
using UnityEngine;

namespace UI
{
    public class WinScreenUI : MonoBehaviour
    {
        [SerializeField] private GameObject winScreen;

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(Events.EventType.PlayerWin, OnPlayerWin);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(Events.EventType.PlayerWin, OnPlayerWin);
        }

        private void OnPlayerWin(EventData eventData)
        {
            if (!eventData.IsEventOfType(out PlayerWinEvent _)) return;
            winScreen.SetActive(true);
        }
    }
}
