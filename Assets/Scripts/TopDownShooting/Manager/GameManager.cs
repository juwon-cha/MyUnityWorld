using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooting
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public PlayerController Player { get; private set; }
        private ResourceController _playerResourceController;

        [SerializeField] private int _currentWaveIndex = 0;
        private int _bestWave = 0;

        private EnemyManager _enemyManager;
        private UIManager _uiManager;

        public static bool IsFirstLoading = true;

        private void Awake()
        {
            Instance = this;

            Player = FindObjectOfType<PlayerController>();
            Player.Init(this);

            _uiManager = FindObjectOfType<UIManager>();

            _enemyManager = GetComponentInChildren<EnemyManager>();
            _enemyManager.Init(this);

            _playerResourceController = Player.GetComponent<ResourceController>();
            _playerResourceController.RemoveHealthChangeEvent(_uiManager.ChangePlayerHP);
            _playerResourceController.AddHealthChangeEvent(_uiManager.ChangePlayerHP);
        }

        private void Start()
        {
            if(!IsFirstLoading)
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
            Player.IsDead = false;
            _uiManager.SetPlayGame();
            StartNextWave();
        }

        private void StartNextWave()
        {
            ++_currentWaveIndex;

            _enemyManager.StartWave(1 + _currentWaveIndex / 5);

            _uiManager.ChangeWave(_currentWaveIndex); // UI 업데이트
        }

        public void EndOfWave()
        {
            StartNextWave();
        }

        public void GameOver()
        {
            _enemyManager.StopWave();
            _uiManager.SetGameOver();
        }

        public void UpdateBestScore()
        {
            if (_currentWaveIndex > _bestWave)
            {
                _bestWave = _currentWaveIndex;
            }

            string topDownBest = GameData.TopDownBest;

            // 기존 최고 점수와 비교하여 갱신
            for (int i = 0; i < topDownBest.Length; i++)
            {
                int score = topDownBest[i] - '0';

                if (_bestWave > score)
                {
                    // 현재 최고 점수가 기존 최고 점수보다 높으면 갱신
                    topDownBest = topDownBest.Insert(i, _bestWave.ToString());

                    // 최고 점수 7개까지만 저장
                    topDownBest = topDownBest.Remove(topDownBest.Length - 1);
                    break;
                }
            }

            PlayerPrefs.SetString("TopDownBest", topDownBest);
        }
    }
}
