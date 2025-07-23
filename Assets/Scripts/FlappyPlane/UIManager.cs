using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FlappyPlane
{
    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI ScoreText;
        public TextMeshProUGUI RestartText;

        void Start()
        {
            if (RestartText == null)
            {
                Debug.LogError("Restart Text is null");
            }

            if (ScoreText == null)
            {
                Debug.LogError("Score Text is null");
            }

            RestartText.gameObject.SetActive(false);
        }

        public void SetRestart()
        {
            RestartText.gameObject.SetActive(true);
        }

        public void UpdateScore(int score)
        {
            ScoreText.text = score.ToString();
        }
    }
}

