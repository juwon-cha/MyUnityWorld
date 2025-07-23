using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyPlane
{
    public class Player : MonoBehaviour
    {
        private Animator mAnimator;
        private Rigidbody2D mRigidbody;

        public float FlapForce = 6f; // 점프하는 힘
        public float ForwardSpeed = 3f; // 앞으로 이동하는 속도
        public bool IsDead = false; // 플레이어가 죽었는지 여부
        private float mDeathCooldown = 0.5f; // 충돌 후 바로 죽는게 아니라 0.5초 후에 죽도록 설정

        private bool mIsFlap = false; // 플레이어가 점프 중인지 여부

        public bool GodMode = false; // 테스트 모드

        private GameManager mGameManager;

        void Start()
        {
            mGameManager = GameManager.Instance;

            mAnimator = GetComponentInChildren<Animator>(); // 자식 오브젝트에서 Animator 컴포넌트를 찾음
            mRigidbody = GetComponent<Rigidbody2D>();

            if (mAnimator == null)
            {
                Debug.LogError("Animator component not found.");
            }

            if (mRigidbody == null)
            {
                Debug.LogError("mRigidbody2D component not found.");
            }
        }

        void Update()
        {
            if (IsDead)
            {
                if (mDeathCooldown <= 0)
                {
                    // 게임 재시작
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                    {
                        mGameManager.RestartGame();
                    }
                }
                else
                {
                    mDeathCooldown -= Time.deltaTime;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    mIsFlap = true;
                }
            }
        }

        private void FixedUpdate()
        {
            if (IsDead)
            {
                return;
            }

            Vector3 velocity = mRigidbody.velocity;
            velocity.x = ForwardSpeed; // 플레이어가 앞으로 이동하도록 설정

            if (mIsFlap)
            {
                velocity.y += FlapForce;
                mIsFlap = false;
            }

            // 계산한 velocity를 rigidbody에 넣어줘야함
            mRigidbody.velocity = velocity;

            // 비행기 각도 계산
            float angle = Mathf.Clamp(mRigidbody.velocity.y * 10f, -90, 90);

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (GodMode || IsDead)
            {
                return;
            }

            IsDead = true;
            mDeathCooldown = 1f;

            mAnimator.SetInteger("IsDie", 1);
            mGameManager.GameOver();
        }
    }
}
