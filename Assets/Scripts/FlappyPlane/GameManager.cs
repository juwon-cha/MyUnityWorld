using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyPlane
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get { return mGameManager; } }

        private static GameManager mGameManager;
        private int mCurrentScore = 0;

        private UIManager mUIManager;
        public UIManager UIManager { get { return mUIManager; } }

        private void Awake()
        {
            mGameManager = this;
            mUIManager = FindObjectOfType<UIManager>();
        }

        private void Start()
        {
            mUIManager.UpdateScore(0);
        }

        public void GameOver()
        {
            Debug.Log("Game Over!");
            mUIManager.SetRestart();
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void AddScore(int score)
        {
            mCurrentScore += score;
            Debug.Log($"Score: {mCurrentScore}");
            mUIManager.UpdateScore(mCurrentScore);
        }
    }
}
