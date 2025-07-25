using TopDownShooting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyUnityWorld
{
    public class TriggerDetection : MonoBehaviour
    {
        private Collider2D _interactableObject; // 상호작용 가능한 대상
        private GameObject _pressPopUp;

        //private void Awake()
        //{
        //    PlayerController playerController = GetComponentInParent<PlayerController>();
        //    if (playerController != null)
        //    {
        //        playerController.OnEnterPressed += HandleEnter;
        //    }
        //}

        private void OnDestroy()
        {
            // 오브젝트가 파괴될 때 이벤트 구독 해제
            PlayerController playerController = GetComponentInParent<PlayerController>();
            if (playerController != null)
            {
                playerController.OnInteractPressed -= HandleInteraction;

                // 엔터키에 대한 구독 해제
                playerController.OnEnterPressed -= HandleEnter;
            }
        }

        // 상호작용이 발생했을 때 호출될 메서드
        public void HandleInteraction()
        {
            // 상호작용 가능한 대상이 범위 안에 있을 때만 로직 실행
            if (_interactableObject != null)
            {
                InteractionManager.Instance.StartInteraction(_interactableObject);
            }
        }

        // 상호작용 중 엔터키를 눌렀을 때 호출될 메서드
        public void HandleEnter()
        {
            // 상호작용 가능한 대상이 범위 안에 있을 때만 로직 실행
            // TODO: NPC는 엔터키 처리 안되게 처리
            if (_interactableObject != null)
            {
                InteractionManager.Instance.EnterEvent(_interactableObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("MiniGame") || collision.CompareTag("NPC") || collision.CompareTag("Mirror") || collision.CompareTag("LeaderBoard"))
            {
                // 상호작용 가능한 대상을 저장
                _interactableObject = collision;

                // "PressF" 게임 오브젝트를 찾아 활성화
                Transform trans = collision.transform.Find("PressF");
                if (trans != null)
                {
                    _pressPopUp = trans.gameObject;
                    _pressPopUp.SetActive(true);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision == _interactableObject)
            {
                _pressPopUp.SetActive(false);
                _pressPopUp = null;

                // 저장된 상호작용 대상을 초기화
                _interactableObject = null;
            }
        }
    }
}
