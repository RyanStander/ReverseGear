using Car.OldCarScripts;
using Events;
using UnityEngine;

namespace Car
{
    public class CarInteractionController : MonoBehaviour
    {
        [SerializeField] private KeyCode interactKey = KeyCode.E;
        [SerializeField] private Rigidbody carRigidbody;
        [SerializeField] private CarController carController;

        [SerializeField]private bool isPushing = false;

        private void OnValidate()
        {
            if (carRigidbody == null)
                carRigidbody = GetComponent<Rigidbody>();
            if (carController == null)
                carController = GetComponent<CarController>();
        }

        private void Update()
        {
            if (!Input.GetKeyDown(interactKey)) 
                return;
            
            isPushing = !isPushing;
            carController.SetControlActive(!isPushing);
            EventManager.currentManager.AddEvent(new ToggleCameraEvent(isPushing));
        }
    }
}
