using MyUnityWorld;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MyUnityWorld
{
    enum EDialogueState
    {
        NONE,
        TYPING,
        COMPLETED,
    }

    public class DialogueHandler : MonoBehaviour
    {
        [SerializeField] private List<string> _dialogueLines = new List<string>();
        [SerializeField] private TextMeshProUGUI _dialogueTxt;
        [SerializeField] private float _typingSpeed = 0.05f; // 타자기 효과 속도

        private EDialogueState _dialogueState = EDialogueState.NONE;

        // 다이얼로그 시작
        public void StartDialogue()
        {
            UIManager.Instance.ShowDialogueUI();

            StartCoroutine(ShowLines());
        }

        private IEnumerator ShowLines()
        {
            _dialogueState = EDialogueState.TYPING;
            _dialogueTxt.text = ""; // 텍스트 초기화

            string fullText = string.Join("\n", _dialogueLines);

            foreach (char letter in fullText)
            {
                _dialogueTxt.text += letter;
                yield return new WaitForSeconds(_typingSpeed);
            }

            _dialogueState = EDialogueState.COMPLETED;
        }

        private void EndDialogue()
        {
            StopAllCoroutines();
            UIManager.Instance.DisableDialogueUI();

            // InteractionManager에 대화 종료를 알림(구독 전환)
            InteractionManager.Instance.EndInteraction();
        }

        // F키 입력 처리
        public void HandleInteraction()
        {
            switch(_dialogueState)
            {
                case EDialogueState.TYPING:
                    // 타자기 효과가 진행 중일 때 F키를 누르면 즉시 모든 텍스트를 보여줌
                    // 코루틴을 중지하고 전체 텍스트를 즉시 표시 (스킵)
                    StopAllCoroutines();
                    _dialogueTxt.text = string.Join("\n", _dialogueLines);
                    _dialogueState = EDialogueState.COMPLETED; // 상태를 '완료'로 변경
                    break;

                case EDialogueState.COMPLETED:
                    EndDialogue();
                    break;

                default:
                    break;
            }
        }
    }
}
