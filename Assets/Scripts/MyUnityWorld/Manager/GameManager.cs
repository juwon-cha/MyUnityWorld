using MyUnityWorld;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerController _playerController;

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
