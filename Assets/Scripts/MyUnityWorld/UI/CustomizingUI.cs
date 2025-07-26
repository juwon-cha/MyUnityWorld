using FlappyPlane;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyUnityWorld
{
    public class CustomizingUI : BaseUI
    {
        [SerializeField] private Button _colorSelectButton;
        [SerializeField] private Button _customizeButton;
        [SerializeField] private Button _rideButton;
        [SerializeField] private Button _exitButton;

        protected override EUIState GetUIState()
        {
            return EUIState.CUSTOMIZING;
        }

        public void Awake()
        {
            _colorSelectButton.onClick.AddListener(OnClickColorSelectButton);
            _customizeButton.onClick.AddListener(OnClickCustomizeButton);
            _rideButton.onClick.AddListener(OnClickRideButton);
            _exitButton.onClick.AddListener(OnClickExitButton);
        }

        public void OnClickColorSelectButton()
        {

        }

        public void OnClickCustomizeButton()
        {

        }

        public void OnClickRideButton()
        {

        }

        public void OnClickExitButton()
        {
            gameObject.SetActive(false);
        }
    }
}
