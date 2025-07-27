using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameData
{
    // 플레이어 좌표
    public static int PosX = 0;
    public static int PosY = 0;

    // 게임 전체에서 공유될 선택된 색상의 인덱스
    public static int SelectedColorIndex = -1;

    // 선택된 장비의 인덱스
    public static int SelectedEquipmentIndex = -1;

    // 선택된 탈것의 인덱스
    public static int SelectedRideIndex = -1;

    // 미니 게임 최고 점수
    // 리더보드에서 상위 7개 점수만 표시
    public static string FlappyPlaneBest;
    public static string TopDownBest;

    public static void UpdateBestScore(int score, string miniGameScores, string key)
    {
        string savedScores = miniGameScores;
        if (string.IsNullOrEmpty(savedScores))
        {
            savedScores = "0,0,0,0,0,0,0";
        }

        // 문자열을 쉼표로 분리하고 각 항목을 정수로 변환하여 리스트 생성
        List<int> scoreList = savedScores.Split(',').Select(int.Parse).ToList();

        // 최고 점수 리스트에 추가
        scoreList.Add(score);

        // 리스트 내림차순 정렬
        scoreList.Sort((a, b) => b.CompareTo(a));

        // 리스트 크기를 최대 7개로 제한
        const int maxScores = 7;
        if (scoreList.Count > maxScores)
        {
            scoreList = scoreList.GetRange(0, maxScores);
        }

        // 업데이트된 점수 리스트를 다시 쉼표로 구분된 문자열로 변환
        string newBestScores = string.Join(",", scoreList);

        // 변환된 문자열을 GameData와 PlayerPrefs에 저장
        miniGameScores = newBestScores;
        PlayerPrefs.SetString(key, newBestScores);
        PlayerPrefs.Save();
    }
}
