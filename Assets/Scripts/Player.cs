using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator mAnimator;
    private Rigidbody2D mRigidbody;

    public float FlapForce = 6f; // �����ϴ� ��
    public float ForwardSpeed = 3f; // ������ �̵��ϴ� �ӵ�
    public bool IsDead = false; // �÷��̾ �׾����� ����
    private float mDeathCooldown = 0.5f; // �浹 �� �ٷ� �״°� �ƴ϶� 0.5�� �Ŀ� �׵��� ����

    private bool mIsFlap = false; // �÷��̾ ���� ������ ����

    public bool GodMode = false; // �׽�Ʈ ���

    private GameManager mGameManager;

    void Start()
    {
        mGameManager = GameManager.Instance;

        mAnimator = GetComponentInChildren<Animator>(); // �ڽ� ������Ʈ���� Animator ������Ʈ�� ã��
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
        if(IsDead)
        {
            if(mDeathCooldown <= 0)
            {
                // ���� �����
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
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                mIsFlap = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if(IsDead)
        {
            return;
        }

        Vector3 velocity = mRigidbody.velocity;
        velocity.x = ForwardSpeed; // �÷��̾ ������ �̵��ϵ��� ����

        if (mIsFlap)
        {
            velocity.y += FlapForce;
            mIsFlap = false;
        }

        // ����� velocity�� rigidbody�� �־������
        mRigidbody.velocity = velocity;

        // ����� ���� ���
        float angle = Mathf.Clamp(mRigidbody.velocity.y * 10f, -90, 90);

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(GodMode || IsDead)
        {
            return;
        }

        IsDead = true;
        mDeathCooldown = 1f;

        mAnimator.SetInteger("IsDie", 1);
        mGameManager.GameOver();
    }
}
