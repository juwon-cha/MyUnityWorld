using FlappyPlane;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyUnityWorld
{
    public class CustomizingUI : BaseUI
    {
        [SerializeField] private Image _characterPreview;
        [SerializeField] private List<ColorSelectButton> _colorSelectButtons;

        protected override EUIState GetUIState()
        {
            return EUIState.CUSTOMIZING;
        }

        private void Start()
        {
            var inst = Instantiate(_characterPreview.material);
            _characterPreview.material = inst;
        }

        public void UpdateColorButton()
        {

        }

        public void UpdatePreviewColor(/*EPlayerColor color*/)
        {
            if (_characterPreview != null && _characterPreview.material != null)
            {
                //_characterPreview.material.SetColor("_PlayerColor", PlayerColor.GetColor(color));
            }
        }

        public void OnClickColorButton(int index)
        {

        }
    }
}
