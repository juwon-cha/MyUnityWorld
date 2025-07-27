using MyUnityWorld;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerController _playerController;

    // 미니게임에서 복귀했을 때 플레이어 컨트롤러 초기화 및 저장된 설정 불러오기
    private void OnEnable()
    {
        LoadBestScores();

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

    public void LoadBestScores()
    {
        // PlayerPrefs에서 최고 점수 문자열 불러옴
        GameData.FlappyPlaneBest = PlayerPrefs.GetString("FlappyPlaneBest", "0000000");
        GameData.TheStackBest = PlayerPrefs.GetString("TheStackBest", "0000000");
        GameData.TopDownBest = PlayerPrefs.GetString("TopDownBest", "0000000");
    }
}
