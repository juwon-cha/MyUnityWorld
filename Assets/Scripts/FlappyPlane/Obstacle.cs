using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyPlane
{
    public class Obstacle : MonoBehaviour
    {
        // 장애물이 상하로 이동할 때 얼마나 이동할지에 대한 수치
        public float HighPosY = 1f;
        public float LowPosY = -1f;

        // 탑과 바텀 사이의 공간을 얼마나 가져갈지에 대한 수치
        public float HoleSizeMin = 1f;
        public float HoleSizeMax = 3f;

        public Transform TopObject;
        public Transform BottomObject;

        public float WidthPadding = 4f;

        private GameManager mGameManager;

        private void Start()
        {
            mGameManager = GameManager.Instance;
        }

        public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
        {
            float holeSize = Random.Range(HoleSizeMin, HoleSizeMax);
            float halfHoleSize = holeSize / 2;

            // 자식 오브젝트(Top, Bottom) localPosition을 변경하면 부모의 위치를 기준으로 움직임
            TopObject.localPosition = new Vector3(0, halfHoleSize, 0); // HoleSize 반만큼 위로 올림
            BottomObject.localPosition = new Vector3(0, -halfHoleSize, 0); // HoleSize 반만큼 아래로 내림

            // 부모 오브젝트(Obstacle)
            // 제일 마지막에 놓여진 장애물 뒤에다가 WidthPadding 만큼 더해서 배치
            Vector3 placePosition = lastPosition + new Vector3(WidthPadding, 0, 0);
            placePosition.y = Random.Range(LowPosY, HighPosY); // 장애물 높이는 랜덤

            transform.position = placePosition;

            return placePosition;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Player player = collision.GetComponent<Player>();
            if (player)
            {
                mGameManager.AddScore(1);
            }
        }
    }

}
