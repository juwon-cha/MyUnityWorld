using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooting
{
    public class EnemyManager : MonoBehaviour
    {
        private Coroutine _waveRoutine;

        [SerializeField] private List<GameObject> _enemyPrefabs;

        [SerializeField] private List<Rect> _spawnAreas;
        [SerializeField] private Color _gizmoColor = new Color(1, 0, 0, 0.3f);

        private List<EnemyController> _activeEnemies = new List<EnemyController>();

        private bool _enemySpawnComplete;

        [SerializeField] private float _timeBetweenSpawns = 0.2f;
        [SerializeField] private float _timeBetweenWaves = 1f;

        private GameManager _gameManager;

        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void StartWave(int waveCount)
        {
            if(waveCount <= 0)
            {
                _gameManager.EndOfWave();
                return;
            }

            if(_waveRoutine != null)
            {
                StopCoroutine(_waveRoutine);
            }

            _waveRoutine = StartCoroutine(SpawnWave(waveCount));
        }

        public void StopWave()
        {
            StopAllCoroutines();
        }

        private IEnumerator SpawnWave(int waveCount)
        {
            _enemySpawnComplete = false;

            yield return new WaitForSeconds(_timeBetweenWaves);

            for(int i = 0; i < waveCount; ++i)
            {
                yield return new WaitForSeconds(_timeBetweenSpawns);
                SpawnRandomEnemy();
            }

            _enemySpawnComplete = true;
        }

        private void SpawnRandomEnemy()
        {
            if(_enemyPrefabs.Count == 0 || _spawnAreas.Count == 0)
            {
                Debug.LogWarning("No enemy prefabs or spawn areas defined.");
                return;
            }

            GameObject randomPrefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];

            // 몬스터 생성 영역 랜덤으로 만듦
            Rect randomArea = _spawnAreas[Random.Range(0, _spawnAreas.Count)];

            Vector2 randomPosition = new Vector2(
                Random.Range(randomArea.xMin, randomArea.xMax),
                Random.Range(randomArea.yMin, randomArea.yMax)
            );

            GameObject spawnEnemy = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
            EnemyController enemyController = spawnEnemy.GetComponent<EnemyController>();
            enemyController.Init(this, _gameManager.Player.transform);

            _activeEnemies.Add(enemyController);
        }

        private void OnDrawGizmosSelected()
        {
            if (_spawnAreas == null)
            {
                return;
            }

            Gizmos.color = _gizmoColor;
            foreach (Rect area in _spawnAreas)
            {
                Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
                Vector3 size = new Vector3(area.width, area.height);

                Gizmos.DrawCube(center, size);
            }
        }

        public void RemoveEnemyOnDead(EnemyController enemy)
        {
            if (_activeEnemies.Contains(enemy))
            {
                _activeEnemies.Remove(enemy);
            }

            if (_activeEnemies.Count == 0 && _enemySpawnComplete)
            {
                _gameManager.EndOfWave();
            }
        }
    }
}
