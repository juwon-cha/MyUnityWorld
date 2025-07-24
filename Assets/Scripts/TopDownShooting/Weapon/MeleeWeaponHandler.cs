using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace TopDownShooting
{
    public class MeleeWeaponHandler : WeaponHandler
    {
        [Header("Melee Attack Info")]
        public Vector2 ColliderBoxSize = Vector2.one; // 근거리 공격(충돌) 범위

        protected override void Start()
        {
            base.Start();

            ColliderBoxSize = ColliderBoxSize * WeaponSize; // 무기 크기에 맞춰 충돌 범위 크기 조정
        }

        public override void Attack()
        {
            base.Attack();

            RaycastHit2D hit = Physics2D.BoxCast(
                // 현재 위치에서 바라보는 방향으로 박스 캐스트
                transform.position + (Vector3)BaseController.LookDirection * ColliderBoxSize.x, // 위치
                ColliderBoxSize, // 사이즈
                0f, // 각도
                Vector2.zero, // 방향
                0f, // 거리
                Target // 레이어 마스크
            );

            if (hit.collider != null)
            {
                ResourceController resourceController = hit.collider.GetComponent<ResourceController>();
                if (resourceController != null)
                {
                    resourceController.ChangeHealth(-Power); // 데미지 적용
                    if(IsOnKnockBack)
                    {
                        // 넉백 처리
                        BaseController controller = hit.collider.GetComponent<BaseController>();
                        if (controller != null)
                        {
                            controller.ApplyKnockBack(transform, KnockBackPower, KnockBackDuration);
                        }
                    }
                }
            }
        }

        public override void Rotate(bool isLeft)
        {
            if(isLeft)
            {
                transform.eulerAngles = new Vector3(0, 180, 0); // 왼쪽으로 회전
            }
            else
            {
                transform.eulerAngles = Vector3.zero; // 오른쪽으로 회전
            }
        }
    }
}
