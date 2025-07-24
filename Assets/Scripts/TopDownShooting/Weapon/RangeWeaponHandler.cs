using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooting
{
    public class RangeWeaponHandler : WeaponHandler
    {
        [Header("Range Attack Data")]
        [SerializeField] private Transform _projectileSpawnPosition;

        [SerializeField] private int _bulletIndex;
        public int BulletIndex { get { return _bulletIndex; } }

        [SerializeField] private float _bulletSize = 1f;
        public float BulletSize { get { return _bulletSize; } }

        [SerializeField] private float _duration;
        public float Duration { get { return _duration; } }

        [SerializeField] private float _spread; // 탄 퍼짐
        public float Spread { get { return _spread; } }

        [SerializeField] private int _numberOfProjectilesPerShot; // 발사할 총알 개수
        public int NumberOfProjectilesPerShot { get { return _numberOfProjectilesPerShot; } }

        [SerializeField] private float _multipleProjectileAngle; // 각각의 탄이 발사되는 각도
        public float MultipleProjectileAngle { get { return _multipleProjectileAngle; } }

        [SerializeField] private Color _projectileColor;
        public Color ProjectileColor { get { return _projectileColor; } }

        private ProjectileManager _projectileManager;

        protected override void Start()
        {
            base.Start();
            _projectileManager = ProjectileManager.Instance;
            if (_projectileManager == null)
            {
                Debug.LogError("ProjectileManager instance is missing.");
            }
        }

        public override void Attack()
        {
            base.Attack();

            float projectileAngleSpace = _multipleProjectileAngle;
            int numberOfProjectilePerShot = _numberOfProjectilesPerShot;

            float minAngle = -(numberOfProjectilePerShot / 2f) * projectileAngleSpace;

            for(int i = 0; i < numberOfProjectilePerShot; ++i)
            {
                float angle = minAngle + (i * projectileAngleSpace);
                float randomSpread = Random.Range(-_spread, _spread);
                angle += randomSpread;

                CreateProjectile(BaseController.LookDirection, angle);
            }
        }

        private void CreateProjectile(Vector2 lookDirection, float angle)
        {
            _projectileManager.ShootBullet(
                this,
                _projectileSpawnPosition.position,
                RotateVector2(lookDirection, angle)
                );
        }

        private static Vector2 RotateVector2(Vector2 v, float degree)
        {
            // Quaternion이 가지는 회전의 수치 만큼 v를 회전시킴
            // 행렬 곱셈을 통해 벡터를 회전시키는 방법 -> 교환 법칙이 성립하지 않아서 순서에 유의
            return Quaternion.Euler(0, 0, degree) * v;
        }
    }
}
