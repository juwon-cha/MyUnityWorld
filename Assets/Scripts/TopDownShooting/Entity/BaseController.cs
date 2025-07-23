using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

namespace TopDownShooting
{
    public class BaseController : MonoBehaviour
    {
        protected Rigidbody2D _rigidBody;

        [SerializeField] private SpriteRenderer _characterRenderer;
        [SerializeField] private Transform _weaponPivot;

        // 이동 방향
        protected Vector2 _movementDirection = Vector2.zero;
        public Vector2 MovementDirection { get { return _movementDirection; } }

        // 바라보는 방향
        protected Vector2 _lookDirection = Vector2.zero;
        public Vector2 LookDirection { get { return _lookDirection; } }

        private Vector2 _knockBack = Vector2.zero;
        private float _knockBackDuration = 0.0f;
        
        protected AnimationHandler _animationHandler;
        protected StatHandler _statHandler;

        [SerializeField] private WeaponHandler _weaponPrefab;
        protected WeaponHandler _weaponHandler;

        protected bool _isAttacking;
        private float _timeSinceLastAttack = float.MaxValue;

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

            _statHandler = GetComponent<StatHandler>();
            if (_statHandler == null)
            {
                Debug.LogError("StatHandler component is missing on " + gameObject.name);
            }

            if (_weaponPrefab != null)
            {
                _weaponHandler = Instantiate(_weaponPrefab, _weaponPivot);
                if (_weaponHandler == null)
                {
                    Debug.LogError("WeaponHandler component is missing on " + gameObject.name);
                }
            }
            else
            {
                // 이미 WeaponHandler가 자식으로 존재하는 경우
                _weaponHandler = GetComponentInChildren<WeaponHandler>();
            }
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            HandleAction();
            Rotate(_lookDirection);
            HandleAttackDelay();
        }

        protected virtual void FixedUpdate()
        {
            Movement(_movementDirection);
            if(_knockBackDuration > 0.0f) // 넉백 지속시간이 남아있다면
            {
                _knockBackDuration -= Time.fixedDeltaTime; // 넉백 지속시간 감소
            }
        }

        protected virtual void HandleAction()
        {

        }

        private void Movement(Vector2 direction)
        {
            direction *= _statHandler.Speed;
            if (_knockBackDuration > 0.0f) // 넉백 지속시간이 남아있다면
            {
                direction *= 0.2f; // 기존 이동방향의 힘을 줄여줌
                direction += _knockBack; // 넉백의 힘만 넣어주겠다.
            }

            _rigidBody.velocity = direction; // Rigidbody2D의 속도에 적용
            _animationHandler.Move(direction); // 애니메이션 핸들러에 이동 방향 전달
        }

        private void Rotate(Vector2 direction)
        {
            // y값과 x값을 받아서 그 사이의 세타값을 구한다. 라디안 값이 나옴 -> 라디안을 각도로 바꿈
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            bool bIsLeft = Mathf.Abs(rotZ) > 90f; // 90도 보다 크면 왼쪽을 바라보고 있다고 판단

            _characterRenderer.flipX = bIsLeft; // 왼쪽을 바라보면 flipX를 true로 설정

            if(_weaponPivot != null)
            {
                // 무기 피봇을 바라보는 방향으로 회전
                _weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
            }

            _weaponHandler?.Rotate(bIsLeft); // 무기 핸들러에 회전 방향 전달
        }

        public void ApplyKnockBack(Transform other, float power, float duration)
        {
            _knockBackDuration = duration; // 넉백 지속시간 설정
            _knockBack = -(other.position - transform.position).normalized * power; // 넉백 방향 설정
        }

        private void HandleAttackDelay()
        {
            if(_weaponHandler == null)
            {
                return; // 무기 핸들러가 없으면 공격 딜레이를 처리하지 않음
            }

            if(_timeSinceLastAttack <= _weaponHandler.Delay)
            {
                _timeSinceLastAttack += Time.deltaTime; // 공격 딜레이 시간 증가
            }
            
            if(_isAttacking && _timeSinceLastAttack > _weaponHandler.Delay)
            {
                _timeSinceLastAttack = 0f; // 공격 딜레이 시간 초기화
                Attack(); // 공격 딜레이가 끝나면 공격 실행
            }
        }

        protected virtual void Attack()
        {
            if(_lookDirection != Vector2.zero)
            {
                _weaponHandler?.Attack();
            }
        }
    }
}
