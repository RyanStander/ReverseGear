using Events;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            EventManager.currentManager.AddEvent(new PlayerWinEvent());
        }
    }
}
