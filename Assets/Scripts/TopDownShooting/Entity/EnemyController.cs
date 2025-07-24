using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooting
{
    public class EnemyController : BaseController
    {
        private EnemyManager _enemyManager;
        private Transform _target;

        [SerializeField] private float _followRange = 15f;

        public void Init(EnemyManager enemyManage, Transform target)
        {
            _enemyManager = enemyManage;
            _target = target;
        }

        protected float DistanceToTarget()
        {
            return Vector3.Distance(transform.position, _target.position);
        }

        protected Vector2 DirectionToTarget()
        {
            return (_target.position - transform.position).normalized; // 방향 계산
        }

        protected override void HandleAction()
        {
            base.HandleAction();

            if(_weaponHandler == null || _target == null)
            {
                if(!_movementDirection.Equals(Vector2.zero))
                {
                    _movementDirection = Vector2.zero; // 타겟이 없거나 무기가 없으면 이동하지 않음
                    return;
                }
            }

            float distance = DistanceToTarget();
            Vector2 direction = DirectionToTarget();

            _isAttacking = false;

            // 플레이어를 따라갈 범위 안에 들어왔다면
            if(distance <= _followRange)
            {
                // 플레이어를 바라봄
                _lookDirection = direction;

                // 공격 범위 안에 들어왔다면
                if(distance < _weaponHandler.AttackRange)
                {
                    // 레이캐스트로 충돌 검사
                    // 레이어마스크를 통해 충돌 구분
                    int layerMaskTarget = _weaponHandler.Target;
                    RaycastHit2D hit = Physics2D.Raycast(transform.position,
                        direction, _weaponHandler.AttackRange * 1.5f,
                        (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                    if(hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                    {
                        _isAttacking = true;
                    }

                    _movementDirection = Vector2.zero;
                    return;
                }

                _movementDirection = direction;
            }
        }

        public override void OnDead()
        {
            base.OnDead();

            _enemyManager.RemoveEnemyOnDead(this);
        }
    }
}
