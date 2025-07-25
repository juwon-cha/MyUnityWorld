using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlappyPlane
{
    public class GameUI : BaseUI
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        public void UpdateScore(int socre)
        {
            _scoreText.text = socre.ToString();
        }

        protected override EUIState GetUIState()
        {
            return EUIState.GAME;
        }
    }
}
