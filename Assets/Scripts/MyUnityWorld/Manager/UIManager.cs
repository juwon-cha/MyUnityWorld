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
    }

    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private DialogueUI _dialogueUI;
        [SerializeField] private GameUI _gameUI;

        private EUIState _currentState;

        private void Awake()
        {
            ChangeState(EUIState.GAME);
        }

        public void ShowDialogueUI()
        {
            _dialogueUI.SetActive(EUIState.DIALOGUE);
        }

        public void DisableDialogueUI()
        {
            if (_dialogueUI != null)
            {
                _dialogueUI.SetActive(EUIState.NONE);
            }
        }

        public void ChangeState(EUIState state)
        {
            _currentState = state;
            _dialogueUI?.SetActive(_currentState);
        }
    }
}
