using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TheStack
{
    public class GameUI : BaseUI
    {
        private TextMeshProUGUI mScore;
        private TextMeshProUGUI mCombo;
        private TextMeshProUGUI mMaxCombo;

        protected override EUIState GetUIState()
        {
            return EUIState.GAME;
        }

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);

            mScore = transform.Find("Score").GetComponent<TextMeshProUGUI>();
            mCombo = transform.Find("Combo").GetComponent<TextMeshProUGUI>();
            mMaxCombo = transform.Find("MaxCombo").GetComponent<TextMeshProUGUI>();
        }

        public void SetUI(int score, int combo, int maxCombo)
        {
            // TODO: int 범위 예외처리

            mScore.text = score.ToString();
            mCombo.text = combo.ToString();
            mMaxCombo.text = maxCombo.ToString();
        }
    }
}
