using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyUnityWorld
{
    public class PlayerController : MonoBehaviour
    {
        protected Rigidbody2D _rigidBody;

        [SerializeField] private SpriteRenderer _characterRenderer;

        // 이동 방향
        protected Vector2 _movementDirection = Vector2.zero;
        public Vector2 MovementDirection { get { return _movementDirection; } }

        [Range(1, 20)][SerializeField] private float _speed = 3.0f;
        public float Speed
        {
            get => _speed;
            set => _speed = Mathf.Clamp(value, 0f, 20.0f);
        }

        protected AnimationHandler _animationHandler;

        protected virtual void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            if (_rigidBody == null)
            {
                Debug.LogError("Rigidbody2D component is missing on " + gameObject.name);
            }

            _animationHandler = GetComponentInChildren<AnimationHandler>();
            if (_animationHandler == null)
            {
                Debug.LogError("AnimationHandler component is missing on " + gameObject.name);
            }
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            Rotate(_movementDirection);
        }

        protected virtual void FixedUpdate()
        {
            Movement(_movementDirection);
        }

        private void Movement(Vector2 direction)
        {
            direction *= Speed;
            _rigidBody.velocity = direction; // Rigidbody2D의 속도에 적용
            _animationHandler.Move(direction); // 애니메이션 핸들러에 이동 방향 전달
        }

        private void Rotate(Vector2 direction)
        {
            if(direction == Vector2.zero)
            {
                return; // 이동 방향이 없으면 회전하지 않음
            }

            // 좌우 이동 입력이 있었을 경우에만 회전
            if (_movementDirection.x != 0)
            {
                // 이동 방향에 따라 캐릭터의 방향을 결정
                bool isFacingLeft = direction.x < 0;

                _characterRenderer.flipX = isFacingLeft; // 왼쪽을 바라보면 flipX를 true로 설정
            }
        }

        private void OnMove(InputValue inputValue)
        {
            _movementDirection = inputValue.Get<Vector2>(); // InputValue를 사용하여 이동 입력 처리
            _movementDirection = _movementDirection.normalized; // 방향 벡터로 정규화
        }
    }
}
