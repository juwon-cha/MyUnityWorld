using System.Collections;
using System.Collections.Generic;
using TheStack;
using TMPro;
using TopDownShooting;
using UnityEngine;

namespace MyUnityWorld
{
    public enum EUIState
    {
        NONE,
        DIALOGUE,
        GAME,
        CUSTOMIZING,
        LEADERBOARD,
    }

    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private DialogueUI _dialogueUI;
        [SerializeField] private CustomizingUI _customizingUI;
        [SerializeField] private LeaderBoardUI _leaderboardUI;

        public DialogueUI DialogueUI { get => _dialogueUI; private set => _dialogueUI = value; }
        public CustomizingUI CustomizingUI { get => _customizingUI; private set => _customizingUI = value; }
        public LeaderBoardUI LeaderboardUI { get => _leaderboardUI; private set => _leaderboardUI = value; }

        public EUIState CurrentState { get; private set; } = EUIState.NONE;

        // 미니게임에서 복귀했을 때 UI들을 초기화
        public void OnEnable()
        {
            _gameUI = FindObjectOfType<GameUI>(true);
            _dialogueUI = FindObjectOfType<DialogueUI>(true);
            _customizingUI = FindObjectOfType<CustomizingUI>(true);
            _leaderboardUI = FindObjectOfType<LeaderBoardUI>(true);
        }

        public void SetDefaultUIState()
        {
            ChangeState(EUIState.NONE);
        }

        public void SetDialogueUI()
        {
            ChangeState(EUIState.DIALOGUE);
        }

        public void SetCustomizingUI()
        {
            ChangeState(EUIState.CUSTOMIZING);
        }

        public void SetLeaderBoardUI()
        {
            ChangeState(EUIState.LEADERBOARD);
        }

        public void ChangeState(EUIState state)
        {
            CurrentState = state;
            DialogueUI?.SetActive(CurrentState);
            CustomizingUI?.SetActive(CurrentState);
            LeaderboardUI?.SetActive(CurrentState);
            
            // UI 상태가 기본으로 돌아갈 때, 모든 상호작용 정리
            if(CurrentState == EUIState.NONE)
            {
                InteractionManager.Instance.EndInteraction();
            }
        }
    }
}
