using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyUnityWorld
{
    public class LeaderBoardUI : BaseUI
    {
        [SerializeField] private Button _flappyPlaneButton;
        [SerializeField] private Button _theStackButton;
        [SerializeField] private Button _TopdownButton;
        [SerializeField] private Button _exitButton;

        [SerializeField] private ScoreBoardUI _scoreBoardUI;

        protected override EUIState GetUIState()
        {
            return EUIState.LEADERBOARD;
        }

        private void Awake()
        {
            _flappyPlaneButton.onClick.AddListener(OnClickFlappyPlaneButton);
            _theStackButton.onClick.AddListener(OnClickTheStackButton);
            _TopdownButton.onClick.AddListener(OnClickTopDownButton);
            _exitButton.onClick.AddListener(OnClickExitButton);
        }

        // 나가기 버튼을 누르고 다시 UI를 활성화 시키면 Enter 입력 충돌로 바로 UI가 꺼지는 문제를 방지하기 위해
        // UI가 활성화될 때마다 EventSystem이 어떤 버튼도 선택하지 못하도록 명시적으로 막음
        private void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        public void OnClickFlappyPlaneButton()
        {
            _scoreBoardUI.UpdateScore(GameData.FlappyPlaneBest);
        }

        public void OnClickTheStackButton()
        {
            _scoreBoardUI.UpdateScore(GameData.TheStackBest);
        }

        public void OnClickTopDownButton()
        {
            _scoreBoardUI.UpdateScore(GameData.TopDownBest);
        }

        public void OnClickExitButton()
        {
            // 메인 화면으로 전환
            UIManager.Instance.SetDefaultUIState();
        }
    }
}
