using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace TopDownShooting
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private LayerMask _levelCollisionLayer;

        private RangeWeaponHandler _rangeWeaponHandler;

        private float _currentDuration;
        private Vector2 _direction;
        private bool _isReady;
        private Transform _pivot;

        private Rigidbody2D _rigibody;
        private SpriteRenderer _spriteRenderer;

        public bool FxOnDestroy = true;

        private ProjectileManager _projectileManager;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _rigibody = GetComponent<Rigidbody2D>();
            _pivot = transform.GetChild(0);
        }

        private void Update()
        {
            if(!_isReady)
            {
                return;
            }

            _currentDuration += Time.deltaTime;

            if(_currentDuration > _rangeWeaponHandler.Duration)
            {
                DestroyProjectile(transform.position, false);
            }

            _rigibody.velocity = _direction * _rangeWeaponHandler.Speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // 벽 충돌
            if (_levelCollisionLayer.value == (_levelCollisionLayer.value | (1 << collision.gameObject.layer)))
            {
                DestroyProjectile(collision.ClosestPoint(transform.position) - _direction * 0.2f, FxOnDestroy);
            }
            // 타겟과 충돌
            else if(_rangeWeaponHandler.Target.value == (_rangeWeaponHandler.Target.value | (1 << collision.gameObject.layer)))
            {
                // 데미지 계산
                ResourceController targetResource = collision.GetComponent<ResourceController>();
                if (targetResource != null)
                {
                    targetResource.ChangeHealth(-_rangeWeaponHandler.Power);
                    if(_rangeWeaponHandler.IsOnKnockBack)
                    {
                        // 넉백 처리
                        BaseController controller = collision.GetComponent<BaseController>();
                        if(controller != null)
                        {
                            controller.ApplyKnockBack(transform, _rangeWeaponHandler.KnockBackPower, _rangeWeaponHandler.KnockBackDuration);
                        }
                    }
                }

                DestroyProjectile(collision.ClosestPoint(transform.position), FxOnDestroy);
            }
        }

        public void Init(Vector2 direction, RangeWeaponHandler weaponHandler, ProjectileManager projectileManager)
        {
            _projectileManager = projectileManager;
            _rangeWeaponHandler = weaponHandler;

            _direction = direction;
            _currentDuration = 0f;
            transform.localScale = Vector3.one * weaponHandler.BulletSize;
            _spriteRenderer.color = weaponHandler.ProjectileColor;

            // 오브젝트의 오른쪽을 _direction 방향으로 바라보게 회전
            transform.right = _direction;

            if (direction.x < 0)
            {
                // 피벗을 회전 시켜줘야 투사체가 제대로 보인다(?)
                _pivot.localRotation = Quaternion.Euler(180, 0, 0);
            }
            else
            {
                _pivot.localRotation = Quaternion.Euler(0, 0, 0);
            }

            _isReady = true;
        }

        private void DestroyProjectile(Vector3 position, bool createFx)
        {
            if(createFx)
            {
                _projectileManager.CreateImpactParticleAtPosition(position, _rangeWeaponHandler);
            }

            Destroy(gameObject);
        }
    }
}
