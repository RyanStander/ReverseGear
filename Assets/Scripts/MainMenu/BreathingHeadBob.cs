using UnityEngine;

namespace MainMenu
{
    public class BreathingHeadBob : MonoBehaviour
    {
        [SerializeField] private float bobSpeed = 0.5f;
        [SerializeField] private float bobAmount = 0.05f;
        [SerializeField] private float returnSpeed = 0.1f;
        private Vector3 initialPosition;
        private Vector3 targetPosition;
        private float timer;

        private void Start()
        {
            initialPosition = transform.localPosition;
            targetPosition = initialPosition;
        }

        private void FixedUpdate()
        {
            timer += Time.deltaTime * bobSpeed;
            float offsetY = Mathf.Sin(timer) * bobAmount;
            targetPosition.y = initialPosition.y + offsetY;

            transform.localPosition =
                Vector3.Lerp(transform.localPosition, targetPosition, returnSpeed * Time.deltaTime);
        }
    }
}
