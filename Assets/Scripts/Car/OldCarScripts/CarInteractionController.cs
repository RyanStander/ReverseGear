using Events;
using UnityEngine;

namespace Car.OldCarScripts
{
    public class CarInteractionController : MonoBehaviour
    {
        [SerializeField] private KeyCode interactKey = KeyCode.E;
        [SerializeField] private Transform pushPosition;
        [SerializeField] private float pushSpeed = 1f;
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private CarReverseController reverseController;

        private bool isPushing = false;

        private void OnValidate()
        {
            if (rigidbody == null)
                rigidbody = GetComponent<Rigidbody>();
            if (reverseController == null)
                reverseController = GetComponent<CarReverseController>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(interactKey))
            {
                isPushing = !isPushing;
                reverseController.SetControlActive(!isPushing);
                EventManager.currentManager.AddEvent(new ToggleCameraEvent(isPushing));
            }

            if (isPushing)
            {
                rigidbody.MovePosition(rigidbody.position + transform.forward * (pushSpeed * Time.deltaTime));
            }
        }
    }
}
