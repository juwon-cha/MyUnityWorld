using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Transform _target;      // 따라갈 대상 (플레이어)
    [SerializeField] private float _smoothSpeed = 5f; // 부드러운 이동 속도

    private Camera _camera;
    private Vector2 _minBounds;     // 카메라가 도달할 수 있는 최소 위치
    private Vector2 _maxBounds;     // 카메라가 도달할 수 있는 최대 위치
    private Vector3 _offset;       // 카메라와 플레이어 간의 초기 거리

    void Start()
    {
        _camera = Camera.main;
        // 카메라 위치를 저장된 플레이어 좌표로 이동
        _camera.transform.position = new Vector3(GameData.PosX, GameData.PosY, -10);
        // 초기 거리 설정
        _offset = transform.position - _target.position;

        if (_tilemap != null)
        {
            // 타일맵의 경계 계산
            _tilemap.CompressBounds();
            BoundsInt bounds = _tilemap.cellBounds;

            // 카메라의 월드 크기 계산
            float camHeight = _camera.orthographicSize * 2;
            float camWidth = camHeight * _camera.aspect;

            // 카메라가 이동할 수 있는 경계 설정
            Vector3 minWorld = _tilemap.CellToWorld(bounds.min);
            Vector3 maxWorld = _tilemap.CellToWorld(bounds.max);

            _minBounds = new Vector2((minWorld.x + 1) + camWidth / 2, minWorld.y + camHeight / 2);
            _maxBounds = new Vector2((maxWorld.x - 1) - camWidth / 2, maxWorld.y - camHeight / 2);
        }
    }

    // LateUpdate()를 사용하는 이유는 모든 캐릭터 이동이 끝난 후에 카메라가 따라가는 연출을 만들기 위함
    void LateUpdate()
    {
        // 따라가야 할 위치 계산 (z는 유지)
        Vector3 desiredPosition = _target.position + _offset;
        desiredPosition.z = transform.position.z;

        // 위치 제한 적용
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, _minBounds.x, _maxBounds.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, _minBounds.y, _maxBounds.y);

        // 부드럽게 이동
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * _smoothSpeed);
    }
}
