using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownShooting
{
    public class GameUI : BaseUI
    {
        [SerializeField] private TextMeshProUGUI _waveText;
        [SerializeField] private Slider _hpSlider;

        private void Start()
        {
            UpdateHPSlider(1);
        }

        public void UpdateHPSlider(float percent)
        {
            _hpSlider.value = percent;
        }

        public void UpdateWaveTxt(int wave)
        {
            _waveText.text = wave.ToString();
        }

        protected override EUIState GetUIState()
        {
            return EUIState.GAME;
        }
    }
}
