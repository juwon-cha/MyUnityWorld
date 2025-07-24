using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooting
{
    public class WeaponHandler : MonoBehaviour
    {
        [Header("Attack Info")]
        [SerializeField] private float _delay = 1f;
        public float Delay { get => _delay; set => _delay = value; }

        [SerializeField] private float _weaponSize = 1f;
        public float WeaponSize { get => _weaponSize; set => _weaponSize = value; }

        [SerializeField] private float _power = 1f;
        public float Power { get => _power; set => _power = value; }

        [SerializeField] private float _speed = 1f;
        public float Speed { get => _speed; set => _speed = value; }

        [SerializeField] private float _attackRange = 10f;
        public float AttackRange { get => _attackRange; set => _attackRange = value; }

        public LayerMask Target;

        [Header("Knock Back Info")]
        [SerializeField] private bool _IsOnKnockBack = false;
        public bool IsOnKnockBack { get => _IsOnKnockBack; set => _IsOnKnockBack = value; }

        [SerializeField] private float _knockBackPower = 0.1f;
        public float KnockBackPower { get => _knockBackPower; set => _knockBackPower = value; }

        [SerializeField] private float _knockBackDuration = 0.5f;
        public float KnockBackDuration { get => _knockBackDuration; set => _knockBackDuration = value; }

        private static readonly int IsAttack = Animator.StringToHash("IsAttack");

        public BaseController BaseController { get; private set; }
        private Animator _animator;
        private SpriteRenderer _weaponRenderer;

        public AudioClip AttackSoundClip;

        protected virtual void Awake()
        {
            BaseController = GetComponentInParent<BaseController>();
            if (BaseController == null)
            {
                Debug.LogError("BaseController component is missing on " + gameObject.name);
            }

            _animator = GetComponentInChildren<Animator>();
            if (_animator == null)
            {
                Debug.LogError("Animator component is missing on " + gameObject.name);
            }

            _weaponRenderer = GetComponentInChildren<SpriteRenderer>();
            if (_weaponRenderer == null)
            {
                Debug.LogError("SpriteRenderer component is missing on " + gameObject.name);
            }

            _animator.speed = 1.0f / _delay; // 애니메이션 속도를 공격 딜레이에 맞춰 조정
            transform.localScale = Vector3.one * _weaponSize; // 무기 크기 설정
        }

        protected virtual void Start()
        {
            // 초기화 작업이 필요하다면 여기에 작성
        }

        public virtual void Attack()
        {
            AttackAnimation();

            if(AttackSoundClip != null)
            {
                SoundManager.PlayClip(AttackSoundClip);
            }
            else
            {
                Debug.LogWarning("AttackSoundClip is not assigned in " + gameObject.name);
            }
        }

        public void AttackAnimation()
        {
            _animator.SetTrigger(IsAttack);
        }

        public virtual void Rotate(bool isLeft)
        {
            if (_weaponRenderer != null)
            {
                _weaponRenderer.flipY = isLeft; // 왼쪽을 바라보면 flipY를 true로 설정
            }
            else
            {
                Debug.LogWarning("WeaponRenderer is not assigned in " + gameObject.name);
            }
        }
    }
}
