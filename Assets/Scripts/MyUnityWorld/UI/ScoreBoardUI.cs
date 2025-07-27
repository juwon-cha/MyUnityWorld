using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoardUI : MonoBehaviour
{
    // 리더보드에서 상위 7개 점수만 표시
    [SerializeField] private List<BestScore> _ScoreTexts;

    // 스코어 초기화
    private void Awake()
    {
        UpdateScore(GameData.FlappyPlaneBest);
    }

    // 스코어 업데이트
    public void UpdateScore(string bestScores)
    {
        if (bestScores.Length < 0)
        {
            return;
        }

        for (int i = 0; i < bestScores.Length; ++i)
        {
            // 점수 문자열에서 해당 인덱스의 점수를 가져옴
            int scoreValue = 0;
            if(int.TryParse(bestScores[i].ToString(), out scoreValue))
            {
                _ScoreTexts[i].SetBestScore(scoreValue);
            }
        }
    }
}
