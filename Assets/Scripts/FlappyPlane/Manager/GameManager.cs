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

        public UIManager UIManager { get { return mUIManager; } }
        private UIManager mUIManager;

        [SerializeField] private Player _player;

        private int mCurrentScore = 0;
        public int BestScore { get; private set; }

        public bool IsGameOver { get; private set; } = true;
        public static bool IsFirstLoading = true;

        private void Awake()
        {
            mGameManager = this;
            mUIManager = FindObjectOfType<UIManager>();
        }

        private void Start()
        {
            mUIManager.UpdateScore(0);

            if (!IsFirstLoading)
            {
                // 재시작 눌렀을 때, HomeUI 안 보여주고 바로 게임 시작
                StartGame();
            }
            else
            {
                IsFirstLoading = false;
            }
        }

        public void StartGame()
        {
            UIManager.SetPlayGame();
            IsGameOver = false;

            _player.ActivatePlayer();
        }

        public void GameOver()
        {
            mUIManager.SetGameOver();
        }

        public void AddScore(int score)
        {
            mCurrentScore += score;
            Debug.Log($"Score: {mCurrentScore}");
            mUIManager.UpdateScore(mCurrentScore);
        }

        public void UpdateBestScore()
        {
            if (mCurrentScore > BestScore)
            {
                BestScore = mCurrentScore;
            }

            string flappyPlaneBest = GameData.FlappyPlaneBest;

            // 기존 최고 점수와 비교하여 갱신
            for (int i = 0; i < flappyPlaneBest.Length; i++)
            {
                int score = flappyPlaneBest[i] - '0';

                if (BestScore > score)
                {
                    // 현재 최고 점수가 기존 최고 점수보다 높으면 갱신
                    flappyPlaneBest = flappyPlaneBest.Insert(i, BestScore.ToString());

                    // 최고 점수 7개까지만 저장
                    flappyPlaneBest = flappyPlaneBest.Remove(flappyPlaneBest.Length - 1);
                    break;
                }
            }

            PlayerPrefs.SetString("FlappyPlaneBest", flappyPlaneBest);
        }
    }
}
