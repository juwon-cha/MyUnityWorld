using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FlappyPlane
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

        private void OnClickStartBtn()
        {
            GameManager.Instance.StartGame();
        }

        private void OnClickExitBtn()
        {
            GameManager.IsFirstLoading = true;
            SceneManager.LoadScene("MyUnityWorld");
        }

        protected override EUIState GetUIState()
        {
            return EUIState.HOME;
        }
    }
}
