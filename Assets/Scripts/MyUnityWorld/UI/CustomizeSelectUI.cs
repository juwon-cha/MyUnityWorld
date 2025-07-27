using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyUnityWorld
{
    public class CustomizeSelectUI : MonoBehaviour
    {
        [SerializeField] private Image _characterPreview;
        [SerializeField] private RectTransform _equipmentPreview;
        [SerializeField] private RectTransform _ridePreview;
        [SerializeField] private List<ColorSelectButton> _colorSelectButtons;
        [SerializeField] private List<EquipmentSelectButton> _equipmentSelectButtons;
        [SerializeField] private List<RideSelectButton> _rideSelectButtons;

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

            for (int i = 0; i < _equipmentSelectButtons.Count; i++)
            {
                int index = i;

                Button button = _equipmentSelectButtons[index].GetComponent<Button>();
                button.onClick.AddListener(() => OnClickEquipButton(index));
            }

            for (int i = 0; i < _rideSelectButtons.Count; i++)
            {
                int index = i;

                Button button = _rideSelectButtons[index].GetComponent<Button>();
                button.onClick.AddListener(() => OnClickRideButton(index));
            }
        }

        private void OnEnable()
        {
            UpdateColorButton(GameData.SelectedColorIndex);
        }

        #region 색상 선택
        public void UpdateColorButton(int selectedIndex)
        {
            if (selectedIndex < 0 || selectedIndex >= _colorSelectButtons.Count)
            {
                return;
            }

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
        #endregion

        #region 장비 선택
        public void UpdateEquipButton(int selectedIndex)
        {
            if (selectedIndex < 0 || selectedIndex >= _equipmentSelectButtons.Count)
            {
                return;
            }

            // TEMP
            if (selectedIndex >= 3 && selectedIndex < _equipmentSelectButtons.Count)
            {
                _equipmentPreview.gameObject.SetActive(false);
                GameManager.Instance.UpdateCharacterEquipment(null, -1);

                // 체크 표시 해제
                for (int i = 0; i < _equipmentSelectButtons.Count; i++)
                {
                    _equipmentSelectButtons[i].SetInteractable(false);
                }

                return;
            }

            // 모든 버튼을 순회하며 선택 상태를 업데이트
            for (int i = 0; i < _equipmentSelectButtons.Count; i++)
            {
                // 현재 인덱스가 선택된 인덱스와 같으면 true, 아니면 false
                _equipmentSelectButtons[i].SetInteractable(i == selectedIndex);
            }

            // 프리뷰 이미지 업데이트
            EquipmentHandler equip = _equipmentSelectButtons[selectedIndex].gameObject.GetComponentInChildren<EquipmentHandler>();
            UpdatePreviewEquipment(equip.EquipmentRenderer.sprite);

            // 인게임 캐릭터 장비 업데이트
            GameManager.Instance.UpdateCharacterEquipment(equip.EquipmentRenderer.sprite, selectedIndex);
        }

        public void UpdatePreviewEquipment(Sprite sprite)
        {
            if (_characterPreview != null)
            {
                _equipmentPreview.gameObject.SetActive(true);

                Image previewImage = _equipmentPreview.GetComponent<Image>();
                previewImage.sprite = sprite;
            }
        }

        public void OnClickEquipButton(int index)
        {
            if (index < 0 || index >= _equipmentSelectButtons.Count)
            {
                Debug.LogError("잘못된 버튼 인덱스입니다: " + index);
                return;
            }

            // 선택한 색상 버튼의 인덱스 저장
            GameData.SelectedEquipmentIndex = index;

            // 변경된 인덱스를 기반으로 UI 상태를 즉시 업데이트
            UpdateEquipButton(index);
        }
        #endregion

        #region 탈것 선택
        public void UpdateRideButton(int selectedIndex)
        {
            if (selectedIndex < 0 || selectedIndex >= _rideSelectButtons.Count)
            {
                return;
            }

            // TEMP
            if (selectedIndex >= 3 && selectedIndex < _rideSelectButtons.Count)
            {
                _characterPreview.rectTransform.localPosition = new Vector3(_characterPreview.rectTransform.localPosition.x, 0, _characterPreview.rectTransform.localPosition.z); // 캐릭터 프리뷰 위치 조정
                _ridePreview.gameObject.SetActive(false);
                GameManager.Instance.UpdateCharacterRide(null, -1);

                // 체크 표시 해제
                for (int i = 0; i < _rideSelectButtons.Count; i++)
                {
                    _rideSelectButtons[i].SetInteractable(false);
                }

                return;
            }

            // 모든 버튼을 순회하며 선택 상태를 업데이트
            for (int i = 0; i < _rideSelectButtons.Count; i++)
            {
                // 현재 인덱스가 선택된 인덱스와 같으면 true, 아니면 false
                _rideSelectButtons[i].SetInteractable(i == selectedIndex);
            }

            // 프리뷰 이미지 업데이트
            RideHandler ride = _rideSelectButtons[selectedIndex].gameObject.GetComponentInChildren<RideHandler>();
            UpdatePreviewRide(ride.RideRenderer.sprite);

            // 인게임 탈것 업데이트
            GameManager.Instance.UpdateCharacterRide(ride, selectedIndex);
        }

        public void UpdatePreviewRide(Sprite sprite)
        {
            if (_characterPreview != null)
            {
                _characterPreview.rectTransform.localPosition = new Vector3(_characterPreview.rectTransform.localPosition.x, 120, _characterPreview.rectTransform.localPosition.z); // 캐릭터 프리뷰 위치 조정

                _ridePreview.gameObject.SetActive(true);

                Image previewImage = _ridePreview.GetComponent<Image>();
                previewImage.sprite = sprite;
            }
        }

        public void OnClickRideButton(int index)
        {
            if (index < 0 || index >= _rideSelectButtons.Count)
            {
                Debug.LogError("잘못된 버튼 인덱스입니다: " + index);
                return;
            }

            // 선택한 색상 버튼의 인덱스 저장
            GameData.SelectedEquipmentIndex = index;

            // 변경된 인덱스를 기반으로 UI 상태를 즉시 업데이트
            UpdateRideButton(index);
        }
        #endregion
    }
}
