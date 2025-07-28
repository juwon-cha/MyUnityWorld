using UnityEngine;

namespace MyUnityWorld
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private CustomizeSelectUI _customizeSelectUI;

        // 미니게임에서 복귀했을 때 플레이어 컨트롤러 초기화 및 저장된 설정 불러오기
        private void OnEnable()
        {
            LoadBestScores();
            LoadCustomizingSettings();

            _playerController = FindObjectOfType<PlayerController>();
            LoadPlayerCoordinates();
        }

        public void SavePlayerCoordinates()
        {
            if (_playerController != null)
            {
                GameData.PosX = Mathf.RoundToInt(_playerController.transform.position.x);
                GameData.PosY = Mathf.RoundToInt(_playerController.transform.position.y);
            }
            else
            {
                Debug.LogError("PlayerController is not assigned in GameManager.");
            }

            PlayerPrefs.SetInt("PlayerPosX", GameData.PosX);
            PlayerPrefs.SetInt("PlayerPosY", GameData.PosY);
        }

        public void LoadPlayerCoordinates()
        {
            if (_playerController != null)
            {
                GameData.PosX = PlayerPrefs.GetInt("PlayerPosX", 0);
                GameData.PosY = PlayerPrefs.GetInt("PlayerPosY", 0);

                // 플레이어의 위치를 GameData에서 불러와서 설정
                _playerController.transform.position = new Vector3(GameData.PosX, GameData.PosY, 0f);
            }
            else
            {
                Debug.LogError("PlayerController is not assigned in GameManager.");
            }
        }

        public void UpdateCharacterColor(Color color)
        {
            if (_playerController != null)
            {
                _playerController.ChangeColor(color);
            }
            else
            {
                Debug.LogError("PlayerController is not assigned in GameManager.");
            }
        }

        public void UpdateCharacterEquipment(Sprite sprite)
        {
            if (_playerController != null)
            {
                _playerController.ChangeEquipment(sprite);
            }
            else
            {
                Debug.LogError("PlayerController is not assigned in GameManager.");
            }
        }

        public void UpdateCharacterRide(RideHandler handler)
        {
            if(handler == null)
            {
                _playerController.RemoveRide();
                return;
            }

            if (_playerController != null)
            {
                _playerController.ChangeRide(handler);
            }
            else
            {
                Debug.LogError("PlayerController is not assigned in GameManager.");
            }
        }

        public void LoadBestScores()
        {
            // PlayerPrefs에서 최고 점수 문자열 불러옴
            GameData.FlappyPlaneBest = PlayerPrefs.GetString("FlappyPlaneBest", "0,0,0,0,0,0,0");
            GameData.TopDownBest = PlayerPrefs.GetString("TopDownBest", "0,0,0,0,0,0,0");
        }

        public void LoadCustomizingSettings()
        {
            // PlayerPrefs에서 선택된 색상, 장비, 탈것 인덱스 불러오기
            GameData.SelectedColorIndex = PlayerPrefs.GetInt("SelectedColorIndex", -1);
            GameData.SelectedEquipmentIndex = PlayerPrefs.GetInt("SelectedEquipmentIndex", -1);
            GameData.SelectedRideIndex = PlayerPrefs.GetInt("SelectedRideIndex", -1);

            _customizeSelectUI.UpdateColorButton(GameData.SelectedColorIndex);
            _customizeSelectUI.UpdateEquipButton(GameData.SelectedEquipmentIndex);
            _customizeSelectUI.UpdateRideButton(GameData.SelectedRideIndex);
        }
    }
}
