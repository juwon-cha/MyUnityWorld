using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    // 플레이어 좌표
    public static int PosX = 0;
    public static int PosY = 0;

    // 게임 전체에서 공유될 선택된 색상의 인덱스
    public static int SelectedColorIndex = -1;

    // 미니 게임 최고 점수
    // 리더보드에서 상위 7개 점수만 표시
    public static string FlappyPlaneBest;
    public static string TheStackBest;
    public static string TopDownBest;
}
