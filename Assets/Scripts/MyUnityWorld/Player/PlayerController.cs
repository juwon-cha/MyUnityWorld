using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyUnityWorld
{
    public class PlayerController : MonoBehaviour
    {
        protected Rigidbody2D _rigidBody;

        [SerializeField] private SpriteRenderer _characterRenderer;
        [SerializeField] private SpriteRenderer _equipmentRenderer;
        [SerializeField] private SpriteRenderer _rideRenderer;
        private EquipmentHandler _equipmentHandler;
        private RideHandler _rideHandler;

        // 이동 방향
        protected Vector2 _movementDirection = Vector2.zero;
        public Vector2 MovementDirection { get { return _movementDirection; } }

        private float _baseSpeed;
        [Range(1, 20)][SerializeField] private float _speed = 3.0f;
        public float Speed
        {
            get => _speed;
            set => _speed = Mathf.Clamp(value, 0f, 20.0f);
        }

        bool _isFacingLeft = false; // 캐릭터가 왼쪽을 바라보고 있는지 여부

        protected AnimationHandler _animationHandler;

        public event Action OnInteractPressed;
        public event Action OnEnterPressed;

        protected void Awake()
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

            GameManager.Instance.LoadPlayerCoordinates(); // 게임 시작 시 플레이어 좌표 불러오기

            _baseSpeed = _speed; // 기본 속도 저장
        }

        protected void Start()
        {
            InteractionManager.Instance.RegisterPlayer(this); // InteractionManager에 플레이어 등록
        }

        protected void Update()
        {
            Rotate(_movementDirection);
        }

        protected void FixedUpdate()
        {
            Movement(_movementDirection);

            GameManager.Instance.SavePlayerCoordinates(); // 매 프레임마다 플레이어 좌표 저장
        }

        public void ChangeColor(Color color)
        {
            if(_characterRenderer != null)
            {
                _characterRenderer.color = color; // 캐릭터의 색상을 변경
            }
            else
            {
                Debug.LogError("Character Renderer is not assigned.");
            }
        }

        public void ChangeEquipment(Sprite equipmentSprite)
        {
            if (_characterRenderer != null)
            {
                // 캐릭터의 장비 활성화
                if (_equipmentRenderer != null)
                {
                    _equipmentRenderer.gameObject.SetActive(true); // 장비 오브젝트 활성화
                    _equipmentRenderer.sprite = equipmentSprite; // 장비 스프라이트 변경
                    _equipmentHandler = _characterRenderer.GetComponentInChildren<EquipmentHandler>();
                }
                else
                {
                    Debug.LogWarning("_equipmentRenderer component not found on character renderer.");
                }
            }
            else
            {
                Debug.LogError("Character Renderer is not assigned.");
            }
        }

        public void ChangeRide(RideHandler handler)
        {
            if (_characterRenderer != null)
            {
                // 캐릭터 탈것 활성화
                if (_rideRenderer != null)
                {
                    _rideRenderer.gameObject.SetActive(true); // 장비 오브젝트 활성화
                    _rideRenderer.sprite = handler.RideRenderer.sprite; // 장비 스프라이트 변경
                    _rideHandler = _characterRenderer.GetComponentInChildren<RideHandler>();
                }
                else
                {
                    Debug.LogWarning("_rideRenderer component not found on character renderer.");
                }
            }
            else
            {
                Debug.LogError("Character Renderer is not assigned.");
            }

            float bonusSpeed = handler.Speed;
            Speed = _baseSpeed + bonusSpeed; // 탈것의 속도를 추가하여 플레이어 속도 증가
        }

        public void RemoveRide()
        {
            if (_rideRenderer != null)
            {
                _rideRenderer.gameObject.SetActive(false); // 탈것 스프라이트 비활성화
                _rideRenderer.sprite = null;
            }

            _rideHandler = null;
            Speed = _baseSpeed; // 속도를 기본 속도로 복원
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
                _isFacingLeft = direction.x < 0;

                // 캐릭터의 로컬 스케일을 조절하여 전체 뒤집기
                if (_isFacingLeft)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }

        private void OnMove(InputValue inputValue)
        {
            _movementDirection = inputValue.Get<Vector2>(); // InputValue를 사용하여 이동 입력 처리
            _movementDirection = _movementDirection.normalized; // 방향 벡터로 정규화
        }

        private void OnInteract(InputValue inputValue)
        {
            if(inputValue.isPressed)
            {
                OnInteractPressed?.Invoke();
            }
        }

        private void OnEnter(InputValue inputValue)
        {
            if (inputValue.isPressed)
            {
                OnEnterPressed?.Invoke();
            }
        }
    }
}
