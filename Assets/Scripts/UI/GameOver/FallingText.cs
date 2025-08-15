using TMPro;
using UnityEngine;

namespace UI.GameOver
{
    public class FallingText : MonoBehaviour
    {
        private TMP_Text tmp;
        private RectTransform rect;
        private float speed;
        private float scaleSpeed;
        private float colorTime;

        private void Awake()
        {
            tmp = GetComponent<TMP_Text>();
            rect = GetComponent<RectTransform>();
            speed = Random.Range(50f, 120f);
            scaleSpeed = Random.Range(0.5f, 2f);
        }

        private void Update()
        {
            // Move down
            rect.anchoredPosition -= new Vector2(0, speed * Time.deltaTime);

            // Pulse scale
            float scale = 1f + Mathf.Sin(Time.time * scaleSpeed) * 0.3f;
            rect.localScale = new Vector3(scale, scale, 1f);

            // Color cycling
            colorTime += Time.deltaTime;
            tmp.color = Color.HSVToRGB(Mathf.PingPong(colorTime, 1f), 1f, 1f);

            // Delete if off-screen
            if (rect.anchoredPosition.y < -200f)
                Destroy(gameObject);
        }
    }
}
