using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace TheStack
{
    // TODO: Block과 Rubble을 오브젝트 풀링을 통해 재사용할 수 있도록 개선

    public class TheStack : MonoBehaviour
    {
        private const float mBoundSize = 3.5f; // 블럭 사이즈
        private const float mMovingBoundsSize = 3f; // 블럭이 이동하는 양
        private const float mStackMovingSpeed = 5f;
        private const float mBlockMovingSpeed = 5f;
        private const float mErrorMargin = 1.0f; // 성공으로 취급할 에러 마진

        public GameObject OriginBlock = null;

        private Vector3 mPrevBlockPos;
        private Vector3 mDesiredPos;
        private Vector3 mStackBounds = new Vector2(mBoundSize, mBoundSize); // 새로 생성되는 블럭 사이즈

        // 새로운 블럭을 생성하기 위한 변수들
        private Transform mLastBlock = null;
        private float mBlockTransition = 0f;
        private float mSecondaryPos = 0f;

        // 점수와 콤보
        public int BestScore { get => mBestScore; }
        public int Score { get { return mStackCount; } }
        public int Combo { get { return mComboCount; } }
        public int MaxCombo { get => mMaxCombo; }
        public int BestCombo { get => mBestCombo; }
        private int mStackCount = -1;
        private int mComboCount = -1;
        private int mMaxCombo = 0;
        private int mBestScore = 0;
        private int mBestCombo = 0;

        private const string BestScoreKey = "BestScore";
        private const string BestComboKey = "BestCombo";

        // 게임 오버
        private bool mbIsGameOver = true;

        // 새로운 블럭 색 설정
        public Color PrevColor;
        public Color NextColor;

        private bool mbIsMovingX = true;

        void Start()
        {
            if (OriginBlock == null)
            {
                Debug.LogError($"OriginBlock is NULL");
                return;
            }

            // 최고 점수와 최고 콤보 로드
            mBestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
            mBestCombo = PlayerPrefs.GetInt(BestComboKey, 0);

            // 색 설정
            PrevColor = GetRandomColor();
            NextColor = GetRandomColor();

            mPrevBlockPos = Vector3.down;

            SpawnBlock();
            SpawnBlock();
        }

        void Update()
        {
            if (mbIsGameOver)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (PlaceBlock())
                {
                    SpawnBlock();
                }
                else
                {
                    // 게임 오버
                    Debug.Log($"Game Over!");
                    UpdateScore();
                    mbIsGameOver = true;
                    ActivateGameOverEffect();
                    UIManager.Instance.SetScoreUI();
                }
            }

            MoveBlock();

            // TheStack 움직임
            transform.position = Vector3.Lerp(transform.position, mDesiredPos, mStackMovingSpeed * Time.deltaTime);
        }

        private bool SpawnBlock()
        {
            if (mLastBlock != null)
            {
                mPrevBlockPos = mLastBlock.localPosition;
            }

            GameObject newBlock = null;
            Transform newTrans = null;

            newBlock = Instantiate(OriginBlock);
            newBlock.name = "Block";

            if (newBlock == null)
            {
                Debug.LogError($"NewBlock instantiate failed");
                return false;
            }

            // 블럭 색 변경
            ColorChange(newBlock);

            newTrans = newBlock.transform;
            newTrans.parent = this.transform;
            newTrans.localPosition = mPrevBlockPos + Vector3.up; // 블록을 한 칸 올림
            newTrans.localRotation = Quaternion.identity; // 회전 없음
            newTrans.localScale = new Vector3(mStackBounds.x, 1, mStackBounds.y);

            ++mStackCount;

            // 블럭이 쌓이면 TheStack을 내림 -> 가장 마지막에 쌓은 블럭이 화면 중앙에 위치해 있도록 하기 위함
            mDesiredPos = Vector3.down * mStackCount;
            mBlockTransition = 0f; // 이동 처리를 위한 기준값

            mLastBlock = newTrans;

            mbIsMovingX = !mbIsMovingX;

            UIManager.Instance.UpdateScore();

            return true;
        }

        private Color GetRandomColor()
        {
            float r = Random.Range(100f, 250f) / 255f;
            float g = Random.Range(100f, 250f) / 255f;
            float b = Random.Range(100f, 250f) / 255f;

            return new Color(r, g, b);
        }

        private void ColorChange(GameObject go)
        {
            // PrevColor부터 NextColor까지의 중간 값들의 컬러들이 스택 카운트에 맞춰서 생성
            Color applyColor = Color.Lerp(PrevColor, NextColor, (mStackCount % 11/*0~10까지 순환*/) / 10f);

            // Renderer는 게임 오브젝트를 그려주는 역할?
            // 블럭이 가지고 있는 Renderer는 MeshRenderer
            // MeshRenderer의 부모가 Renderer
            Renderer rn = go.GetComponent<Renderer>();
            if (rn == null)
            {
                Debug.LogError("Renderer is NULL");
                return;
            }

            rn.material.color = applyColor;

            Camera.main.backgroundColor = applyColor - new Color(0.1f, 0.1f, 0.1f);

            if (applyColor.Equals(NextColor) == true)
            {
                PrevColor = NextColor;
                NextColor = GetRandomColor();
            }
        }

        private void MoveBlock()
        {
            mBlockTransition += Time.deltaTime * mBlockMovingSpeed;

            // mBoundSize의 중심을 기준으로 전체 mBoundSize 만큼 블럭 이동할 수 있는 크기
            // pinpong: 0부터 정해준 값까지 순환
            float movePos = Mathf.PingPong(mBlockTransition, mBoundSize) - mBoundSize / 2;

            if (mbIsMovingX)
            {
                mLastBlock.localPosition = new Vector3(movePos * mMovingBoundsSize, mStackCount, mSecondaryPos);
            }
            else
            {
                mLastBlock.localPosition = new Vector3(mSecondaryPos, mStackCount, -movePos * mMovingBoundsSize);
            }
        }

        private bool PlaceBlock()
        {
            Vector3 lastPos = mLastBlock.localPosition;

            if (mbIsMovingX)
            {
                // 이전 블럭과 배치한 블럭의 중심 좌표들의 차
                float deltaX = mPrevBlockPos.x - lastPos.x;
                bool bIsNegativeNum = (deltaX < 0) ? true : false; // Rubble 생성 방향 결정

                deltaX = Mathf.Abs(deltaX);
                if (deltaX > mErrorMargin)
                {
                    mStackBounds.x -= deltaX;
                    if (mStackBounds.x <= 0)
                    {
                        return false;
                    }

                    // 배치한 블록을 이전 블록 위에서 이탈한 만큼을 제외한 블럭 위치로 만들어줌
                    // 두 블럭의 중심 지점
                    float middle = (mPrevBlockPos.x + lastPos.x) / 2f;
                    mLastBlock.localScale = new Vector3(mStackBounds.x, 1, mStackBounds.y);

                    Vector3 tempPos = mLastBlock.localPosition;
                    tempPos.x = middle;

                    lastPos = tempPos;
                    mLastBlock.localPosition = lastPos;

                    float rubbleHalfScale = deltaX / 2f;
                    CreateRubble(
                        new Vector3(
                        bIsNegativeNum
                        ? lastPos.x + mStackBounds.x / 2 + rubbleHalfScale
                        : lastPos.x - mStackBounds.x / 2 - rubbleHalfScale
                        , lastPos.y
                        , lastPos.z),
                        new Vector3(deltaX, 1, mStackBounds.y)
                    );

                    // 콤보 초기화
                    mComboCount = 0;
                }
                else
                {
                    ComboCheck();

                    // 제대로 배치됨 -> 위치 보정
                    mLastBlock.localPosition = mPrevBlockPos + Vector3.up;
                }
            }
            else
            {
                float deltaZ = mPrevBlockPos.z - lastPos.z;
                bool bIsNegativeNum = (deltaZ < 0) ? true : false; // Rubble 생성 방향 결정

                deltaZ = Mathf.Abs(deltaZ);
                if (deltaZ > mErrorMargin)
                {
                    mStackBounds.y -= deltaZ;
                    if (mStackBounds.y <= 0)
                    {
                        return false;
                    }

                    float middle = (mPrevBlockPos.z + lastPos.z) / 2f;
                    mLastBlock.localScale = new Vector3(mStackBounds.x, 1, mStackBounds.y);

                    Vector3 tempPos = mLastBlock.localPosition;
                    tempPos.z = middle;

                    lastPos = tempPos;
                    mLastBlock.localPosition = lastPos;

                    float rubbleHalfScale = deltaZ / 2f;
                    CreateRubble(
                        new Vector3(
                        lastPos.x,
                        lastPos.y,
                        bIsNegativeNum
                        ? lastPos.z + mStackBounds.y / 2 + rubbleHalfScale
                        : lastPos.z - mStackBounds.y / 2 - rubbleHalfScale),
                        new Vector3(mStackBounds.x, 1, deltaZ)
                    );

                    // 콤보 초기화
                    mComboCount = 0;
                }
                else
                {
                    ComboCheck();

                    // 제대로 배치됨 -> 위치 보정
                    mLastBlock.localPosition = mPrevBlockPos + Vector3.up;
                }
            }

            mSecondaryPos = (mbIsMovingX) ? mLastBlock.localPosition.x : mLastBlock.localPosition.z;

            return true;
        }

        // 잘려진 파편 생성
        private void CreateRubble(Vector3 pos, Vector3 scale)
        {
            GameObject go = Instantiate(mLastBlock.gameObject);
            go.transform.parent = this.transform; // TheStack의 자식으로 설정

            go.transform.localPosition = pos;
            go.transform.localScale = scale;
            go.transform.localRotation = Quaternion.identity;

            go.AddComponent<Rigidbody>();
            go.name = "Rubble";
        }

        private void ComboCheck()
        {
            ++mComboCount;

            if (mComboCount > mMaxCombo)
            {
                mMaxCombo = mComboCount;
            }

            if (mComboCount % 5 == 0)
            {
                Debug.Log("5 Combo Success!");

                // 콤보 성공 시 배치할 블럭 크기 증가
                mStackBounds += new Vector3(0.5f, 0.5f);
                // 초기 블럭 크기보다 커지지 않게 설정
                mStackBounds.x = (mStackBounds.x > mBoundSize) ? mBoundSize : mStackBounds.x;
                mStackBounds.y = (mStackBounds.y > mBoundSize) ? mBoundSize : mStackBounds.y;
            }
        }

        private void UpdateScore()
        {
            if (mBestScore < mStackCount)
            {
                Debug.Log($"최고 점수 갱신");
                mBestScore = mStackCount;
                mBestCombo = mMaxCombo;

                PlayerPrefs.SetInt(BestScoreKey, mBestScore);
                PlayerPrefs.SetInt(BestComboKey, mBestCombo);
                PlayerPrefs.Save();
            }
        }

        private void ActivateGameOverEffect()
        {
            int childCount = this.transform.childCount;

            for (int i = 1; i < 20; ++i)
            {
                if (childCount < i)
                {
                    break;
                }

                GameObject go = transform.GetChild(childCount - i).gameObject; // 제일 끝에 있는 게임 오브젝트

                if (go.name.Equals("Rubble"))
                {
                    continue;
                }

                Rigidbody rigid = go.AddComponent<Rigidbody>();
                rigid.AddForce(
                    (Vector3.up * Random.Range(0, 10f) + Vector3.right * (Random.Range(0, 10f) - 5f)) * 100f
                    );
            }
        }

        public void Restart()
        {
            int childCount = transform.childCount;

            for(int i = 0; i < childCount; ++i)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            mbIsGameOver = false;

            mLastBlock = null;
            mDesiredPos = Vector3.zero;
            mStackBounds = new Vector3(mBoundSize, mBoundSize);

            mStackCount = -1;
            mbIsMovingX = true;
            mBlockTransition = 0f;
            mSecondaryPos = 0f;

            mComboCount = 0;
            mMaxCombo = 0;

            mPrevBlockPos = Vector3.down;

            PrevColor = GetRandomColor();
            NextColor = GetRandomColor();

            SpawnBlock(); // 처음 블럭
            SpawnBlock(); // 이동 블럭
        }
    }
}

