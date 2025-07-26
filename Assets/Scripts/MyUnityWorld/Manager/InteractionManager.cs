using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUnityWorld
{
    public class InteractionManager : Singleton<InteractionManager>
    {
        private PlayerController _playerController;
        private TriggerDetection _triggerDetection;
        private BaseEvent _baseEvent;
        private DialogueHandler _dialogueHandler;

        public void RegisterPlayer(PlayerController player)
        {
            _playerController = player;
            // TriggerDetection은 Player의 자식
            _triggerDetection = player.GetComponentInChildren<TriggerDetection>();

            // 초기 이벤트 구독 설정
            _playerController.OnInteractPressed += _triggerDetection.HandleInteraction;
        }

        // TriggerDetection이 이 메서드 호출
        public void StartInteraction(Collider2D collision)
        {
            _dialogueHandler = collision.GetComponent<DialogueHandler>();
            if (_dialogueHandler == null)
            {
                return;
            }

            // 이벤트 구독자 교체
            // 기존 TriggerDetection의 구독을 해제
            _playerController.OnInteractPressed -= _triggerDetection.HandleInteraction;
            // 새로운 DialogueHandler의 구독을 추가
            _playerController.OnInteractPressed += _dialogueHandler.HandleInteraction;

            // 대화 UI가 활성화되었을 때 Enter키 이벤트를 처리하도록 설정
            _playerController.OnEnterPressed += _triggerDetection.HandleEnter;

            // 다이얼로그 시작
            _dialogueHandler.StartDialogue();
        }

        // UIManager에서 메인 화면으로 돌아갈 때 이 메서드 호출(상호작용 종료)
        public void EndInteraction()
        {
            if (_dialogueHandler == null)
            {
                return;
            }

            // 이벤트 구독자 복구
            // DialogueHandler의 구독 해제
            _playerController.OnInteractPressed -= _dialogueHandler.HandleInteraction;
            // 다시 TriggerDetection의 구독 추가
            _playerController.OnInteractPressed += _triggerDetection.HandleInteraction;

            // 대화 상호작용이 끝나면 Enter키 이벤트 구독 해제
            _playerController.OnEnterPressed -= _triggerDetection.HandleEnter;

            // 다이얼로그 UI 비활성화
            _dialogueHandler = null;
        }

        // 엔터키 이벤트 처리
        public void EnterEvent(Collider2D collision)
        {
            _baseEvent = collision.GetComponent<BaseEvent>();
            _baseEvent.StartEvent(collision);
        }
    }
}
