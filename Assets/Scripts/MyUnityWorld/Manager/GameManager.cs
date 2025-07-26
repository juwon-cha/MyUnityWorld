using MyUnityWorld;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerController _playerController;

    // 미니게임에서 복귀했을 때 플레이어 컨트롤러 초기화
    private void OnEnable()
    {
        _playerController = FindObjectOfType<PlayerController>();
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
}
