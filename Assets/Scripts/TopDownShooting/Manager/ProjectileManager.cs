using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooting
{
    public class ProjectileManager : MonoBehaviour
    {
        private static ProjectileManager _instance;
        public static ProjectileManager Instance { get { return _instance; } }

        [SerializeField] private GameObject[] _projectilePrefabs;
        [SerializeField] private ParticleSystem _impactParticleSystem;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPosition, Vector2 direction)
        {
            GameObject origin = _projectilePrefabs[rangeWeaponHandler.BulletIndex];
            GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);

            ProjectileController projectileController = obj.GetComponent<ProjectileController>();
            projectileController.Init(direction, rangeWeaponHandler, this);
        }

        public void CreateImpactParticleAtPosition(Vector3 position, RangeWeaponHandler weaponHandler)
        {
            _impactParticleSystem.transform.position = position;
            ParticleSystem.EmissionModule emission = _impactParticleSystem.emission;
            emission.SetBurst(0, new ParticleSystem.Burst(0f, Mathf.Ceil(weaponHandler.BulletSize * 5f)));

            ParticleSystem.MainModule mainModule = _impactParticleSystem.main;
            mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10f;
            _impactParticleSystem.Play();
        }
    }
}
