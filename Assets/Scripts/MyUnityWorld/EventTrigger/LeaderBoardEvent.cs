using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUnityWorld
{
    public class LeaderBoardEvent : BaseEvent
    {
        [SerializeField] private LeaderBoardUI _leaderBoardUI;

        public override void StartEvent(Collider2D collision)
        {
            // 커스터마이징 UI 활성화
            UIManager.Instance.SetLeaderBoardUI();
        }
    }
}

