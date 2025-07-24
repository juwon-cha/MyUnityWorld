using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace TopDownShooting
{
    public class PlayerController : BaseController
    {
        private Camera _camera;
        private GameManager _gameManager;

        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
            _camera = Camera.main;
        }

        protected override void HandleAction()
        {
            //// OnMove
            //// 이동 입력 처리
            //float horizontal = Input.GetAxisRaw("Horizontal");
            //float vertical = Input.GetAxisRaw("Vertical");
            //_movementDirection = new Vector2(horizontal, vertical).normalized; // 방향 벡터로 정규화

            //// OnLook
            //// 마우스 좌표는 해상도에 따라 다르므로, 카메라를 통해 월드 좌표로 변환해야 함
            //Vector2 mousePosition = Input.mousePosition;
            //Vector2 worldPosition = _camera.ScreenToWorldPoint(mousePosition);

            //// 플레이어 위치에서 마우스 위치로 향하는 방향 벡터
            //_lookDirection = (worldPosition - (Vector2)transform.position);
            //if (_lookDirection.magnitude < 0.9f)
            //{
            //    _lookDirection = Vector2.zero;
            //}
            //else
            //{
            //    _lookDirection = _lookDirection.normalized; // 정규화하여 방향만 사용
            //}

            //// OnFire
            //_isAttacking = Input.GetMouseButtonDown(0); // 공격 버튼 입력 처리
        }

        public override void OnDead()
        {
            base.OnDead();

            _gameManager.GameOver(); // 게임 매니저에 게임 오버 알림
        }

        private void OnMove(InputValue inputValue)
        {
            //float horizontal = Input.GetAxisRaw("Horizontal");
            //float vertical = Input.GetAxisRaw("Vertical");
            //_movementDirection = new Vector2(horizontal, vertical).normalized; // 방향 벡터로 정규화

            _movementDirection = inputValue.Get<Vector2>(); // InputValue를 사용하여 이동 입력 처리
            _movementDirection = _movementDirection.normalized; // 방향 벡터로 정규화
        }

        private void OnLook(InputValue inputValue)
        {
            //// 마우스 좌표는 해상도에 따라 다르므로, 카메라를 통해 월드 좌표로 변환해야 함
            //Vector2 mousePosition = Input.mousePosition;
            Vector2 mousePosition = inputValue.Get<Vector2>();
            Vector2 worldPosition = _camera.ScreenToWorldPoint(mousePosition);

            // 플레이어 위치에서 마우스 위치로 향하는 방향 벡터
            _lookDirection = (worldPosition - (Vector2)transform.position);
            if (_lookDirection.magnitude < 0.9f)
            {
                _lookDirection = Vector2.zero;
            }
            else
            {
                _lookDirection = _lookDirection.normalized; // 정규화하여 방향만 사용
            }
        }

        private void OnFire(InputValue inputValue)
        {
            //_isAttacking = Input.GetMouseButtonDown(0); // 공격 버튼 입력 처리

            if(EventSystem.current.IsPointerOverGameObject())
            {
                // UI 요소 위에 있을 때는 공격하지 않음
                return;
            }

            _isAttacking = inputValue.isPressed; // InputValue를 사용하여 공격 버튼 입력 처리
        }
    }
}
