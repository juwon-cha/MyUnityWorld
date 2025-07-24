using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TopDownShooting
{
    public class GameOverUI : BaseUI
    {
        [SerializeField] private Button _restartBtn;
        [SerializeField] private Button _exitBtn;

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);
            _restartBtn.onClick.AddListener(OnClickRestartBtn);
            _exitBtn.onClick.AddListener(OnClickExitBtn);
        }

        public void OnClickRestartBtn()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void OnClickExitBtn()
        {
            Application.Quit();
            // If running in the editor, stop playing
        }

        protected override EUIState GetUIState()
        {
            return EUIState.GAME_OVER;
        }
    }
}
