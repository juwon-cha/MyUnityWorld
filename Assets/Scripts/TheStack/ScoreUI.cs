using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheStack
{
    public class ScoreUI : BaseUI
    {
        private TextMeshProUGUI mScore;
        private TextMeshProUGUI mCombo;
        private TextMeshProUGUI mBestScore;
        private TextMeshProUGUI mBestCombo;

        private Button mStartBtn;
        private Button mExitBtn;

        protected override EUIState GetUIState()
        {
            return EUIState.SCORE;
        }

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);

            mScore = transform.Find("Image/ScoreTxt/Score").GetComponent<TextMeshProUGUI>();
            mCombo = transform.Find("Image/ComboTxt/Combo").GetComponent<TextMeshProUGUI>();
            mBestScore = transform.Find("Image/BestScoreTxt/BestScore").GetComponent<TextMeshProUGUI>();
            mBestCombo = transform.Find("Image/BestComboTxt/BestCombo").GetComponent<TextMeshProUGUI>();

            mStartBtn = transform.Find("StartBtn").GetComponent<Button>();
            mExitBtn = transform.Find("ExitBtn").GetComponent<Button>();

            // 버튼 클릭 이벤트 등록
            mStartBtn.onClick.AddListener(OnClickStartBtn);
            mExitBtn.onClick.AddListener(OnClickExitBtn);
        }

        public void SetUI(int score, int combo, int bestScore, int bestCombo)
        {
            // TODO: int 범위 예외처리

            mScore.text = score.ToString();
            mCombo.text = combo.ToString();
            mBestScore.text = bestScore.ToString();
            mBestCombo.text = bestCombo.ToString();
        }

        private void OnClickStartBtn()
        {
            mUIManager.OnClickStart();
        }

        private void OnClickExitBtn()
        {
            mUIManager.OnClickExit();
        }
    }
}
