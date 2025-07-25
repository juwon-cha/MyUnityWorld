using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

namespace TheStack
{
    public enum EUIState
    {
        HOME,
        GAME,
        SCORE
    }

    public class UIManager : MonoBehaviour
    {
        private static UIManager mInstance;
        public static UIManager Instance
        {
            get { return mInstance; }
        }

        private EUIState mCurrentState = EUIState.HOME;

        private HomeUI mHomeUI = null;
        private GameUI mGameUI = null;
        private ScoreUI mScoreUI = null;

        private TheStack mTheStack = null;

        private void Awake()
        {
            mInstance = this;

            mTheStack = FindObjectOfType<TheStack>();

            mHomeUI = GetComponentInChildren<HomeUI>(true);
            mHomeUI?.Init(this);

            mGameUI = GetComponentInChildren<GameUI>(true);
            mGameUI?.Init(this);

            mScoreUI = GetComponentInChildren<ScoreUI>(true);
            mScoreUI?.Init(this);

            ChangeState(EUIState.HOME);
        }

        public void ChangeState(EUIState state)
        {
            mCurrentState = state;
            mHomeUI?.SetActive(mCurrentState);
            mGameUI?.SetActive(mCurrentState);
            mScoreUI?.SetActive(mCurrentState);
        }

        public void OnClickStart()
        {
            mTheStack.Restart();
            ChangeState(EUIState.GAME);
        }

        public void OnClickExit()
        {
            Screen.SetResolution(1920, 1080, true);

            SceneManager.LoadScene("MyUnityWorld");
        }

        public void UpdateScore()
        {
            mGameUI.SetUI(mTheStack.Score, mTheStack.Combo, mTheStack.MaxCombo);
        }

        public void SetScoreUI()
        {
            mScoreUI.SetUI(mTheStack.Score, mTheStack.MaxCombo, mTheStack.BestScore, mTheStack.BestCombo);
            ChangeState(EUIState.SCORE);
        }
    }
}
