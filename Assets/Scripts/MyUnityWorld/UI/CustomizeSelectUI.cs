using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace MyUnityWorld
{
    public class CustomizeSelectUI : MonoBehaviour
    {
        [SerializeField] private Image _characterPreview;
        [SerializeField] private List<ColorSelectButton> _colorSelectButtons;

        private void Awake()
        {
            // 버튼 리스너 자동 등록
            for (int i = 0; i < _colorSelectButtons.Count; i++)
            {
                // 클로저(closure) 문제를 피하기 위해 인덱스를 별도 변수에 복사
                int index = i;

                Button button = _colorSelectButtons[index].GetComponent<Button>();
                button.onClick.AddListener(() => OnClickColorButton(index));
            }
        }

        private void OnEnable()
        {
            UpdateColorButton(GameData.SelectedColorIndex);
        }

        public void UpdateColorButton(int selectedIndex)
        {
            if (selectedIndex < 0 || selectedIndex >= _colorSelectButtons.Count) return;

            // 모든 버튼을 순회하며 선택 상태를 업데이트
            for (int i = 0; i < _colorSelectButtons.Count; i++)
            {
                // 현재 인덱스가 선택된 인덱스와 같으면 true, 아니면 false
                _colorSelectButtons[i].SetInteractable(i == selectedIndex);
            }

            // 선택된 버튼의 색상 정보를 가져옴
            ColorSelectButton select = _colorSelectButtons[selectedIndex];
            Button button = select.GetComponent<Button>();
            Color selectedColor = button.image.color;

            // 프리뷰 이미지 색상 업데이트
            UpdatePreviewColor(selectedColor);

            // 인게임 캐릭터 색상 업데이트
            GameManager.Instance.UpdateCharacterColor(selectedColor);
        }

        public void UpdatePreviewColor(Color color)
        {
            if (_characterPreview != null)
            {
                _characterPreview.color = color;
            }
        }

        public void OnClickColorButton(int index)
        {
            if (index < 0 || index >= _colorSelectButtons.Count)
            {
                Debug.LogError("잘못된 버튼 인덱스입니다: " + index);
                return;
            }

            // 선택한 색상 버튼의 인덱스 저장
            GameData.SelectedColorIndex = index;

            // 변경된 인덱스를 기반으로 UI 상태를 즉시 업데이트
            UpdateColorButton(index);
        }
    }
}
