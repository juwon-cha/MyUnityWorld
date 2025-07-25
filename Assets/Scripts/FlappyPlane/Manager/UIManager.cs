using System.Collections;
using System.Collections.Generic;
using TMPro;
using TopDownShooting;
using UnityEngine;

namespace FlappyPlane
{
    public enum EUIState
    {
        HOME,
        GAME,
        GAME_OVER
    }

    public class UIManager : MonoBehaviour
    {
        private HomeUI _homeUI;
        private GameUI _gameUI;
        private GameOverUI _gameOverUI;

        private EUIState _currentState;

        private void Awake()
        {
            _homeUI = GetComponentInChildren<HomeUI>(true);
            _gameUI = GetComponentInChildren<GameUI>(true);
            _gameOverUI = GetComponentInChildren<GameOverUI>(true);
            _homeUI?.Init(this);
            _gameUI?.Init(this);
            _gameOverUI?.Init(this);
            ChangeState(EUIState.HOME);
        }

        public void SetPlayGame()
        {
            ChangeState(EUIState.GAME);
        }

        public void SetGameOver()
        {
            ChangeState(EUIState.GAME_OVER);
        }

        public void UpdateScore(int score)
        {
            _gameUI.UpdateScore(score);
        }

        public void ChangeState(EUIState state)
        {
            _currentState = state;
            _homeUI?.SetActive(_currentState);
            _gameUI?.SetActive(_currentState);
            _gameOverUI?.SetActive(_currentState);
        }
    }
}

