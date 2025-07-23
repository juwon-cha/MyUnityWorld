using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooting
{
    public class PlayerController : BaseController
    {
        private Camera _camera;

        protected override void Start()
        {
            base.Start();
            _camera = Camera.main;
            if (_camera == null)
            {
                Debug.LogError("Main camera is not found.");
            }
        }

        protected override void HandleAction()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            _movementDirection = new Vector2(horizontal, vertical).normalized; // 방향 벡터로 정규화

            // 마우스 좌표는 해상도에 따라 다르므로, 카메라를 통해 월드 좌표로 변환해야 함
            Vector2 mousePosition = Input.mousePosition;
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

            _isAttacking = Input.GetMouseButtonDown(0); // 공격 버튼 입력 처리
        }
    }
}
