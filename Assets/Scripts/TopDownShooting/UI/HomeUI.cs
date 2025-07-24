using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownShooting
{
    public class HomeUI : BaseUI
    {
        [SerializeField] private Button _startBtn;
        [SerializeField] private Button _exitBtn;

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);

            _startBtn.onClick.AddListener(OnClickStartBtn);
            _exitBtn.onClick.AddListener(OnClickExitBtn);
        }

        public void OnClickStartBtn()
        {
            GameManager.Instance.StartGame();
        }

        public void OnClickExitBtn()
        {
            Application.Quit();
            // If running in the editor, stop playing
        }

        protected override EUIState GetUIState()
        {
            return EUIState.HOME;
        }
    }
}
