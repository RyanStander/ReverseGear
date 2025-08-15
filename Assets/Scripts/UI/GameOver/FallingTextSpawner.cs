using TMPro;
using UnityEngine;

namespace UI.GameOver
{
    public class FallingTextSpawner : MonoBehaviour
    {
        [SerializeField] private RectTransform canvasTransform;
        [SerializeField] private GameObject textPrefab;
        [SerializeField] private float spawnInterval = 1.5f;

        [SerializeField] private string[] messages =
        {
            "Nice job!",
            "You did your best!",
            "Amazing!",
            "Fantastic!",
            "Well played!",
            "Try again, champ!",
            "Not bad, kid!",
            "Epic fail? More like epic win!",
            "You're filled with DETERMINATION.",
            "The real treasure was the friends we made along the way.",
            "Game over? Nah, bonus round!",
            "Git gud?",
            "Press F to pay respects.",
            "Insert coin to continue.",
            "Winner in our hearts!",
            "Continue? (Y/N)",
            "You were the imposter all along.",
            "It's dangerous to go alone… take a break!",
            "Respawning in 3… 2… 1…",
            "Achievement unlocked: Almost made it!",
            "Mission failed… successfully.",
            "Don't worry, Luigi is used to it.",
            "Snake? Snaaaake!",
            "Had fun? That’s the real victory.",
            "A winner is you!",
            "You are already dead. (Nani?!)",
            "Even Dark Souls thinks that was unfair.",
            "Thank you, Mario! But your skills are in another castle.",
            "Wasted.",
            "Try again, we believe in you!"
        };

        private float timer;

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                timer = 0f;
                SpawnText();
            }
        }

        private void SpawnText()
        {
            float xPos = Random.Range(-canvasTransform.rect.width / 2, canvasTransform.rect.width / 2);
            Vector3 spawnPos = new Vector3(xPos, canvasTransform.rect.height + 50, 0f);

            GameObject newText = Instantiate(textPrefab, canvasTransform);
            newText.GetComponent<RectTransform>().anchoredPosition = spawnPos;

            TMP_Text tmp = newText.GetComponent<TMP_Text>();
            tmp.text = messages[Random.Range(0, messages.Length)];
        }
    }
}
