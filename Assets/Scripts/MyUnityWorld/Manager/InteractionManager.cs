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
            _playerController.OnEnterPressed += _triggerDetection.HandleEnter;
        }

        // TriggerDetection이 이 메서드 호출
        public void StartInteraction(Collider2D collision)
        {
            _dialogueHandler = collision.GetComponent<DialogueHandler>();
            if (_dialogueHandler == null) return;

            // 이벤트 구독자 교체
            // 기존 TriggerDetection의 구독을 해제
            _playerController.OnInteractPressed -= _triggerDetection.HandleInteraction;
            // 새로운 DialogueHandler의 구독을 추가
            _playerController.OnInteractPressed += _dialogueHandler.HandleInteraction;

            // 다이얼로그 시작
            _dialogueHandler.StartDialogue();
        }

        // DialogueHandler가 다이얼로그 종료 시 이 메서드 호출
        public void EndInteraction()
        {
            if (_dialogueHandler == null) return;

            // 이벤트 구독자 복구
            // DialogueHandler의 구독 해제
            _playerController.OnInteractPressed -= _dialogueHandler.HandleInteraction;
            // 다시 TriggerDetection의 구독 추가
            _playerController.OnInteractPressed += _triggerDetection.HandleInteraction;

            _dialogueHandler = null;
        }

        // TODO: 엔터 입력 시 미니게임 실행/리더보드UI/거울UI 등 다른 이벤트 처리 추가
        public void EnterEvent(Collider2D collision)
        {
            _baseEvent = collision.GetComponent<BaseEvent>();
            _baseEvent.StartEvent(collision);
        }
    }
}
